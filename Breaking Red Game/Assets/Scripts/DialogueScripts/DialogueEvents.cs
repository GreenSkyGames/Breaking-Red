using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;

public class DialogueEvents
{
    public event Action<string> onEnterDialogue;
	
	public void EnterDialogue(string knotName)
	{
		//onEnterDialogue?.Invoke(knotName);

		//Debug.Log("Test " + onEnterDialogue);
		//Debug.Log("1st Test " + knotName);
		
		if (onEnterDialogue != null)
		{
			//Debug.Log("2nd Test " + knotName);
			onEnterDialogue(knotName);
		}
	}

	public event Action onDialogueStarted;

	public void DialogueStarted()
	{
		//Debug.Log("1st test");
		if(onDialogueStarted != null)
		{
			//Debug.Log("2nd test"); //This is not working for whatever flipass reason
			onDialogueStarted.Invoke();
		}
	}

	public event Action onDialogueFinished;

	public void DialogueFinished()
	{
		if(onDialogueFinished != null)
		{
			onDialogueFinished();
		}
	}

	public event Action<string, List<Choice>, string> onDisplayDialogue;

	public void DisplayDialogue(string dialogueLine, List<Choice> dialogueChoices, string name)
	{
		if(onDisplayDialogue != null)
		{
			onDisplayDialogue(dialogueLine, dialogueChoices, name);
		}
	}

	public event Action<int> onUpdateChoiceIndex;

	public void UpdateChoiceIndex(int choiceIndex)
	{
		if(onUpdateChoiceIndex != null)
		{
			onUpdateChoiceIndex(choiceIndex);
		}
	}

	public event Action startHostility;

	public void StartHostility()
	{
		if(startHostility != null)
		{
			startHostility();
		}
	}

	public event Action stopHostility;

	public void StopHostility()
	{
		if(stopHostility != null)
		{
			stopHostility();
		}
	}
}
