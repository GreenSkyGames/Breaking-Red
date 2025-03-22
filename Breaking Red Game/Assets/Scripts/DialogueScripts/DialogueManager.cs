using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
	public static DialogueManager Instance { get; private set; }

	//public GameObject DialogueParent;
	//public TMP_Text DialogueTitleText;
	//public TMP_Text DialogueBodyText;
	//public GameObject responseButtonPrefab;
	//public Transform responseButtonContainer;

	//public GameEventsManager gameEventsManager;

	private GameObject _dialogueBoxCanvas;

	[SerializeField] private TextAsset _inkJson;
	private bool _dialoguePlaying = false;

	private InkExternalFunctions _inkExternalFunctions;

	private Story _story;

	private int _currentChoiceIndex = -1;

	private void Awake()
	{
		_dialogueBoxCanvas = GameObject.FindWithTag("DialogueBox");
		//Debug.Log("Weird tag: " + _dialogueBoxCanvas.tag);

		_story = new Story(_inkJson.text); //shapedrain
		_inkExternalFunctions = new InkExternalFunctions();
		_inkExternalFunctions.bind(_story);

		closeDialogue();
	}

	private void OnDestroy()
	{
		_inkExternalFunctions.unbind(_story);
	}

	private void OnEnable()
	{
		//_dialogueBoxCanvas = GameObject.Find("GameEventsManager");
		//_dialogueBoxCanvas.dialogueEvents.onEnterDialogue += EnterDialogue;
		StartCoroutine(WaitForGameEventsManager());
	}

	private IEnumerator WaitForGameEventsManager()
	{
		while(GameEventsManager.instance == null)
		{
			yield return null;
		}
		//Debug.Log("Enable test");
		GameEventsManager.instance.dialogueEvents.onEnterDialogue += enterDialogue;
		//GameEventsManager.instance.inputEvents.onSubmitPressed += SubmitPressed; //useful if using unity input system
		GameEventsManager.instance.dialogueEvents.onUpdateChoiceIndex += updateChoiceIndex;
	}

	private void OnDisable()
	{
		//Debug.Log("Disable test");
		GameEventsManager.instance.dialogueEvents.onEnterDialogue -= enterDialogue;
		//GameEventsManager.instance.inputEvents.onSubmitPressed -= SubmitPressed;
		GameEventsManager.instance.dialogueEvents.onUpdateChoiceIndex -= updateChoiceIndex;
	}

	private void updateChoiceIndex(int choiceIndex)
	{
		this._currentChoiceIndex = choiceIndex;
	}

	//This should perhaps be bound to a Next button
	//Works as a button
	public void submitPressed()
	{
		if(!_dialoguePlaying)
		{
			return;
		}

		continueOrExitStory();
	}

	private void enterDialogue(string knotName)
	{
		Debug.Log("Entering dialogue for knot name: " + knotName);

		if(_dialoguePlaying)
		{
			return;
		}
		_dialoguePlaying = true;

		if (GameEventsManager.instance == null || GameEventsManager.instance.dialogueEvents == null)
		{
			Debug.LogError("GameEventsManager or dialogueEvents is null!");
			return;
		}

		//GameEventsManager.instance.dialogueEvents.DialogueStarted();  //Doesn't work, see DialoguePanelUI for reason why

		_dialogueBoxCanvas.SetActive(true);
		//Debug.Log("Weird tag: " + _dialogueBoxCanvas.tag); //Better be tagged dialoguebox
		//Debug.Log("Name is " + Name);

		if(!knotName.Equals(""))
		{
			_story.ChoosePathString(knotName);

			string name = _story.variablesState["Name"].ToString();

			//Debug.Log("Name is " + name);
		}
		else
		{
			Debug.LogWarning("Knot name was the empty string when entering dialogue.");
		}

		continueOrExitStory();
	}

	//This is currently being done by button click instead of event management
	public void continueOrExitStory()
	{

		//make a choice, if applicable
		if(_story.currentChoices.Count > 0 && _currentChoiceIndex != -1)
		{
			_story.ChooseChoiceIndex(_currentChoiceIndex);
			_currentChoiceIndex = -1;
		}
		if(_story.canContinue)
		{
			string dialogueLine = _story.Continue();

			//Acquire the name of the speaker
			name = _story.variablesState["Name"].ToString();

			Debug.Log("Name is " + name);

			Debug.Log(dialogueLine);

			//Handle case where there's an empty line of dialogue
			while(isLineBlank(dialogueLine) && _story.canContinue)
			{
				dialogueLine = _story.Continue();
			}

			//handle case where the last line of dialogue is blank
			if(isLineBlank(dialogueLine) && !_story.canContinue)
			{
				exitDialogue();
			}
			else
			{
				GameEventsManager.instance.dialogueEvents.displayDialogue(dialogueLine, _story.currentChoices, name);
			}
		}
		else if (_story.currentChoices.Count == 0)
		{
			Debug.Log("End of choice path");
			//StartCoroutine(ExitDialogue());
			exitDialogue();
		}
	}

	private void exitDialogue()
	{
		//Makes them end on a different frame
		//yield return null; //this is to stop a race condition that does not exist in this format
		GameEventsManager.instance.dialogueEvents.dialogueFinished();

		//GameEventsManager.instance.playerEvents.EnablePlayerMovement();

		Debug.Log("Exiting dialogue.");

		_dialoguePlaying = false;

		_story.ResetState();
	}

	private bool isLineBlank(string dialogueLine)
	{
		return dialogueLine.Trim().Equals("") || dialogueLine.Trim().Equals("\n");
	}


	//If this is used by the canvas button, it will not inherit currentNPC
	//This means the NPC talking will retain the last state given
	public void closeDialogue()
	{
		//_dialogueBoxCanvas = GameObject.FindWithTag("DialogueBox");
		//Debug.Log("Weird tag " + _dialogueBoxCanvas.tag);
		_dialoguePlaying = false;
		_dialogueBoxCanvas.SetActive(false);
		return;
	}

	public void openDialogue()
	{
		//_dialogueBoxCanvas = GameObject.FindWithTag("DialogueBox");
		_dialogueBoxCanvas.SetActive(true);
		//return;
	}
}
