using UnityEngine;

public class DialogueManager : MonoBehaviour
{
	public void closeDialogue()
	{
		gameObject.SetActive(false);
	}

	public void openDialogue()
	{
		gameObject.SetActive(true);
	}
}
