using UnityEngine;

[System.Serializable]
public class PlayerDialogue
{
	//dialogueresponse
	//This is the text for the player's dialogue responses.
	//This does not require monobehaviour.
    public string responseText;
	public NPCDialogue nextNode;
}
