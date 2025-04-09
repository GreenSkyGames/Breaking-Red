using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;

/* Name: Todd Carter
 * Role: Team Lead 2 -- Software Architect
 *
 *	This is the script for the dialogue manager, which is an object
 *	that enables the flow of data between the NPC and the player using
 *	the .ink json file.
 *	
*/
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

	private List<string> cluesGathered = new List<string>();
	public List<string> killList = new List<string>();

	//On Awake, the diaogue box, the ink json file, and the inkExternal functions
	//are all found.
	//
	//Further, the inkExternalFunctions are bound, which allows the .ink file to use functions.
	//
	//Lastly, the dialogue box is closed, just in case it is open. 
	private void Awake()
	{
		_dialogueBoxCanvas = GameObject.FindWithTag("DialogueBox");
		//Debug.Log("Weird tag: " + _dialogueBoxCanvas.tag);

		_story = new Story(_inkJson.text); //shapedrain
		_inkExternalFunctions = new InkExternalFunctions();
		_inkExternalFunctions.bind(_story);

		//closeDialogue();
	}

	//When the dialogue manager is destroyed, the functions are unbound.
	private void OnDestroy()
	{
		_inkExternalFunctions.unbind(_story);
	}

	//When the manager is enabled, a coroutine is started for the sake of timing.
	private void OnEnable()
	{
		//_dialogueBoxCanvas = GameObject.Find("GameEventsManager");
		//_dialogueBoxCanvas.dialogueEvents.onEnterDialogue += EnterDialogue;
		StartCoroutine(WaitForGameEventsManager());
	}

	//Waiting for the GameEventsManager ensures the correct functions are available.
	//This then subscribes the functions to the event manager.
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

	//When disabled, the functions are unsubscribed from the manager.
	private void OnDisable()
	{
		//Debug.Log("Disable test");
		GameEventsManager.instance.dialogueEvents.onEnterDialogue -= enterDialogue;
		//GameEventsManager.instance.inputEvents.onSubmitPressed -= SubmitPressed;
		GameEventsManager.instance.dialogueEvents.onUpdateChoiceIndex -= updateChoiceIndex;
	}

	//A simple method to update the choice index for displaying the correct choice.
	private void updateChoiceIndex(int choiceIndex)
	{
		this._currentChoiceIndex = choiceIndex;
	}

	//submitPressed advances the dialoge using continueOrExitStory()
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

	//This starts the dialogue using the knot name to locate the dialogue in the ink json.
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


		//This is where the dialogue box is intially activated.
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

	//This advances the dialogue to the next step of the index.
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

	//A method to safely exit the dialogue sequence.
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

	//Simple method to check if a line of dialogue is empty.
	private bool isLineBlank(string dialogueLine)
	{
		return dialogueLine.Trim().Equals("") || dialogueLine.Trim().Equals("\n");
	}

	//A reliable means to close the dialogue box.
	//If this is used by the canvas button, it will not inherit currentNPC
	//This means the NPC talking will retain the last state given
	public void closeDialogue()
	{
		//_dialogueBoxCanvas = GameObject.FindWithTag("DialogueBox");
		//Debug.Log("Weird tag " + _dialogueBoxCanvas.tag);
		_dialoguePlaying = false;
		_dialogueBoxCanvas.SetActive(false);

        AudioManager.instance.Play("ClickSound"); // Play the button click

        return;
	}

	//A reliable means to open the dialogue box.
	public void openDialogue()
	{
		//_dialogueBoxCanvas = GameObject.FindWithTag("DialogueBox");
		_dialogueBoxCanvas.SetActive(true);
		//return;
	}

	//Function to add enemy tag to the clue list
	public void addClue(string clueTag)
	{
		if (!cluesGathered.Contains(clueTag))
		{
			cluesGathered.Add(clueTag);
			Debug.Log("Added clue: " + clueTag);
		}
	}

    public bool checkClues()
    {
		if(cluesGathered.Count >= 5)
		{
			Debug.Log("Collected clues: " + string.Join(", ", cluesGathered));
			Debug.Log("Check clues success");
			return true;
		}
		else
		{
			Debug.Log("Check clues failure");
			return false;
		}
    }

	//Function to add enemy tag to the clue list
	//
	//This runs twice, but the if() catches duplicates.
	public void addKill(string killTag)
	{
		Debug.Log("Test All kills: " + string.Join(", ", killList));

		if (!killList.Contains(killTag))
		{
			Debug.Log("All kills: " + string.Join(", ", killList));
			killList.Add(killTag);
		}
	}
}
