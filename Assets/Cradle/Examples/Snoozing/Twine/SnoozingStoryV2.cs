﻿/*
------------------------------------------------
Generated by Cradle 2.0.0.0 on 6/3/2016 12:36:58 AM
https://github.com/daterre/Cradle

Original file: SnoozingStoryV2.html
Story format: Sugar
------------------------------------------------
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cradle;
using IStoryThread = System.Collections.Generic.IEnumerable<Cradle.StoryOutput>;
using Cradle.StoryFormats.Sugar;

public partial class @SnoozingStoryV2: Cradle.StoryFormats.Sugar.SugarStory
{
	#region Variables
	// ---------------

	public class VarDefs: RuntimeVars
	{
		public VarDefs()
		{
			VarDef("wait", () => this.@wait, val => this.@wait = val);
			VarDef("again", () => this.@again, val => this.@again = val);
			VarDef("dream", () => this.@dream, val => this.@dream = val);
			VarDef("anxiety", () => this.@anxiety, val => this.@anxiety = val);
			VarDef("click", () => this.@click, val => this.@click = val);
		}

		public StoryVar @wait;
		public StoryVar @again;
		public StoryVar @dream;
		public StoryVar @anxiety;
		public StoryVar @click;
	}

	public new VarDefs Vars
	{
		get { return (VarDefs) base.Vars; }
	}

	// ---------------
	#endregion

	#region Initialization
	// ---------------

	public readonly UnityTwineTestMacros macros1;

	@SnoozingStoryV2()
	{
		this.StartPassage = "alarm";

		base.Vars = new VarDefs() { Story = this, StrictMode = false };

		macros1 = new UnityTwineTestMacros() { Story = this };
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
	}

	// ---------------
	#endregion

	// .............
	// #1: alarm

	void passage1_Init()
	{
		this.Passages[@"alarm"] = new StoryPassage(@"alarm", new string[]{  }, passage1_Main);
	}

	#line 1 "alarm"
	IStoryThread passage1_Main()
	{
		Vars.wait = 1;
		yield return lineBreak();
		yield return text(@"...");
		yield return lineBreak();
		yield return lineBreak();
		if (! Vars.again)
		{
			yield return lineBreak();
			Vars.wait = 2;
			yield return lineBreak();
			yield return text(@"? ?");
			yield return lineBreak();
			yield return lineBreak();
		}
		yield return lineBreak();
		Vars.wait = 1.5;
		yield return lineBreak();
		yield return text(@"My alarm clock.");
		yield return lineBreak();
		Vars.wait = 1.5;
		yield return lineBreak();
		yield return link(@"Time to get up.", @"getUp", null);
		yield return lineBreak();
		yield return lineBreak();
		if (visited("her", "relationship", "work", "street", "sea"))
		{
			yield return lineBreak();
			yield return link(@"snooze=", @"alone", null);
			yield return lineBreak();
		}
		else
		{
			yield return lineBreak();
			yield return link(@"snooze=", @"snooze", null);
			yield return lineBreak();
			Vars.again=true;
			yield return lineBreak();
		}
		yield return lineBreak();
		yield break;
	}


	// .............
	// #2: getUp

	void passage2_Init()
	{
		this.Passages[@"getUp"] = new StoryPassage(@"getUp", new string[]{  }, passage2_Main);
	}

	#line 1 "getUp"
	IStoryThread passage2_Main()
	{
		yield return text(either("Here it goes again", "Forgot to get coffee", "I need air", "Train leaves at 7:00"));
		yield return lineBreak();
		yield return lineBreak();
		yield return lineBreak();
		Vars.wait = 4;
		yield return lineBreak();
		yield return text(@"THE END");
		yield return lineBreak();
		yield break;
	}


	// .............
	// #3: snooze

	void passage3_Init()
	{
		this.Passages[@"snooze"] = new StoryPassage(@"snooze", new string[]{  }, passage3_Main);
	}

	#line 1 "snooze"
	IStoryThread passage3_Main()
	{
		Vars.wait = 1;
		yield return lineBreak();
		yield return text(either("I can't handle this right now.", "Keep my eyes closed."));
		yield return lineBreak();
		yield return link(@"dream=Dream", @"dreaming", null);
		yield return lineBreak();
		yield return link(@"anxiety=Anxiety", @"anxiety", null);
		yield return lineBreak();
		yield return link(@"her=Her", @"her", null);
		yield return lineBreak();
		yield break;
	}


	// .............
	// #4: dreaming

	void passage4_Init()
	{
		this.Passages[@"dreaming"] = new StoryPassage(@"dreaming", new string[]{  }, passage4_Main);
	}

	#line 1 "dreaming"
	IStoryThread passage4_Main()
	{
		if (Vars.dream == "sea")
		{
			yield return lineBreak();
			yield return text(@"	");
			Vars.dream = "street";
			yield return lineBreak();
		}
		else if (Vars.dream == "street")
		{
			yield return lineBreak();
			yield return text(@"	");
			Vars.dream = "sea";
			yield return lineBreak();
		}
		else
		{
			yield return lineBreak();
			yield return text(@"	");
			Vars.dream = either("sea", "street");
			yield return lineBreak();
		}
		yield return lineBreak();
		yield return abort(goToPassage: Vars.dream);
		yield return lineBreak();
		yield break;
	}


	// .............
	// #5: anxiety

	void passage5_Init()
	{
		this.Passages[@"anxiety"] = new StoryPassage(@"anxiety", new string[]{  }, passage5_Main);
	}

	#line 1 "anxiety"
	IStoryThread passage5_Main()
	{
		if (Vars.anxiety == "work")
		{
			yield return lineBreak();
			yield return text(@"	");
			Vars.anxiety = "relationship";
			yield return lineBreak();
		}
		else if (Vars.anxiety == "relationship")
		{
			yield return lineBreak();
			yield return text(@"	");
			Vars.anxiety = "work";
			yield return lineBreak();
		}
		else
		{
			yield return lineBreak();
			yield return text(@"	");
			Vars.anxiety = either("work", "relationship");
			yield return lineBreak();
		}
		yield return lineBreak();
		yield return abort(goToPassage: Vars.anxiety);
		yield return lineBreak();
		yield break;
	}


	// .............
	// #6: her

	void passage6_Init()
	{
		this.Passages[@"her"] = new StoryPassage(@"her", new string[]{  }, passage6_Main);
	}

	#line 1 "her"
	IStoryThread passage6_Main()
	{
		yield return text(@"why");
		yield return lineBreak();
		yield return text(@"can't");
		yield return lineBreak();
		yield return text(@"i");
		yield return lineBreak();
		yield return text(@"see");
		yield return lineBreak();
		yield return text(@"her");
		yield return lineBreak();
		yield return link(@"continue=", @"alarm", null);
		yield return lineBreak();
		yield break;
	}


	// .............
	// #7: relationship

	void passage7_Init()
	{
		this.Passages[@"relationship"] = new StoryPassage(@"relationship", new string[]{  }, passage7_Main);
	}

	#line 1 "relationship"
	IStoryThread passage7_Main()
	{
		yield return text(@"She knows it. ");
		Vars.click = true;
		yield return lineBreak();
		yield return text(@"She looks at you,");
		yield return lineBreak();
		yield return text(@"and she knows it.");
		yield return lineBreak();
		yield return text(@"Pretends not to.");
		yield return lineBreak();
		yield return text(@"She smiles.");
		yield return lineBreak();
		yield return text(@"She's kind.");
		yield return lineBreak();
		yield return text(@"You're not.");
		yield return lineBreak();
		yield return text(@"You're a cheat.");
		yield return lineBreak();
		yield return text(@"A little cheat,");
		yield return lineBreak();
		Vars.click = false;
		yield return lineBreak();
		yield return link(@"continue=", @"alarm", null);
		yield return lineBreak();
		yield break;
	}


	// .............
	// #8: work

	void passage8_Init()
	{
		this.Passages[@"work"] = new StoryPassage(@"work", new string[]{  }, passage8_Main);
	}

	#line 1 "work"
	IStoryThread passage8_Main()
	{
		yield return text(@"Two days till the meeting.");
		yield return lineBreak();
		yield return text(@"Two days.");
		yield return lineBreak();
		yield return text(@"Everyone is going to");
		yield return lineBreak();
		yield return text(@"hate me");
		yield return lineBreak();
		yield return text(@"Think I'm a clown");
		yield return lineBreak();
		yield return text(@"I can't");
		yield return lineBreak();
		yield return text(@"take it");
		yield return lineBreak();
		yield return lineBreak();
		yield return link(@"continue=", @"alarm", null);
		yield return lineBreak();
		yield break;
	}


	// .............
	// #9: street

	void passage9_Init()
	{
		this.Passages[@"street"] = new StoryPassage(@"street", new string[]{  }, passage9_Main);
	}

	#line 1 "street"
	IStoryThread passage9_Main()
	{
		Vars.wait = 1;
		yield return lineBreak();
		yield return text(@"I'm late.");
		yield return lineBreak();
		yield return lineBreak();
		yield return text(@"Where are my keys?");
		yield return lineBreak();
		yield return text(@"Shit.");
		yield return lineBreak();
		yield return link(@"continue=", @"street2", null);
		yield return lineBreak();
		yield break;
	}


	// .............
	// #10: street2

	void passage10_Init()
	{
		this.Passages[@"street2"] = new StoryPassage(@"street2", new string[]{  }, passage10_Main);
	}

	#line 1 "street2"
	IStoryThread passage10_Main()
	{
		Vars.wait = 1;
		yield return lineBreak();
		yield return text(@"My bag is empty.");
		yield return lineBreak();
		yield return text(@"...");
		yield return lineBreak();
		yield return text(@"No, it's not.");
		yield return lineBreak();
		yield return text(@"There's a half-eaten sandwich.");
		yield return lineBreak();
		yield return text(@"It's hers.");
		yield return lineBreak();
		yield return link(@"continue=", @"street3", null);
		yield return lineBreak();
		yield break;
	}


	// .............
	// #11: street3

	void passage11_Init()
	{
		this.Passages[@"street3"] = new StoryPassage(@"street3", new string[]{  }, passage11_Main);
	}

	#line 1 "street3"
	IStoryThread passage11_Main()
	{
		Vars.wait = 1;
		yield return lineBreak();
		yield return text(@"I should throw it out.");
		yield return lineBreak();
		yield return text(@"But maybe she'll want it?");
		yield return lineBreak();
		yield return text(@"Her lips touched it.");
		yield return lineBreak();
		yield return text(@"Lips");
		yield return lineBreak();
		yield return link(@"continue=", @"alarm", null);
		yield return lineBreak();
		yield break;
	}


	// .............
	// #12: sea

	void passage12_Init()
	{
		this.Passages[@"sea"] = new StoryPassage(@"sea", new string[]{  }, passage12_Main);
	}

	#line 1 "sea"
	IStoryThread passage12_Main()
	{
		Vars.click = true;
		yield return lineBreak();
		yield return text(@"i can breath");
		yield return lineBreak();
		yield return lineBreak();
		yield return text(@"i can fly");
		yield return lineBreak();
		yield return lineBreak();
		yield return text(@"i can't see the sky.");
		yield return lineBreak();
		yield return lineBreak();
		yield return text(@"but it's warm");
		yield return lineBreak();
		yield return lineBreak();
		yield return text(@"go deep");
		yield return lineBreak();
		yield return lineBreak();
		yield return text(@"always sleep");
		yield return lineBreak();
		Vars.click = false;
		yield return lineBreak();
		yield return link(@"continue=", @"alarm", null);
		yield return lineBreak();
		yield break;
	}


	// .............
	// #13: alone

	void passage13_Init()
	{
		this.Passages[@"alone"] = new StoryPassage(@"alone", new string[]{  }, passage13_Main);
	}

	#line 1 "alone"
	IStoryThread passage13_Main()
	{
		yield return text(@"Just an empty body, ");
		Vars.wait = 2;
		yield return lineBreak();
		yield return text(@"alone. ");
		Vars.wait = 4;
		yield return lineBreak();
		yield return lineBreak();
		yield return link(@"Must get up.", @"getUp", null);
		yield return lineBreak();
		yield break;
	}


}