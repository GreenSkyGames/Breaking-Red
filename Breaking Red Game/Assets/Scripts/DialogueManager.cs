using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
	public TMP_Text nameText;
	public TMP_Text dialogueText;

	private Queue<string> sentences;

	public void StartDialogue (NPCDialogue dialogue)
	{
		Debug.Log("Starting convo with " + dialogue.name);

		nameText.text = dialogue.name;

		sentences.Clear();

		foreach(string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence()
	{
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}
		string sentence = sentences.Dequeue();
		dialogueText.text = sentence;
	}

	void EndDialogue()
	{
		Debug.Log("End of convo.");
	}

	void Start()
	{
		sentences = new Queue<string>();
	}
	
	public void closeDialogue()
	{
		gameObject.SetActive(false);
	}

	public void openDialogue()
	{
		gameObject.SetActive(true);
	}
}
