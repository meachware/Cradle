﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System;
using System.Reflection;

namespace UnityTwine.Editor
{
	public abstract class TwineImporter
	{
		public readonly string AssetPath;
		public StoryFormatTranscoder Transcoder {get; protected set;}
		public TwinePassageData CurrentPassage { get; private set; }

		public readonly List<TwinePassageData> Passages = new List<TwinePassageData>();
		public readonly HashSet<string> Vars = new HashSet<string>();
		public HashSet<MacroLib> MacroLibs { get; private set; }
		public Dictionary<string,MacroDef> Macros { get; private set; }

		public TwineImporter(string assetPath)
		{
			this.AssetPath = assetPath;
		}

		public virtual bool Validate() { return true; }
		public abstract void Load();
		
		public void Transcode()
		{
			if (this.Transcoder == null)
				throw new System.NotImplementedException("TwineImporter.Transcoder must be set by the importer implementation.");

			// Load macro types 
			Macros = new Dictionary<string, MacroDef>(StringComparer.OrdinalIgnoreCase);
			MacroLibs = new HashSet<MacroLib>(LoadMacros<TwineRuntimeMacros>(Macros));

			this.Transcoder.Init();

			for (int i = 0; i < this.Passages.Count; i++)
			{
				CurrentPassage = this.Passages[i];

				CurrentPassage.Tags = Regex.Replace(CurrentPassage.Tags, @"([^\s]+)", "\"$&\",");

				try
				{
					CurrentPassage.Code = this.Transcoder.PassageToCode(CurrentPassage);
				}
				catch(TwineTranscodingException ex)
				{
					ex.Passage = CurrentPassage.Name;
					throw;
				}
			}
		}

		public void RegisterVar(string name)
		{
			Vars.Add(name);
		}

		IEnumerable<MacroLib> LoadMacros<T>(Dictionary<string, MacroDef> macros)
		{
			string projectDir = Directory.GetParent((Path.GetFullPath(Application.dataPath))).FullName;
			Type baseType = typeof(T);

			int libIndex = 0;
			foreach(Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				// Skip references external to the project
				if (!string.IsNullOrEmpty(assembly.Location) && !Path.GetFullPath(assembly.Location).StartsWith(projectDir, StringComparison.OrdinalIgnoreCase))
					continue;
				
				foreach(Type type in assembly.GetTypes())
				{
					if (type.IsAbstract || type.IsNested || !baseType.IsAssignableFrom(type))
						continue;

					MacroLib macroLib = new MacroLib()
					{
						Type = type
					};

					bool hasMethods = false;
					foreach (MethodInfo method in type.GetMethods())
					{
						var attr = (TwineRuntimeMacroAttribute) Attribute.GetCustomAttribute(method, typeof(TwineRuntimeMacroAttribute), true);
						if (attr == null)
							continue;

						hasMethods = true;
						macros[attr.TwineName ?? method.Name] = new MacroDef()
						{
							Name = method.Name,
							Lib = macroLib
						};
					}

					if (hasMethods)
					{
						macroLib.Name = "macros" + (++libIndex).ToString();
						yield return macroLib;
					}
				}
			}
		}
	}

	public class MacroLib
	{
		public Type Type;
		public string Name;
	}

	public class MacroDef
	{
		public string Name;
		public MacroLib Lib;
	}
}