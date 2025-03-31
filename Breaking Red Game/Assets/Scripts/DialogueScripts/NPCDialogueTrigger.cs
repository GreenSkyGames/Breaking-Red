using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;

/* Name: Todd Carter
 * Role: Team Lead 2 -- Software Architect
 *
 *	This is the script the connects the NPC with their specific dialogue section in the ink json.
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
    [SerializeField] private AudioClip dialogueClip;  // The audio clip corresponding to the NPC's dialogue.

    private void Start()
    {
        // Add the NPC's dialogue clip to the DialogueSound manager using the NPC's name (_dialogueKnotName)
        DialogueSound.instance.AddNpcDialogueClip(_dialogueKnotName, dialogueClip);
    }

    //This method assigns the dialogue section to the GameEventsManager using dialogueEvents.
    public void triggerDialogue()
	{
		//FindFirstObjectByType<DialogueManager>().StartDialogue(Dialogue);
		//DialogueManager.Instance.StartDialogue(Name, Dialogue.RootNode);
		if (!_dialogueKnotName.Equals(""))
		{
			GameEventsManager.instance.dialogueEvents.enterDialogue(_dialogueKnotName);

            // Play the NPC's dialogue audio
            DialogueSound.instance.PlayDialogueSound(_dialogueKnotName);
        }
		else
		{
			//do what you were doing previously or something else
		}
	}

}
