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
		closeDialogue();
	}

	public void StartDialogue(string title, NPCDialogue node)
	{
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
		//Debug.Log("Stuff is " + response.responseText);

		//This will be an if-else chain for all ten NPCs
		//This is how to make things happen during dialogue.
		//Certain lines and titles can be used like a key to create a certain result.
		if(title == "The Wolf")
		{
			temp = GameObject.FindWithTag("TheWolf");
			//Debug.Log("the worhfla: " + temp.tag);

			if(response.responseText == "*Attack*")
			{
				//temp.GetComponent<NPCManager>().ChangeState(EnemyState.Attacking);
				temp.GetComponent<NPCManager>().onHostility();
			}
			if(response.responseText == "I'm watching you, Wolf.")
			{
				temp.GetComponent<NPCManager>().offHostility();
			}
		}
		else if(title == "The Bear")
		{
			//stuff for The Bear goes here
		}
		else if(title == "The Hiker")
		{
			//stuff for The Hiker goes here
		}

		if(!response.nextNode.IsLastNode())
		{
			StartDialogue(title, response.nextNode);
		}
		else
		{
			closeDialogue();
		}
	}

	//If this is used by the canvas button, it will not inherit currentNPC
	//This means the NPC talking will retain the last state given
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
