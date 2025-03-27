using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;

/*
 * This is the script the connects the NPC with their specific dialogue section in the ink json.
 *
 * the Knot Name refers to the specific NPC, and is designed to be the same as their tag.
 *
*/
public class NPCDialogueTrigger : MonoBehaviour
{
	//actor

    //public NPCDialogue Dialogue; //Brackseys
	//public Dialogue Dialogue; //Duls
	//public string Name;

	[SerializeField] private string _dialogueKnotName;

	//This method assigns the dialogue section to the GameEventsManager using dialogueEvents.
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
