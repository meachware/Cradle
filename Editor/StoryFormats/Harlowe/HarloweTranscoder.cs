﻿#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using UnityTwine.Editor.Utils;

namespace UnityTwine.Editor.StoryFormats.Harlowe
{
	public class HarloweTranscoder : StoryFormatTranscoder
	{
		public static Dictionary<string, CodeGenMacro> CodeGenMacros = new Dictionary<string, CodeGenMacro>(StringComparer.OrdinalIgnoreCase);

		HarlowePassageData _input;
		TwinePassageCode _output;
		string _lastVariable;

		public GeneratedCode Code { get; private set; }

		static Regex rx_LinkNames = new Regex(@"((?'linkName'[^|\n]+?)\s*=\s*)?(?'linkText'.*)",
			RegexOptions.IgnoreCase |
			RegexOptions.ExplicitCapture);

		static HarloweTranscoder()
		{
			// Supported macros
			CodeGenMacros["put"] =
			CodeGenMacros["move"] = 
			CodeGenMacros["set"] = BuiltInCodeGenMacros.Assignment;

			CodeGenMacros["unless"] =
			CodeGenMacros["if"] =
			CodeGenMacros["elseif"] =
			CodeGenMacros["else"] = BuiltInCodeGenMacros.Conditional;

			CodeGenMacros["link"] =
			CodeGenMacros["linkgoto"] =
			CodeGenMacros["linkreveal"] =
			CodeGenMacros["linkrepeat"] = BuiltInCodeGenMacros.Link;

			CodeGenMacros["goto"] = BuiltInCodeGenMacros.GoTo;

			CodeGenMacros["align"] =
			CodeGenMacros["font"] =
			CodeGenMacros["css"] =
			CodeGenMacros["background"] =
			CodeGenMacros["color"] =
				CodeGenMacros["colour"] =
				CodeGenMacros["text-color"] =
				CodeGenMacros["text-colour"] =
			CodeGenMacros["text-style"] =
			CodeGenMacros["text-rotate"] =
			CodeGenMacros["transition"] =
				CodeGenMacros["t8n"] = 
			CodeGenMacros["hook"] = BuiltInCodeGenMacros.Style;

			CodeGenMacros["print"] = BuiltInCodeGenMacros.Print;
		}

		public HarloweTranscoder(TwineImporter importer) : base(importer)
		{
		}

		public override StoryFormatMetadata Metadata
		{
			get
			{
				return new StoryFormatMetadata()
				{
					StoryFormatName = "Harlowe",
					StoryBaseType = typeof(UnityTwine.StoryFormats.Harlowe.HarloweStory),
					RuntimeMacrosType = typeof(UnityTwine.StoryFormats.Harlowe.HarloweRuntimeMacros),
					StrictMode = true
				};
			}
		}

		public override void Init()
		{
			// Run the story file in PhantomJS, inject the bridge script that invokes the Harlowe lexer and deserialize the JSON output
			PhantomOutput<HarlowePassageData[]> output = PhantomJS.Run<HarlowePassageData[]>(
				new System.Uri(Application.dataPath + "/../" + Importer.AssetPath).AbsoluteUri,
				new System.Uri(Application.dataPath + "/Plugins/UnityTwine/Editor/StoryFormats/Harlowe/.js/harlowe.bridge.js").AbsolutePath
			);

			this.Importer.Passages.AddRange(output.result);
		}

		public override TwinePassageCode PassageToCode(TwinePassageData p)
		{
			if (p is HarlowePassageData == false)
				throw new NotSupportedException("HarloweParser called with incompatible passage data");

			_input = (HarlowePassageData)p;
			_output = new TwinePassageCode();
			Code = new GeneratedCode();

			GenerateBody(_input.Tokens);

			// Get final string
			string code = Code.Buffer.ToString();
			_output.Main = code;

			return _output;
		}

		public void GenerateBody(LexerToken[] tokens, bool breaks = true)
		{
			for (int t = 0; t < tokens.Length; t++)
			{
				LexerToken token = tokens[t];

				switch (token.type)
				{
					case "text":
						Code.Indent();
						GenerateText(token.text, true);
						break;

					case "verbatim":
						Code.Indent();
						GenerateText(token.innerText, true);
						break;

					case "bulleted":
					case "numbered":
						Debug.LogWarning("Bulleted and numbered lists not currently supported");
						break;

					case "italic":
					case "bold":
					case "em":
					case "del":
					case "strong":
					case "sup":
						Code.Indent();
						GenerateStyle(string.Format("\"{0}\", true", token.type), token.tokens);
						break;

					case "collapsed":
						bool wasCollapsed = Code.Collapsed;
						Code.Collapsed = true;
						GenerateBody(token.tokens, false);
						Code.Collapsed = wasCollapsed;
						break;

					case "br":
						if (!Code.Collapsed)
						{
							Code.Indent();
							GenerateLineBreak();
						}
						break;

					case "whitespace":
						if (!Code.Collapsed)
						{
							Code.Indent();
							GenerateText(token.text, true);
						}
						break;

					case "variable":
						Code.Indent();
						int hookIndex = FollowedBy("hook", tokens, t, true, false);
						if (hookIndex >= 0)
						{
							GenerateStyle(BuildVariableRef(token), tokens[hookIndex].tokens);
							t = hookIndex;
						}
						else
							GenerateText(BuildVariableRef(token), false);
						break;

					case "macro": {

						// Can't reference variables from other macros
						_lastVariable = null;

						// If macro is followed by a hook, tell the macro
						t = GenerateMacro(tokens, t, FollowedBy("hook", tokens, t, true, false) >= 0 ?
							MacroUsage.LineAndHook :
							MacroUsage.Line
						);
						break;
					}

					case "hook":
					default:
						break;
				}
			}

			if (breaks)
			{
				Code.Indent();
				Code.Buffer.Append("yield break;");
			}
		}

