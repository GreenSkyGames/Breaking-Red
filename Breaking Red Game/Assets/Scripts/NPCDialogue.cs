using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class NPCDialogue
{
	//dialoguenode
	//This is the text for the NPC dialogue.
	//This does not require monobehaviour.
	public string dialogueText;
	public List<PlayerDialogue> responses;

	internal bool IsLastNode()
	{
		return responses.Count <= 0;
	}
}
