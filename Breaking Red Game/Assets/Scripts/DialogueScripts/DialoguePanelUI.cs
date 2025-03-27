using UnityEngine;
using TMPro;
using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;

/*
 *	This is the script that handles what is displayed on the dialogue box,
 *	and how the dialogue choice buttons are displayed.
 *
*/
public class DialoguePanelUI : MonoBehaviour
{
    private GameObject contentParent;
    private GameObject temp;
	[SerializeField] private TMP_Text _dialogueText;
	[SerializeField] private TMP_Text _nameText;
	[SerializeField] private DialogueChoiceButton[] _choiceButtons;

	//On Awake, the panel is reset, just in case.
	private void Awake()
	{
		//contentParent.SetActive(false);
		resetPanel();
	}

	//On enable, a coroutine is started for timing.
	private void OnEnable()
	{
		StartCoroutine(WaitForGameEventsManager());
	}

	//The coroutine makes sure the methods are only subscribed to the event manager after the manager is active.
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

	//Disabling the panel unsubscribes the methods from the manager.
	private void OnDisable()
	{
		if(GameEventsManager.instance != null && GameEventsManager.instance.dialogueEvents != null)
		{
			GameEventsManager.instance.dialogueEvents.onDialogueStarted -= dialogueStarted;
			GameEventsManager.instance.dialogueEvents.onDialogueFinished -= dialogueFinished;
			GameEventsManager.instance.dialogueEvents.onDisplayDialogue -= displayDialogue;
		}
	}

	//A method to activate the dialogue box canvas.
	private void dialogueStarted()
	{
		contentParent.SetActive(true);
		//Debug.Log("Weird tag ");
		//temp = GameObject.FindWithTag("DialogueBox");
		//temp.SetActive(true);
		//ResetPanel();
	}

	//A method to deactivate the dialogue box when finished.
	private void dialogueFinished()
	{
		contentParent = GameObject.FindWithTag("DialogueBox");
		//Debug.Log("Weird tag " + temp.tag);
		contentParent.SetActive(false);
		resetPanel();
	}

	//This is the method that displays the dialogue on the box.
	//It receives the string for dialogue, the list of choices, and the string for name
	// and incorporates them into the dialogue panel text box.
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

	//Method to clear the text on the panel.
	private void resetPanel()
	{
		_dialogueText.text = "";
	}
}
