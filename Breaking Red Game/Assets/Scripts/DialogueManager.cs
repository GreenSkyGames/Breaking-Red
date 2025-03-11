using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
	public static DialogueManager Instance { get; private set; }

	public GameObject DialogueParent;
	public TMP_Text DialogueTitleText;
	public TMP_Text DialogueBodyText;
	public GameObject responseButtonPrefab;
	public Transform responseButtonContainer;

	private GameObject temp;

	private void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
		//HideDialogue();
		closeDialogue();
	}

	public void StartDialogue(string title, NPCDialogue node)
	{
		//Display the ui (not needed)
		//ShowDialogue();
		//openDialogue();

		//Set name and body text
		DialogueTitleText.text = title; //This gives name of the NPC
		DialogueBodyText.text = node.dialogueText;

		//Remove any existing response buttons
		foreach(Transform child in responseButtonContainer)
		{
			Destroy(child.gameObject);
		}

		foreach(PlayerDialogue response in node.responses)
		{
			//This creates the buttons for each response
			GameObject buttonObj = Instantiate(responseButtonPrefab, responseButtonContainer);
			buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = response.responseText;

			//Set button to trigger SelectResponse
			buttonObj.GetComponent<Button>().onClick.AddListener(() => SelectResponse(response, title));
		}
	}

	//Stuff will happen in SelectResponse
	public void SelectResponse(PlayerDialogue response, string title)
	{
		//Maybe make button do something here?
		Debug.Log("Stuff is " + response.responseText);

		//This will be an if-else chain for all ten NPCs
		if(title == "The Wolf")
		{
			temp = GameObject.FindWithTag("TheWolf");
			//Debug.Log("the worhfla: " + temp.tag);

			//This is how to make things happen during dialogue.
			//Certain lines and titles can be used like a key to create a certain result.
			if(response.responseText == "*Attack*")
			{
				//temp.GetComponent<NPCManager>().ChangeState(EnemyState.Attacking);
				temp.GetComponent<NPCManager>().enemyAttack();
			}
			if(response.responseText == "I'm watching you, Wolf.")
			{
				temp.GetComponent<NPCManager>().switchHostility();
			}
		}
		else if(title == "The Bear")
		{
			//stuff for The Bear goes here
		}

		if(!response.nextNode.IsLastNode())
		{
			StartDialogue(title, response.nextNode);
		}
		else
		{
			//HideDialogue();
			closeDialogue();
		}
	}

	public void HideDialogue()
	{
		DialogueParent.SetActive(false);
	}

	public void ShowDialogue()
	{
		DialogueParent.SetActive(true);
	}
	
	//If this is used by the canvas button, it will not inherit currentNPC
	//This means the NPC talking will retain the last state given 
	//(The last state is usually Dialogue, since the box just opened)
	public void closeDialogue()
	{
		temp = GameObject.FindWithTag("DialogueBox");
		//Debug.Log("Weird tag " + temp.tag);
		temp.SetActive(false);
		//return;
	}

	public void openDialogue()
	{
		temp = GameObject.FindWithTag("DialogueBox");
		temp.SetActive(true);
		//return;
	}
}