		public void GenerateText(string text, bool isString)
		{
			Code.Buffer.Append("yield return text(");
			if (isString)
			{
				Code.Buffer.Append("\"");
				Code.Buffer.Append(text.Replace("\"", "\\\""));
				Code.Buffer.Append("\"");
			}
			else
				Code.Buffer.Append(text);

			Code.Buffer.AppendLine(");");
		}

		public void GenerateLineBreak()
		{
			Code.Buffer.AppendLine("yield return lineBreak();");
		}

		public void GenerateStyle(string styleParams, LexerToken[] tokens)
		{
			Code.Buffer
				.AppendFormat("using (Style.Apply({0})) {{", styleParams)
				.AppendLine();
			Code.Indentation++;
			GenerateBody(tokens, breaks: false);
			Code.Indentation--;
			Code.Indent();
			Code.Buffer.AppendLine("}");
		}

		public string GenerateFragment(LexerToken[] tokens)
		{
			GeneratedCode outer = Code;
			Code = new GeneratedCode();
			GenerateBody(tokens);
			_output.Fragments.Add(Code.Buffer.ToString());
			Code = outer;
			return string.Format("passage{0}_Fragment_{1}", _input.Pid, _output.Fragments.Count - 1);
		}

		public int GenerateMacro(LexerToken[] tokens, int macroTokenIndex, MacroUsage usage)
		{
			LexerToken macroToken = tokens[macroTokenIndex];
			CodeGenMacro macro;

			if (!CodeGenMacros.TryGetValue(macroToken.name, out macro))
				macro = BuiltInCodeGenMacros.RuntimeMacro;

			if (macro != null)
			{
				if (usage != MacroUsage.Inline)
					Code.Indent();

				return macro(this, tokens, macroTokenIndex, usage);
			}
			else
				return macroTokenIndex;
		}

		public void GeneratedAssignment(string assignType, LexerToken[] assignTokens, int start, int end)
		{
			string delimeter = assignType == "set" ? "to" : "into";
			bool usesPropertyOperator = false;

			int t = start;
			for (; t <= end; t++)
			{
				LexerToken token = assignTokens[t];
				if (token.type == delimeter)
				{
					bool close;
					string assignCode;
					if (assignType == "set")
					{
						// Special case: when GetMember or AsPropertyOf will be used in the left-side expression, can't use = but must use ReplaceWith
						assignCode = usesPropertyOperator ? ".ReplaceWith(" : "= ";
						close = usesPropertyOperator;
					}
					else
					{
						assignCode = assignType == "move" ? ".MoveInto(" : ".PutInto(";
						close = true;
					}

					GenerateExpression(assignTokens, start, t-1);
					Code.Buffer.Append(assignCode);
					GenerateExpression(assignTokens, t+1, end);
					if (close)
						Code.Buffer.Append(")");

					return;
				}
				else
				{
					usesPropertyOperator |=
						token.type == "belongingProperty" ||
						token.type == "possessiveOperator" ||
						token.type == "belongingOperator";
				}
			}

			throw new TwineTranscodingException(string.Format("The '{0}' assignment was not written correctly.", assignType));
		}

		string BuildInverseAssignment(LexerToken[] tokens, ref int tokenIndex, bool moveAssignment)
		{
			string statement = string.Format(".{0}(vref(Vars, \"", moveAssignment ? "MoveInto" : "PutInto");
			tokenIndex++;
			for (; tokenIndex < tokens.Length; tokenIndex++)
			{
				LexerToken token = tokens[tokenIndex];
				if (token.type == "whitespace")
					continue;

				if (token.type != "variable")
					break;

				statement += token.name + "\"))";
				return statement;
			}

			throw new TwineTranscodingException(string.Format("{0} macro was not formatted correctly", moveAssignment ? "Move" : "Put"));
		}

		public void GenerateExpression(LexerToken[] tokens, int start = 0, int end = -1)
		{
			// Skip whitespace at beginning of expression
			while (start < tokens.Length && tokens[start].type == "whitespace")
				start++;

			for (int t = start; t < (end > 0 ? end + 1 : tokens.Length); t++)
			{
				LexerToken token = tokens[t];
				string expr = BuildExpressionSegment(tokens, ref t);
				if (expr != null)
					Code.Buffer.Append(expr);
			}
		}

