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

	private GameObject currentNPC;
	private GameObject temp;

	public void StartDialogue (NPCDialogue dialogue)
	{
		Debug.Log("Starting convo with " + dialogue.name);

		nameText.text = dialogue.name;

		if(dialogue.name == "The Wolf")
		{
			currentNPC = GameObject.FindWithTag("TheWolf");
		}

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
			closeDialogue();
		}
		string sentence = sentences.Dequeue();
		dialogueText.text = sentence;
	}

	void EndDialogue()
	{
		Debug.Log("End of convo.");
		currentNPC.GetComponent<NPCManager>().ChangeState(EnemyState.Attacking);
		//gameObject.SetActive(false);
	}

	void Start()
	{
		sentences = new Queue<string>();
	}
	
	public void closeDialogue()
	{
		temp = GameObject.FindWithTag("DialogueBox");
		Debug.Log("Weird tag " + temp.tag);
		//Debug.Log("What I do have " + nameText.text);
		temp.SetActive(false);
		return;
	}

	public void openDialogue()
	{
		gameObject.SetActive(true);
	}
}
