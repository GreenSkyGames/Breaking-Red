using UnityEngine;

public class NPCDialogueTrigger : MonoBehaviour
{
	//actor

    //public NPCDialogue Dialogue; //Brackseys
	public Dialogue Dialogue; //Duls
	public string Name;
	//public Dialogue DeathScript;

	public void TriggerDialogue()
	{
		//FindFirstObjectByType<DialogueManager>().StartDialogue(Dialogue);
		DialogueManager.Instance.StartDialogue(Name, Dialogue.RootNode);
	}

}
