using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;

/* Name: Todd Carter
 * Role: Team Lead 2 -- Software Architect
 *
 *	This is the script that handles dialogue events.
 *	This produces a unique class object that contains the data
 *	such as strings for the dialogue to be displayed.
 *	
 *	Being events, each of these must be subscribed by other scripts.
 *
*/
public class DialogueEvents
{
    public event Action<string> onEnterDialogue;
	
	//On entering dialogue, the knotName is used to identify
	//the location on the .ink document.
	public void enterDialogue(string knotName)
	{		
		if (onEnterDialogue != null)
		{
			//Debug.Log("2nd Test " + knotName);
			onEnterDialogue(knotName);
		}
	}

	public event Action onDialogueStarted;

	//This activates the dialogue system.
	public void dialogueStarted()
	{
		//Debug.Log("1st test");
		if(onDialogueStarted != null)
		{
			//Debug.Log("2nd test");
			onDialogueStarted.Invoke();
		}
	}

	public event Action onDialogueFinished;

	//This ends the dialogue system.
	public void dialogueFinished()
	{
		if(onDialogueFinished != null)
		{
			onDialogueFinished();
		}
	}

	public event Action<string, List<Choice>, string> onDisplayDialogue;

	//This sends the variables out to be displayed.
	public void displayDialogue(string dialogueLine, List<Choice> dialogueChoices, string name)
	{
		if(onDisplayDialogue != null)
		{
			onDisplayDialogue(dialogueLine, dialogueChoices, name);
		}
	}

	public event Action<int> onUpdateChoiceIndex;

	//This updates the choice index number for the next choice location in the .ink.
	public void updateChoiceIndex(int choiceIndex)
	{
		if(onUpdateChoiceIndex != null)
		{
			onUpdateChoiceIndex(choiceIndex);
		}
	}

	public event Action onStartHostility;

	//This event activates the hostility in an NPC.
	public void startHostility()
	{
		Debug.Log("startHostility in dialogue events test: ");
		if(onStartHostility != null)
		{
			onStartHostility();
		}
	}

	public event Action onStopHostility;

	//This event deactivates the hostility in an NPC.
	public void stopHostility()
	{
		if(onStopHostility != null)
		{
			onStopHostility();
		}
	}

	public event Action onGatherClue;

	//This event adds the NPC tag to the list in dialogue manager.
	public void gatherClue()
	{
		if(onGatherClue != null)
		{
			onGatherClue();
		}
	}
}
