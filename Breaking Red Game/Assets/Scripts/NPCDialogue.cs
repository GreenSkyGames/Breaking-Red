using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class NPCDialogue
{
	//dialoguenode
	//public string name;
	public string dialogueText;
	public List<PlayerDialogue> responses;

	internal bool IsLastNode()
	{
		return responses.Count <= 0;
	}

}
