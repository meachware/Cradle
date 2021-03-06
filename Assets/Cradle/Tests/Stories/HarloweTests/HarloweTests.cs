﻿/*
------------------------------------------------
Generated by Cradle 2.0.0.0 on 5/16/2016 2:14:56 PM
https://github.com/daterre/Cradle

Original file: HarloweTests.html
Story format: Harlowe
------------------------------------------------
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cradle;
using IStoryThread = System.Collections.Generic.IEnumerable<Cradle.StoryOutput>;
using Cradle.StoryFormats.Harlowe;

public partial class @HarloweTests: Cradle.StoryFormats.Harlowe.HarloweStory
{
	#region Variables
	// ---------------

	public class VarDefs: RuntimeVars
	{
		public VarDefs()
		{
			VarDef("str", () => this.@str, val => this.@str = val);
			VarDef("b", () => this.@b, val => this.@b = val);
			VarDef("b2", () => this.@b2, val => this.@b2 = val);
			VarDef("int", () => this.@int, val => this.@int = val);
			VarDef("dec", () => this.@dec, val => this.@dec = val);
			VarDef("naked", () => this.@naked, val => this.@naked = val);
			VarDef("red", () => this.@red, val => this.@red = val);
			VarDef("ar", () => this.@ar, val => this.@ar = val);
			VarDef("dst", () => this.@dst, val => this.@dst = val);
			VarDef("map", () => this.@map, val => this.@map = val);
		}

		public StoryVar @str;
		public StoryVar @b;
		public StoryVar @b2;
		public StoryVar @int;
		public StoryVar @dec;
		public StoryVar @naked;
		public StoryVar @red;
		public StoryVar @ar;
		public StoryVar @dst;
		public StoryVar @map;
	}

	public new VarDefs Vars
	{
		get { return (VarDefs) base.Vars; }
	}

	// ---------------
	#endregion

	#region Initialization
	// ---------------

	public readonly Cradle.StoryFormats.Harlowe.HarloweRuntimeMacros macros1;
	public readonly UnityTwineTestMacros macros2;

	@HarloweTests()
	{
		base.Vars = new VarDefs() { Story = this, StrictMode = true };

		macros1 = new Cradle.StoryFormats.Harlowe.HarloweRuntimeMacros() { Story = this };
		macros2 = new UnityTwineTestMacros() { Story = this };
	}
	
	void Awake() {
		base.Init();
		passage1_Init();
		passage2_Init();
		passage3_Init();
		passage4_Init();
		passage5_Init();
		passage6_Init();
		passage7_Init();
		passage8_Init();
		passage9_Init();
		passage10_Init();
		passage11_Init();
		passage12_Init();
		passage13_Init();
		passage14_Init();
		passage15_Init();
		passage16_Init();
		passage17_Init();
		passage18_Init();
		passage19_Init();
		passage20_Init();
		passage21_Init();
	}

	// ---------------
	#endregion

	// .............
	// #1: Result

	void passage1_Init()
	{
		this.Passages["Result"] = new StoryPassage("Result", new string[]{  }, passage1_Main);
	}

	#line 1 "Result"
	IStoryThread passage1_Main()
	{
		yield return lineBreak();
		yield return text("Yay! This is the end of the test.");
		yield break;
	}


	// .............
	// #2: Vars_Type_String

	void passage2_Init()
	{
		this.Passages["Vars_Type_String"] = new StoryPassage("Vars_Type_String", new string[]{  }, passage2_Main);
	}

	#line 1 "Vars_Type_String"
	IStoryThread passage2_Main()
	{
		Vars.str  = "A";
		macros2.assert(Vars.str == "A", "equality (left)");
		macros2.assert("A" == Vars.str, "equality (right)");
		macros2.assert(Vars.str != "ABC", "inequality (left)");
		macros2.assert("ABC" != Vars.str, "inequality (right)");
		Vars.str  = Vars.str + "B";
		macros2.assert(Vars.str == "AB", "join operator (left)");
		Vars.str  = "C" + Vars.str;
		macros2.assert(Vars.str == "CAB", "join operator (right)");
		macros2.assert(Vars.str .Contains("CA"), "contains literal");
		macros2.assert(Vars.str["1st"] == "C", "position fwd");
		macros2.assert(Vars.str["2ndlast"] == "A", "position bkwd");
		macros2.assert(Vars.str[(3)] == "B", "numeric index fwd");
		macros2.assert(Vars.str[(-1)] == "A", "numeric index bkwd");
		macros2.assert(Vars.str[macros1.a(1,3)] == "CB", "index array (substring)");
		macros2.assert(Vars.str["length"] == 3, "length");
		macros2.assert(v("Fear") .Contains(("e" + "ar")), "contains expression");
		macros2.assert(v("ugh") .ContainedBy("Through"), "contained by");
		macros2.assert(v("YO")["1st"] == "Y", "position fwd");
		macros2.assert(v("YO")["last"] == "O", "position bkwd");
		macros2.assert(v("PS")[(2)] == "S", "numeric index");
		macros2.assert(v("1st").AsMemberOf[("YO")] == "Y", "reverse position fwd");
		macros2.assert(v("last").AsMemberOf[("YO")] == "O", "reverse position bkwd");
		macros2.assert( v(2) .AsMemberOf[("PS")] == "S", "numeric index");
		macros2.assert(macros1.substring("cheese", 2, 4) == "hee", "macro substring");
		macros2.assert(macros1.text(4*80) == "320", "text macro");
		macros2.pass();
		yield break;
	}


	// .............
	// #3: Vars_Type_Bool

	void passage3_Init()
	{
		this.Passages["Vars_Type_Bool"] = new StoryPassage("Vars_Type_Bool", new string[]{  }, passage3_Main);
	}

	#line 1 "Vars_Type_Bool"
	IStoryThread passage3_Main()
	{
		Vars.b  = true;
		macros2.assert(Vars.b, "evaluate");
		macros2.assert(Vars.b == true, "equality (left)");
		macros2.assert(true == Vars.b, "equality (right)");
		macros2.assert(Vars.b != false, "inequality (left)");
		macros2.assert(false != Vars.b, "inequality (right)");
		macros2.assert(Vars.b && true, "and operator");
		macros2.assert(false || Vars.b, "or operator");
		macros2.assert(! (Vars.b == false), "not operator");
		Vars.b2  = Vars.b && true || false;
		macros2.assert(Vars.b2, "assignment");
		macros2.assert((4 > 2) && ((1 == 0) || Vars.b2) && (67*1+1) == 68, "nesting");
		macros2.pass();
		yield break;
	}


	// .............
	// #4: Vars_Type_Number

	void passage4_Init()
	{
		this.Passages["Vars_Type_Number"] = new StoryPassage("Vars_Type_Number", new string[]{  }, passage4_Main);
	}

	#line 1 "Vars_Type_Number"
	IStoryThread passage4_Main()
	{
		Vars.@int  = 2;
		macros2.assert(2 + Vars.@int == 4, "int addition (right)");
		macros2.assert(Vars.@int + 2 == 4, "int addition (left)");
		macros2.assert(3 - Vars.@int == 1, "int substraction (right)");
		macros2.assert(Vars.@int - 3 == -1, "int substraction (left)");
		macros2.assert(5 * Vars.@int == 10, "int multiplication (right)");
		macros2.assert(Vars.@int * 5 == 10, "int multiplication (left)");
		macros2.assert(2 / Vars.@int == 1, "int division (right)");
		macros2.assert(Vars.@int / 2 == 1, "int division (left)");
		macros2.assert(5 % Vars.@int == 1, "int modulo (right)");
		macros2.assert(Vars.@int % 2 == 0, "int modulo (left)");
		macros2.assert(3 > Vars.@int, "int greater than (right)");
		macros2.assert(Vars.@int > 1, "int greater than (left)");
		macros2.assert(2 >= Vars.@int, "int greater than or equal (right)");
		macros2.assert(Vars.@int >= 2, "int greater than or equal (left)");
		macros2.assert(1 < Vars.@int, "int less than (right)");
		macros2.assert(Vars.@int < 3, "int less than (left)");
		macros2.assert(1 <= Vars.@int, "int less than or equal (right)");
		macros2.assert(Vars.@int <= 3, "int less than or equal (left)");
		Vars.dec  = 2.1;
		macros2.assert(macros1.round(2 + Vars.dec, 1) == 4.1, "decimal addition (right)");
		macros2.assert(macros1.round(Vars.dec + 2, 1) == 4.1, "decimal addition (left)");
		macros2.assert(macros1.round(3 - Vars.dec, 1) == 0.9, "decimal substraction (right)");
		macros2.assert(macros1.round(Vars.dec - 3, 1) == -0.9, "decimal substraction (left)");
		macros2.assert(macros1.round(5 * Vars.dec, 1) == 10.5, "decimal multiplication (right)");
		macros2.assert(macros1.round(Vars.dec * 5, 1) == 10.5, "decimal multiplication (left)");
		macros2.assert(macros1.round(4 / Vars.dec, 1) < 2, "decimal division (right)");
		macros2.assert(macros1.round(Vars.dec / 2, 2) == 1.05, "decimal division (left)");
		macros2.assert(macros1.round(5 % Vars.dec, 1) == 0.8, "decimal modulo (right)");
		macros2.assert(macros1.round(Vars.dec % 2, 1) == 0.1, "decimal modulo (left)");
		macros2.assert(3 > Vars.dec, "decimal greater than (right)");
		macros2.assert(Vars.dec > 2, "decimal greater than (left)");
		macros2.assert(3 >= Vars.dec, "decimal greater than or equal (right)");
		macros2.assert(Vars.dec >= 2, "decimal greater than or equal (left)");
		macros2.assert(2 < Vars.dec, "decimal less than (right)");
		macros2.assert(Vars.dec < 3, "decimal less than (left)");
		macros2.assert(2 <= Vars.dec, "decimal less than or equal (right)");
		macros2.assert(Vars.dec <= 3, "decimal less than or equal (left)");
		macros2.assert(macros1.ceil(3.1) == 4, "macro: ceil");
		macros2.assert(macros1.floor(-3.1) == -4, "macro: floor");
		macros2.assert(macros1.num("2"+"5") == 25, "macro: num");
		macros2.assert(macros1.number("2"+"5") == 25, "macro: number");
		macros2.assert(macros1.random(1,6) .ContainedBy(macros1.range(1,6)), "macro: random");
		macros2.pass();
		yield break;
	}


	// .............
	// #5: Markup_Scope_Styles

	void passage5_Init()
	{
		this.Passages["Markup_Scope_Styles"] = new StoryPassage("Markup_Scope_Styles", new string[]{  }, passage5_Main);
	}

	#line 1 "Markup_Scope_Styles"
	IStoryThread passage5_Main()
	{
		using (ApplyStyle("italic", true)) {
			yield return text("italic");
			macros2.assertStyle("italic", true);
		}
		macros2.assertStyle("italic", false);
		using (ApplyStyle("bold", true)) {
			yield return text("bold");
			macros2.assertStyle("bold", true);
		}
		macros2.assertStyle("bold", false);
		using (ApplyStyle("del", true)) {
			yield return text("strikeout");
			macros2.assertStyle("del", true);
		}
		macros2.assertStyle("del", false);
		using (ApplyStyle("em", true)) {
			yield return text("emphasis");
			macros2.assertStyle("em", true);
		}
		macros2.assertStyle("em", false);
		using (ApplyStyle("strong", true)) {
			yield return text("strong");
			macros2.assertStyle("strong", true);
		}
		macros2.assertStyle("strong", false);
		using (ApplyStyle("sup", true)) {
			yield return text("superscript");
			macros2.assertStyle("sup", true);
		}
		macros2.assertStyle("sup", false);
		using (ApplyStyle("bulleted", 1)) {
			yield return text("Bulleted item ");
			macros2.assertStyle("bulleted", 1);
		}
		macros2.assertStyle("bulleted", false);
		using (ApplyStyle("bulleted", 2)) {
			yield return text("Indented bulleted item ");
			macros2.assertStyle("bulleted", 2);
		}
		macros2.assertStyle("bulleted", false);
		using (ApplyStyle("bulleted", 3)) {
			yield return text("Indented bulleted ite3 ");
			macros2.assertStyle("bulleted", 3);
		}
		macros2.assertStyle("bulleted", false);
		using (ApplyStyle("numbered", 1)) {
			yield return text("Numbered item ");
			macros2.assertStyle("numbered", 1);
		}
		using (ApplyStyle("numbered", 1)) {
			yield return text("Numbered item 2 ");
			macros2.assertStyle("numbered", 1);
		}
		using (ApplyStyle("numbered", 2)) {
			yield return text("Indented numbered item ");
			macros2.assertStyle("numbered", 2);
		}
		macros2.assertStyle("numbered", false);
		using (ApplyStyle("heading", 1)) {
			yield return text("Level 1 heading  ");
			macros2.assertStyle("heading", 1);
		}
		macros2.assertStyle("heading", false);
		using (ApplyStyle("heading", 3)) {
			yield return text("Level 3 heading  ");
			macros2.assertStyle("heading", 3);
		}
		macros2.assertStyle("heading", false);
		using (ApplyStyle("heading", 6)) {
			yield return text("Level 6 heading  ");
			macros2.assertStyle("heading", 6);
		}
		macros2.assertStyle("heading", false);
		macros2.pass();
		yield break;
	}


	// .............
	// #6: Markup_HTML

	void passage6_Init()
	{
		this.Passages["Markup_HTML"] = new StoryPassage("Markup_HTML", new string[]{ "NoException", }, passage6_Main);
	}

	#line 1 "Markup_HTML"
	IStoryThread passage6_Main()
	{
		yield return text("raw HTML");
		yield return lineBreak();
		macros2.pass();
		yield break;
	}


	// .............
	// #7: Markup_Verbatim

	void passage7_Init()
	{
		this.Passages["Markup_Verbatim"] = new StoryPassage("Markup_Verbatim", new string[]{  }, passage7_Main);
	}

	#line 1 "Markup_Verbatim"
	IStoryThread passage7_Main()
	{
		yield return text("(print: \"verbatim1\")");
		yield return lineBreak();
		macros2.assertText("(print: \"verbatim1\")", "verbatim");
		yield return lineBreak();
		yield return text("(print: \"verbatim2\")");
		macros2.assertText("(print: \"verbatim2\")", "verbatim");
		yield return lineBreak();
		yield return text("(print: \"verbatim3\")");
		macros2.assertText("(print: \"verbatim3\")", "verbatim");
		yield return lineBreak();
		macros2.pass();
		yield break;
	}


	// .............
	// #8: Markup_Unsupported

	void passage8_Init()
	{
		this.Passages["Markup_Unsupported"] = new StoryPassage("Markup_Unsupported", new string[]{  }, passage8_Main);
	}

	#line 1 "Markup_Unsupported"
	IStoryThread passage8_Main()
	{
		yield return text("This is right-aligned");
		yield return lineBreak();
		yield return text("This is centered");
		yield return lineBreak();
		yield return text("This is justified");
		yield return lineBreak();
		yield return text("This is left-aligned (undoes the above)");
		yield return lineBreak();
		yield return text("This has margins 3/4 left, 1/4 right");
		yield return lineBreak();
		yield return lineBreak();
		macros2.pass();
		yield break;
	}


	// .............
	// #9: Markup_LineBreaks

	void passage9_Init()
	{
		this.Passages["Markup_LineBreaks"] = new StoryPassage("Markup_LineBreaks", new string[]{  }, passage9_Main);
	}

	#line 1 "Markup_LineBreaks"
	IStoryThread passage9_Main()
	{
		yield return text("This line");
		yield return text("and this line");
		yield return text("and this line, are actually just one line.");
		macros2.assertNoOutput(typeof(Cradle.LineBreak));
		yield return lineBreak();
		macros2.pass();
		yield break;
	}


	// .............
	// #10: Markup_NakedVar

	void passage10_Init()
	{
		this.Passages["Markup_NakedVar"] = new StoryPassage("Markup_NakedVar", new string[]{  }, passage10_Main);
	}

	#line 1 "Markup_NakedVar"
	IStoryThread passage10_Main()
	{
		Vars.naked  = "I can see your butt :)";
		yield return lineBreak();
		yield return lineBreak();
		yield return text(Vars.naked);
		yield return lineBreak();
		yield return lineBreak();
		macros2.assertText("I can see your butt :)", "naked var");
		yield return lineBreak();
		macros2.pass();
		yield break;
	}


	// .............
	// #11: Markup_Scope_Hooks

	void passage11_Init()
	{
		this.Passages["Markup_Scope_Hooks"] = new StoryPassage("Markup_Scope_Hooks", new string[]{  }, passage11_Main);
	}

	#line 1 "Markup_Scope_Hooks"
	IStoryThread passage11_Main()
	{
		using (ApplyStyle("hook", hook("c0"))) {
			yield return lineBreak();
			yield return text("  ");
			using (ApplyStyle("hook", hook("c1"))) {
				yield return text("ballroom");
				macros2.assertHook("c1");
			}
			yield return lineBreak();
			yield return text("  ");
			using (ApplyStyle("hook", hook("c2"))) {
				yield return text("bright red");
				macros2.assertHook("c2");
			}
			yield return lineBreak();
			yield return text("  ");
			macros2.assertHook("c0");
			yield return lineBreak();
		}
		yield return lineBreak();
		macros2.pass();
		yield break;
	}


	// .............
	// #12: Vars_It

	void passage12_Init()
	{
		this.Passages["Vars_It"] = new StoryPassage("Vars_It", new string[]{  }, passage12_Main);
	}

	#line 1 "Vars_It"
	IStoryThread passage12_Main()
	{
		Vars.red  = "eg";
		Vars.red  = Vars.red + "g";
		macros2.assert(Vars.red == "egg" && Vars.red["length"] == 3, "it/its");
		macros2.pass();
		yield break;
	}


	// .............
	// #13: Vars_Type_Array

	void passage13_Init()
	{
		this.Passages["Vars_Type_Array"] = new StoryPassage("Vars_Type_Array", new string[]{  }, passage13_Main);
	}

	#line 1 "Vars_Type_Array"
	IStoryThread passage13_Main()
	{
		Vars.ar  = macros1.a("red","blue","green");
		macros2.assert(Vars.ar == macros1.a("red","blue","green"), "equality");
		macros2.assert(macros1.a("red","blue","green") == Vars.ar, "equality (right)");
		macros2.assert(Vars.ar != macros1.a(9,1,2), "equality (left)");
		macros2.assert(Vars.ar .Contains("green"), "contains");
		macros2.assert(v("green") .ContainedBy(Vars.ar), "contained by");
		macros2.assert(Vars.ar["1st"] == "red", "position (named)");
		macros2.assert(Vars.ar[(2+1)] == "green", "position (expression)");
		macros2.assert(Vars.ar["last"] == "green", "position (last)");
		macros2.assert(Vars.ar["3rdlast"] == "red", "position (number from last)");
		macros2.assert(Vars.ar[macros1.a(1,3)] == macros1.a("red","green"), "position array");
		macros2.assert(macros1.a(1,2) + macros1.a(1,2) == macros1.a(1,2,1,2), "join");
		macros2.assert(macros1.a(1,1,2,3,4,5) - macros1.a(1,2) == macros1.a(3,4,5), "subtract");
		macros2.assert(macros1.a(0, (HarloweSpread)macros1.a(1,2,3,4), 5) == macros1.a(0,1,2,3,4,5), "spread");
		Vars.ar["1st"]  = "yellow";
		macros2.assert(v("yellow") == Vars.ar["1st"], "set position");
		Vars.ar["2nd"] = v("brown") ;
		macros2.assert(v("brown") == Vars.ar[(2)], "put position");
		macros2.assert(macros1.count(macros1.a(5,6,5,5,8), 5) == 3, "macro: count");
		macros2.assert(macros1.range(1,14) == macros1.a(1,2,3,4,5,6,7,8,9,10,11,12,13,14) && macros1.range(2,-2) == macros1.a(-2,-1,0,1,2), "macro: range");
		macros2.assert(macros1.rotated(1, "A","B","C","D") == macros1.a("D","A","B","C") && macros1.rotated(-2, "A","B","C","D") == macros1.a("C","D","A","B"), "macro: rotated");
		macros2.assert(macros1.shuffled(1,3,4,5)["length"] == 4, "macro: shuffled");
		macros2.assert(macros1.sorted("9", "1", "Z", "a", "é") == macros1.a("1","9","a","é", "Z"), "macro: sorted");
		macros2.assert(macros1.subarray(macros1.range(21,25), 3, 5) == macros1.a(23, 24, 25), "macro: subarray");
		macros2.pass();
		yield break;
	}


	// .............
	// #14: Vars_Type_Dataset

	void passage14_Init()
	{
		this.Passages["Vars_Type_Dataset"] = new StoryPassage("Vars_Type_Dataset", new string[]{  }, passage14_Main);
	}

	#line 1 "Vars_Type_Dataset"
	IStoryThread passage14_Main()
	{
		Vars.dst  = macros1.dataset("red","blue","green");
		macros2.assert(Vars.dst["length"] == 3, "length");
		macros2.assert(Vars.dst == macros1.dataset("red","blue","green"), "equality");
		macros2.assert(Vars.dst != macros1.dataset(9,1,2), "inequality");
		macros2.assert(Vars.dst .Contains("green"), "contains");
		macros2.assert(v("green") .ContainedBy(Vars.dst), "contained by");
		macros2.assert(macros1.dataset(1,2) + macros1.dataset(1,2) == macros1.dataset(1,2), "join");
		macros2.assert(macros1.dataset(1,2,3,4,5) - macros1.dataset(1,2) == macros1.dataset(5,4,3), "subtract");
		macros2.assert(macros1.dataset(0, (HarloweSpread)macros1.a(1,2,3,4,4,4,4), 5) == macros1.dataset(0,1,2,3,4,5), "spread");
		macros2.pass();
		yield break;
	}


	// .............
	// #15: Vars_Type_Datamap

	void passage15_Init()
	{
		this.Passages["Vars_Type_Datamap"] = new StoryPassage("Vars_Type_Datamap", new string[]{  }, passage15_Main);
	}

	#line 1 "Vars_Type_Datamap"
	IStoryThread passage15_Main()
	{
		Vars.map  = macros1.datamap("juice","orange", "vessel","glass");
		macros2.assert(Vars.map == macros1.datamap("juice","orange", "vessel","glass"), "equality");
		macros2.assert(Vars.map != macros1.datamap("juice","apple", "vessel","snifter"), "inequality");
		macros2.assert(Vars.map["juice"] == "orange", "get member (name)");
		macros2.assert(Vars.map[("ve"+"ssel")] == "glass", "get member (expression)");
		Vars.map[("juice")]  = "strawberry";
		macros2.assert(Vars.map["juice"] == "strawberry", "set member (expression)");
		Vars.map["vessel"] = v("bottle") ;
		macros2.assert(Vars.map[macros1.a("juice","vessel")] == macros1.a("strawberry","bottle"), "get member (subarray)");
		macros2.assert(macros1.datamap("a",1,"b",2)-macros1.a("a") == macros1.datamap("b",2), "remove keys (subtraction)");
		macros2.assert(macros1.datanames(Vars.map) == macros1.a("juice", "vessel"), "macro: datanames");
		macros2.assert(macros1.datavalues(Vars.map) == macros1.a("strawberry","bottle"), "macro: datavalues");
		macros2.pass();
		yield break;
	}


	// .............
	// #16: Markup_Links

	void passage16_Init()
	{
		this.Passages["Markup_Links"] = new StoryPassage("Markup_Links", new string[]{  }, passage16_Main);
	}

	#line 1 "Markup_Links"
	IStoryThread passage16_Main()
	{
		yield return link("Result", "Result", null);
		macros2.assertLink("Result", "Result", "Link without text");
		yield return link("Link classic", "Result", null);
		macros2.assertLink("Link classic", "Result");
		yield return link("Link right", "Result", null);
		macros2.assertLink("Link right", "Result");
		yield return link("Link left", "Result", null);
		macros2.assertLink("Link left", "Result");
		yield return link("Link-goto macro", "Result", null);
		macros2.assertLink("Link-goto macro", "Result");
		using (ApplyStyle("hook", hook("74193cf6efc04847aa23feca0532ae1a")))
			yield return link("Link macro", null, () => enchantHook("74193cf6efc04847aa23feca0532ae1a", HarloweEnchantCommand.Replace, passage16_Fragment_0));
		macros2.assertLink("Link macro", null);
		macros2.pass();
		yield break;
	}

	#line 1 "Markup_Links#frag#0"
	IStoryThread passage16_Fragment_0()
	{
		yield break;
	}

	// .............
	// #17: Macros_Links

	void passage17_Init()
	{
		this.Passages["Macros_Links"] = new StoryPassage("Macros_Links", new string[]{  }, passage17_Main);
	}

	#line 1 "Macros_Links"
	IStoryThread passage17_Main()
	{
		yield return link("Result", "Result", null);
		yield return lineBreak();
		yield return lineBreak();
		using (ApplyStyle("hook", hook("fc51a4bb74a34cafab816a9090f88163")))
			yield return link("Click and it's gone", null, () => enchantHook("fc51a4bb74a34cafab816a9090f88163", HarloweEnchantCommand.Replace, passage17_Fragment_0));
		yield return lineBreak();
		yield return lineBreak();
		using (ApplyStyle("hook", hook("9f324c4fb091466487105e2493edbfc2")))
			yield return link("Click and it's gone", null, () => enchantHook("9f324c4fb091466487105e2493edbfc2", HarloweEnchantCommand.Replace, passage17_Fragment_1));
		yield return lineBreak();
		yield return lineBreak();
		using (ApplyStyle("hook", hook("a7867608c54948d5bae4d86acf205d5d")))
			yield return link("What comes next?", null, () => enchantHook("a7867608c54948d5bae4d86acf205d5d", HarloweEnchantCommand.Replace, passage17_Fragment_2, linkTextPrefix: true));
		
		yield return lineBreak();
		yield return lineBreak();
		yield return link("How long are you here?", null, () => enchantHook("29ab6291821c4edd89006bc35aae9cb2", HarloweEnchantCommand.Replace, passage17_Fragment_3, wrap: true));
		using (ApplyStyle("hook", hook("29ab6291821c4edd89006bc35aae9cb2"))) {}
		yield break;
	}

	#line 1 "Macros_Links#frag#0"
	IStoryThread passage17_Fragment_0()
	{
		yield return text("It's gone.");
		yield break;
	}
	#line 1 "Macros_Links#frag#1"
	IStoryThread passage17_Fragment_1()
	{
		yield return text("It's gone.");
		yield break;
	}
	#line 1 "Macros_Links#frag#2"
	IStoryThread passage17_Fragment_2()
	{
		yield return text("This does!");
		yield break;
	}
	#line 1 "Macros_Links#frag#3"
	IStoryThread passage17_Fragment_3()
	{
		if(this.PassageTime < 10000/1000f) {
			yield return text("Not very long.");
		}
		else {
			yield return text("A while.");
		}
		yield break;
	}

	// .............
	// #18: Macros_Clicks

	void passage18_Init()
	{
		this.Passages["Macros_Clicks"] = new StoryPassage("Macros_Clicks", new string[]{  }, passage18_Main);
	}

	#line 1 "Macros_Clicks"
	IStoryThread passage18_Main()
	{
		yield return text("hey, why do you keep saying hey?");
		yield return lineBreak();
		yield return lineBreak();
		yield return enchantIntoLink("hey", passage18_Fragment_0);
		yield break;
	}

	#line 1 "Macros_Clicks#frag#0"
	IStoryThread passage18_Fragment_0()
	{
		yield return lineBreak();
		yield return text("   ");
		yield return text("It's just fun, is all.");
		yield return lineBreak();
		yield break;
	}

	// .............
	// #19: HeaderFooter

	void passage19_Init()
	{
		this.Passages["HeaderFooter"] = new StoryPassage("HeaderFooter", new string[]{  }, passage19_Main);
	}

	#line 1 "HeaderFooter"
	IStoryThread passage19_Main()
	{
		yield return enchantIntoLink("hi", passage19_Fragment_0);
		yield break;
	}

	#line 1 "HeaderFooter#frag#0"
	IStoryThread passage19_Fragment_0()
	{
		yield return text("Yo!");
		yield break;
	}

	// .............
	// #20: header-test

	void passage20_Init()
	{
		this.Passages["header-test"] = new StoryPassage("header-test", new string[]{ "header", }, passage20_Main);
	}

	#line 1 "header-test"
	IStoryThread passage20_Main()
	{
		yield return text("hi!");
		yield break;
	}


	// .............
	// #21: footer-test

	void passage21_Init()
	{
		this.Passages["footer-test"] = new StoryPassage("footer-test", new string[]{ "footer", }, passage21_Main);
	}

	#line 1 "footer-test"
	IStoryThread passage21_Main()
	{
		yield return text("bye!");
		yield break;
	}


}