		string BuildVariableRef(LexerToken token)
		{
			Importer.RegisterVar(token.name);
			_lastVariable = string.Format("Vars.{0}", EscapeReservedWord (token.name));
			return _lastVariable;
		}

		string BuildExpressionSegment(LexerToken[] tokens, ref int tokenIndex)
		{
			LexerToken token = tokens[tokenIndex];
			switch (token.type)
			{
				case "variable":
					return BuildVariableRef(token);
				case "identifier":
					if (_lastVariable == null)
						throw new TwineTranscodingException("'it' or 'its' used without first mentioning a variable");
					return _lastVariable;
				case "macro":
					GenerateMacro(tokens, tokenIndex, MacroUsage.Inline);
					return null;
				case "hookRef":
					return string.Format("hookRef(\"{0}\")", token.name);
				case "string":
					return WrapInVar(string.Format("\"{0}\"", token.innerText), tokens, tokenIndex);
				case "number":
					return WrapInVar(token.text, tokens, tokenIndex);
				case "colour":
					return WrapInVar(string.Format("\"{0}\"", token.text), tokens, tokenIndex);
				case "grouping":
					if(WrapInVarRequired(tokens, tokenIndex))
						Code.Buffer.Append(" v");
					Code.Buffer.Append("(");
					GenerateExpression(token.tokens);
					Code.Buffer.Append(")");
					return null;
				case "itsProperty":
					if (_lastVariable == null)
						throw new TwineTranscodingException("'it' or 'its' used without first mentioning a variable");
					return string.Format("{0}[\"{1}\"]", _lastVariable, token.name);
				case "property":
					return string.Format("[\"{0}\"]", token.name);
				case "belongingProperty":
					FollowedBy("grouping", tokens, tokenIndex, true, true);
					return string.Format("v(\"{0}\").AsMemberOf", token.name);
				case "contains":
					FollowedBy("grouping", tokens, tokenIndex, true, true);
					return ".Contains";
				case "isIn":
					FollowedBy("grouping", tokens, tokenIndex, true, true);
					return ".ContainedBy";
				case "possessiveOperator":
					FollowedBy("grouping", tokens, tokenIndex, true, true);
					return ".GetMember";
				case "belongingOperator":
					FollowedBy("grouping", tokens, tokenIndex, true, true);
					return ".AsMemberOf";
				case "and":
					return "&&";
				case "or":
					return "||";
				case "is":
					return "==";
				case "isNot":
					return "!=";
				case "not":
					return "!";
				case "spread":
					return "(HarloweSpread)";
				case "to":
				case "into":
					throw new TwineTranscodingException(string.Format("'{0}' is an assignment keyword and cannot be used inside an expression.", token.type));
				default:
					return token.text;
			}
		}

		bool WrapInVarRequired(LexerToken[] tokens, int tokenIndex)
		{
			bool wrap = false;

			if (tokenIndex < tokens.Length - 1)
			{
				for (int t = tokenIndex + 1; t < tokens.Length; t++)
				{
					switch(tokens[t].type)
					{
						case "property":
						case "contains":
						case "isIn":
						case "into":
						case "possessiveOperator":
						case "belongingOperator":
							wrap = true;
							break;
						case "whitespace":
							continue;
						default:
							break;
					}
				}
			}

			return wrap;
		}

		string WrapInVar(string expr, LexerToken[] tokens, int tokenIndex)
		{
			return WrapInVarRequired(tokens, tokenIndex) ?
				string.Format("v({0})", expr) :
				expr;
		}

		int FollowedBy(string tokenType, LexerToken[] tokens, int tokenIndex, bool allowWhitespace, bool throwException)
		{
			int index = -1;

			if (tokenIndex < tokens.Length - 1)
			{
				for (int t = tokenIndex + 1; t < tokens.Length; t++)
				{
					string ttype = (tokens[t].type);
					if (ttype == tokenType)
					{
							index = t;
							break;
					}
					else
					{
						if (ttype == "whitespace")
						{
							if (!allowWhitespace)
								break;
							else
								continue;
						}
						else
							break;
					}
				}
			}

			if (throwException && index < 0)
				throw new TwineTranscodingException(string.Format(
					"'{0}' must be followed by a '{1}' ",
					tokens[tokenIndex].text,
					tokenType
				));

			return index;
		}
	}

	[Serializable]
	public class LexerToken
	{
		public string type;
		public string name;
		public string text;
		public string innerText;
		public string value;
		public string passage;
		public LexerToken[] tokens;
	}

	[Serializable]
	public class HarlowePassageData : TwinePassageData
	{
		public LexerToken[] Tokens;
	}

	public class GeneratedCode
	{
		public StringBuilder Buffer = new StringBuilder();
		public int Indentation = 0;
		public bool Collapsed = false;

		public void Indent()
		{
			Utils.CodeGenUtils.Indent(Indentation, Buffer);
		}
	}
}
#endif