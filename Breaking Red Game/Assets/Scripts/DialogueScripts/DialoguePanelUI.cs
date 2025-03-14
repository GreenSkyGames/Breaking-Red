using UnityEngine;
using TMPro;
using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;

public class DialoguePanelUI : MonoBehaviour
{
    private GameObject contentParent;
    private GameObject temp;
	[SerializeField] private TMP_Text dialogueText;
	[SerializeField] private TMP_Text nameText;
	[SerializeField] private DialogueChoiceButton[] choiceButtons;

	private void Awake()
	{
		//contentParent.SetActive(false);
		ResetPanel();
	}

	private void OnEnable()
	{
		StartCoroutine(WaitForGameEventsManager());
	}

	private IEnumerator WaitForGameEventsManager()
	{
		while(GameEventsManager.instance == null || GameEventsManager.instance.dialogueEvents == null)
		{
			yield return null;
		}
		//Debug.Log("Dialogue enable test");
		//These are only subscribed to GameEventsManager when the dang thing is active
		//Since it's usually not active, these won't work.
		//So to use DialogueStarted to activate it, it already has to be activated -_-
		GameEventsManager.instance.dialogueEvents.onDialogueStarted += DialogueStarted;
		GameEventsManager.instance.dialogueEvents.onDialogueFinished += DialogueFinished;
		GameEventsManager.instance.dialogueEvents.onDisplayDialogue += DisplayDialogue;
	}

	private void OnDisable()
	{
		if(GameEventsManager.instance != null && GameEventsManager.instance.dialogueEvents != null)
		{
			GameEventsManager.instance.dialogueEvents.onDialogueStarted -= DialogueStarted;
			GameEventsManager.instance.dialogueEvents.onDialogueFinished -= DialogueFinished;
			GameEventsManager.instance.dialogueEvents.onDisplayDialogue -= DisplayDialogue;
		}
	}

	private void DialogueStarted()
	{
		contentParent.SetActive(true);
		Debug.Log("Weird tag ");
		//temp = GameObject.FindWithTag("DialogueBox");
		//temp.SetActive(true);
		//ResetPanel();
	}
	private void DialogueFinished()
	{
		contentParent = GameObject.FindWithTag("DialogueBox");
		//Debug.Log("Weird tag " + temp.tag);
		contentParent.SetActive(false);
		ResetPanel();
	}

	private void DisplayDialogue(string dialogueLine, List<Choice> dialogueChoices, string name)
	{
		dialogueText.text = dialogueLine;
		nameText.text = name;  //display the name

		if(dialogueChoices.Count > choiceButtons.Length)
		{
			Debug.LogError("More dialogue choices("
				+ dialogueChoices.Count + ") came through than are supported ("
				+ choiceButtons.Length + ").");
		}

		foreach (DialogueChoiceButton choiceButton in choiceButtons)
		{
			choiceButton.gameObject.SetActive(false);
		}

		int choiceButtonIndex = dialogueChoices.Count - 1;

		for(int inkChoiceIndex = 0; inkChoiceIndex < dialogueChoices.Count; inkChoiceIndex++)
		{
			Choice dialogueChoice = dialogueChoices[inkChoiceIndex];
			DialogueChoiceButton choiceButton = choiceButtons[choiceButtonIndex];

			choiceButton.gameObject.SetActive(true);
			choiceButton.SetChoiceText(dialogueChoice.text);
			choiceButton.SetChoiceIndex(inkChoiceIndex);

			if(inkChoiceIndex == 0)
			{
				choiceButton.SelectButton();
				GameEventsManager.instance.dialogueEvents.UpdateChoiceIndex(0);
			}

			choiceButtonIndex--;
		}
	}

	private void ResetPanel()
	{
		dialogueText.text = "";
	}
}
