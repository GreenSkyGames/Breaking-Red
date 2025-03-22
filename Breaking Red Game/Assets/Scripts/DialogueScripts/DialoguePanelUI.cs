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
	[SerializeField] private TMP_Text _dialogueText;
	[SerializeField] private TMP_Text _nameText;
	[SerializeField] private DialogueChoiceButton[] _choiceButtons;

	private void Awake()
	{
		//contentParent.SetActive(false);
		resetPanel();
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
		GameEventsManager.instance.dialogueEvents.onDialogueStarted += dialogueStarted;
		GameEventsManager.instance.dialogueEvents.onDialogueFinished += dialogueFinished;
		GameEventsManager.instance.dialogueEvents.onDisplayDialogue += displayDialogue;
	}

	private void OnDisable()
	{
		if(GameEventsManager.instance != null && GameEventsManager.instance.dialogueEvents != null)
		{
			GameEventsManager.instance.dialogueEvents.onDialogueStarted -= dialogueStarted;
			GameEventsManager.instance.dialogueEvents.onDialogueFinished -= dialogueFinished;
			GameEventsManager.instance.dialogueEvents.onDisplayDialogue -= displayDialogue;
		}
	}

	private void dialogueStarted()
	{
		contentParent.SetActive(true);
		Debug.Log("Weird tag ");
		//temp = GameObject.FindWithTag("DialogueBox");
		//temp.SetActive(true);
		//ResetPanel();
	}
	private void dialogueFinished()
	{
		contentParent = GameObject.FindWithTag("DialogueBox");
		//Debug.Log("Weird tag " + temp.tag);
		contentParent.SetActive(false);
		resetPanel();
	}

	private void displayDialogue(string dialogueLine, List<Choice> dialogueChoices, string name)
	{
		_dialogueText.text = dialogueLine;
		_nameText.text = name;  //display the name

		if(dialogueChoices.Count > _choiceButtons.Length)
		{
			Debug.LogError("More dialogue choices("
				+ dialogueChoices.Count + ") came through than are supported ("
				+ _choiceButtons.Length + ").");
		}

		foreach (DialogueChoiceButton choiceButton in _choiceButtons)
		{
			choiceButton.gameObject.SetActive(false);
		}

		int choiceButtonIndex = dialogueChoices.Count - 1;

		for(int inkChoiceIndex = 0; inkChoiceIndex < dialogueChoices.Count; inkChoiceIndex++)
		{
			Choice dialogueChoice = dialogueChoices[inkChoiceIndex];
			DialogueChoiceButton choiceButton = _choiceButtons[choiceButtonIndex];

			choiceButton.gameObject.SetActive(true);
			choiceButton.setChoiceText(dialogueChoice.text);
			choiceButton.setChoiceIndex(inkChoiceIndex);

			if(inkChoiceIndex == 0)
			{
				choiceButton.selectButton();
				GameEventsManager.instance.dialogueEvents.updateChoiceIndex(0);
			}

			choiceButtonIndex--;
		}
	}

	private void resetPanel()
	{
		_dialogueText.text = "";
	}
}
