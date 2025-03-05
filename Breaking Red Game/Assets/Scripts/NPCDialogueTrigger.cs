using UnityEngine;

public class NPCDialogueTrigger : MonoBehaviour
{
    public NPCDialogue dialogue;

	public void TriggerDialogue()
	{
		FindFirstObjectByType<DialogueManager>().StartDialogue(dialogue);
	}

}
