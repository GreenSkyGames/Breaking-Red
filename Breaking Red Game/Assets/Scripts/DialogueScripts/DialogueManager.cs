using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
	public static DialogueManager Instance { get; private set; }

	public GameObject DialogueParent;
	public TMP_Text DialogueTitleText;
	public TMP_Text DialogueBodyText;
	public GameObject responseButtonPrefab;
	public Transform responseButtonContainer;

	//public GameEventsManager gameEventsManager;

	private GameObject temp;

	[SerializeField] private TextAsset inkJson;
	private bool dialoguePlaying = false;

	private InkExternalFunctions inkExternalFunctions;

	private Story story;

	private int currentChoiceIndex = -1;

	private void Awake()
	{
		temp = GameObject.FindWithTag("DialogueBox");
		//Debug.Log("Weird tag: " + temp.tag);

		story = new Story(inkJson.text); //shapedrain
		inkExternalFunctions = new InkExternalFunctions();
		inkExternalFunctions.Bind(story);

		closeDialogue();
	}

	private void OnDestroy()
	{
		inkExternalFunctions.Unbind(story);
	}

	private void OnEnable()
	{
		//temp = GameObject.Find("GameEventsManager");
		//temp.dialogueEvents.onEnterDialogue += EnterDialogue;
		StartCoroutine(WaitForGameEventsManager());
	}

	private IEnumerator WaitForGameEventsManager()
	{
		while(GameEventsManager.instance == null)
		{
			yield return null;
		}
		//Debug.Log("Enable test");
		GameEventsManager.instance.dialogueEvents.onEnterDialogue += EnterDialogue;
		//GameEventsManager.instance.inputEvents.onSubmitPressed += SubmitPressed; //useful if using unity input system
		GameEventsManager.instance.dialogueEvents.onUpdateChoiceIndex += UpdateChoiceIndex;
	}

	private void OnDisable()
	{
		//Debug.Log("Disable test");
		GameEventsManager.instance.dialogueEvents.onEnterDialogue -= EnterDialogue;
		//GameEventsManager.instance.inputEvents.onSubmitPressed -= SubmitPressed;
		GameEventsManager.instance.dialogueEvents.onUpdateChoiceIndex -= UpdateChoiceIndex;
	}

	private void UpdateChoiceIndex(int choiceIndex)
	{
		this.currentChoiceIndex = choiceIndex;
	}

	//This should perhaps be bound to a Next button
	//Works as a button
	public void SubmitPressed()
	{
		if(!dialoguePlaying)
		{
			return;
		}

		ContinueOrExitStory();
	}

	private void EnterDialogue(string knotName)
	{
		Debug.Log("Entering dialogue for knot name: " + knotName);

		if(dialoguePlaying)
		{
			return;
		}
		dialoguePlaying = true;

		if (GameEventsManager.instance == null || GameEventsManager.instance.dialogueEvents == null)
		{
			Debug.LogError("GameEventsManager or dialogueEvents is null!");
			return;
		}

		//GameEventsManager.instance.dialogueEvents.DialogueStarted();  //Doesn't work, see DialoguePanelUI for reason why

		temp.SetActive(true);
		//Debug.Log("Weird tag: " + temp.tag); //Better be tagged dialoguebox
		//Debug.Log("Name is " + Name);

		if(!knotName.Equals(""))
		{
			story.ChoosePathString(knotName);

			string name = story.variablesState["Name"].ToString();

			//Debug.Log("Name is " + name);
		}
		else
		{
			Debug.LogWarning("Knot name was the empty string when entering dialogue.");
		}

		ContinueOrExitStory();
	}

	//This is currently being done by button click instead of event management
	public void ContinueOrExitStory()
	{

		//make a choice, if applicable
		if(story.currentChoices.Count > 0 && currentChoiceIndex != -1)
		{
			story.ChooseChoiceIndex(currentChoiceIndex);
			currentChoiceIndex = -1;
		}
		if(story.canContinue)
		{
			string dialogueLine = story.Continue();

			//Acquire the name of the speaker
			name = story.variablesState["Name"].ToString();

			Debug.Log("Name is " + name);

			Debug.Log(dialogueLine);

			//Handle case where there's an empty line of dialogue
			while(IsLineBlank(dialogueLine) && story.canContinue)
			{
				dialogueLine = story.Continue();
			}

			//handle case where the last line of dialogue is blank
			if(IsLineBlank(dialogueLine) && !story.canContinue)
			{
				ExitDialogue();
			}
			else
			{
				GameEventsManager.instance.dialogueEvents.DisplayDialogue(dialogueLine, story.currentChoices, name);
			}
		}
		else if (story.currentChoices.Count == 0)
		{
			Debug.Log("End of choice path");
			//StartCoroutine(ExitDialogue());
			ExitDialogue();
		}
	}

	private void ExitDialogue()
	{
		//Makes them end on a different frame
		//yield return null; //this is to stop a race condition that does not exist in this format
		GameEventsManager.instance.dialogueEvents.DialogueFinished();

		//GameEventsManager.instance.playerEvents.EnablePlayerMovement();

		Debug.Log("Exiting dialogue.");

		dialoguePlaying = false;

		story.ResetState();
	}

	private bool IsLineBlank(string dialogueLine)
	{
		return dialogueLine.Trim().Equals("") || dialogueLine.Trim().Equals("\n");
	}


	//If this is used by the canvas button, it will not inherit currentNPC
	//This means the NPC talking will retain the last state given
	public void closeDialogue()
	{
		//temp = GameObject.FindWithTag("DialogueBox");
		//Debug.Log("Weird tag " + temp.tag);
		dialoguePlaying = false;
		temp.SetActive(false);
		return;
	}

	public void openDialogue()
	{
		//temp = GameObject.FindWithTag("DialogueBox");
		temp.SetActive(true);
		//return;
	}
}
