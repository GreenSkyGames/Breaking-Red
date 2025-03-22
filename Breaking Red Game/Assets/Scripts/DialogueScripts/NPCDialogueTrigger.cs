using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;

public class NPCDialogueTrigger : MonoBehaviour
{
	//actor

    //public NPCDialogue Dialogue; //Brackseys
	//public Dialogue Dialogue; //Duls
	//public string Name;

	[SerializeField] private string _dialogueKnotName;

	public void triggerDialogue()
	{
		//FindFirstObjectByType<DialogueManager>().StartDialogue(Dialogue);
		//DialogueManager.Instance.StartDialogue(Name, Dialogue.RootNode);
		if (!_dialogueKnotName.Equals(""))
		{
			GameEventsManager.instance.dialogueEvents.enterDialogue(_dialogueKnotName);
		}
		else
		{
			//do what you were doing previously or something else
		}
	}

}
