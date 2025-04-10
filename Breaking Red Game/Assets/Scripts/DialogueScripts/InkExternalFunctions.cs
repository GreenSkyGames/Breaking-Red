using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;

/* Name: Todd Carter
 * Role: Team Lead 2 -- Software Architect
 *
 *	This is the script file that contains the functions which are then bound to the ink json file.
 * These enable the usage of these methods through the ink file after they have been bound.
 *
 * This means these are the functions that will let NPCs have behaviors as a result of dialogue choices.
 *
*/
public class InkExternalFunctions : MonoBehaviour
{
	public GameObject temp;

	//Function to bind the methods.
	public void bind(Story story)
	{
		//Each ink function needs a separate binding.
		story.BindExternalFunction("startWolfHostility", (bool activate) => startWolfHostility(activate));
		story.BindExternalFunction("stopWolfHostility", (bool activate) => stopWolfHostility(activate));
		story.BindExternalFunction("startPurpleTorchEnemyHostility", (bool activate) => startPurpleTorchEnemyHostility(activate));
		story.BindExternalFunction("stopPurpleTorchEnemyHostility", (bool activate) => stopPurpleTorchEnemyHostility(activate));
		story.BindExternalFunction("startBearHostility", (bool activate) => startBearHostility(activate));
		story.BindExternalFunction("stopBearHostility", (bool activate) => stopBearHostility(activate));
		story.BindExternalFunction("startWizardHostility", (bool activate) => startWizardHostility(activate));
		story.BindExternalFunction("stopWizardHostility", (bool activate) => stopWizardHostility(activate));
		story.BindExternalFunction("startHunterHostility", (bool activate) => startHunterHostility(activate));
		story.BindExternalFunction("stopHunterHostility", (bool activate) => stopHunterHostility(activate));
		story.BindExternalFunction("startAxmanHostility", (bool activate) => startAxmanHostility(activate));
		story.BindExternalFunction("stopAxmanHostility", (bool activate) => stopAxmanHostility(activate));
		story.BindExternalFunction("addHunterClue", (bool activate) => addHunterClue(activate));
		story.BindExternalFunction("addHikerClue", (bool activate) => addHikerClue(activate));
		story.BindExternalFunction("addHippieClue", (bool activate) => addHippieClue(activate));
		story.BindExternalFunction("addWizardClue", (bool activate) => addWizardClue(activate));
		story.BindExternalFunction("addBearClue", (bool activate) => addBearClue(activate));
		story.BindExternalFunction("addFishClue", (bool activate) => addFishClue(activate));
		story.BindExternalFunction("addAxmanClue", (bool activate) => addAxmanClue(activate));
		story.BindExternalFunction("addCatClue", (bool activate) => addCatClue(activate));
		story.BindExternalFunction("checkAxmanClues", (bool activate) => checkAxmanClues(activate));
		story.BindExternalFunction("checkCatCollectibles", (bool activate) => checkCatCollectibles(activate));

	}

	//Function to unbind the methods.
	public void unbind(Story story)
	{
		story.UnbindExternalFunction("startWolfHostility");
		story.UnbindExternalFunction("stopWolfHostility");
		story.UnbindExternalFunction("startPurpleTorchEnemyHostility");
		story.UnbindExternalFunction("stopPurpleTorchEnemyHostility");
		story.UnbindExternalFunction("startBearHostility");
		story.UnbindExternalFunction("stopBearHostility");
		story.UnbindExternalFunction("startWizardHostility");
		story.UnbindExternalFunction("stopWizardHostility");
		story.UnbindExternalFunction("startHunterHostility");
		story.UnbindExternalFunction("stopHunterHostility");
		story.UnbindExternalFunction("startAxmanHostility");
		story.UnbindExternalFunction("stopAxmanHostility");
		story.UnbindExternalFunction("addHunterClue");
		story.UnbindExternalFunction("addHikerClue");
		story.UnbindExternalFunction("addHippieClue");
		story.UnbindExternalFunction("addWizardClue");
		story.UnbindExternalFunction("addBearClue");
		story.UnbindExternalFunction("addFishClue");
		story.UnbindExternalFunction("addCatClue");
		story.UnbindExternalFunction("addAxmanClue");
		story.UnbindExternalFunction("checkAxmanClues");
		story.UnbindExternalFunction("checkCatCollectibles");

	}



//These switch NPC hostility on and off:

	//Calling it as an event turns all of them hostile at once.
    private void startWolfHostility(bool activate)
	{
		//Debug.Log("Miracle");
		//GameEventsManager.instance.dialogueEvents.startHostility(); //This line requires testing
		temp = GameObject.FindWithTag("TheWolf");
		temp.GetComponent<NPCManager>().onHostility();
	}

	//Turn off The Wolf hostility
    private void stopWolfHostility(bool activate)
	{
		//Debug.Log("Miracle2");
		temp = GameObject.FindWithTag("TheWolf");
		Debug.Log("Miracle for "+ temp.tag);
		temp.GetComponent<NPCManager>().offHostility();
	}

	//Turn on the Purple Torch Enemy hostility
    private void startPurpleTorchEnemyHostility(bool activate)
	{
		temp = GameObject.FindWithTag("PurpleTorchEnemy");
		temp.GetComponent<NPCManager>().onHostility();
	}

	//Turn off the Purple Torch Enemy hostility
    private void stopPurpleTorchEnemyHostility(bool activate)
	{
		temp = GameObject.FindWithTag("PurpleTorchEnemy");
		temp.GetComponent<NPCManager>().offHostility();
	}

	//Turn on The Bear hostility
    private void startBearHostility(bool activate)
	{
		temp = GameObject.FindWithTag("TheBear");
		Debug.Log("Miracle for "+ temp.tag);
		temp.GetComponent<NPCManager>().onHostility();
	}

	//Turn off The Bear hostility
    private void stopBearHostility(bool activate)
	{
		Debug.Log("Miracle2");
		temp = GameObject.FindWithTag("TheBear");
		temp.GetComponent<NPCManager>().offHostility();
	}

	//Turn on The Wizard hostility
    private void startWizardHostility(bool activate)
	{
		temp = GameObject.FindWithTag("TheWizard");
		Debug.Log("Miracle for "+ temp.tag);
		temp.GetComponent<NPCManager>().onHostility();
	}

	//Turn off The Wizard hostility
    private void stopWizardHostility(bool activate)
	{
		Debug.Log("Miracle2");
		temp = GameObject.FindWithTag("TheWizard");
		temp.GetComponent<NPCManager>().offHostility();
	}

	//Turn on The Hunter hostility
    private void startHunterHostility(bool activate)
	{
		temp = GameObject.FindWithTag("TheHunter");
		Debug.Log("Miracle for "+ temp.tag);
		temp.GetComponent<NPCManager>().onHostility();
	}

	//Turn off The Hunter hostility
    private void stopHunterHostility(bool activate)
	{
		//Debug.Log("Miracle2");
		temp = GameObject.FindWithTag("TheHunter");
		temp.GetComponent<NPCManager>().offHostility();
	}

	//Turn on The Axman hostility
    private void startAxmanHostility(bool activate)
	{
		//Debug.Log("Miracle");
		temp = GameObject.FindWithTag("TheAxman");
		Debug.Log("Miracle for "+ temp.tag);
		temp.GetComponent<NPCManager>().onHostility();
	}

	//Turn off The Axman hostility
    private void stopAxmanHostility(bool activate)
	{
		//Debug.Log("Miracle2");
		temp = GameObject.FindWithTag("TheAxman");
		temp.GetComponent<NPCManager>().offHostility();
	}



//The functions to add clues to the list:

	//Add 'TheHunter' to clue list:
    private void addHunterClue(bool activate)
	{
		temp = GameObject.FindWithTag("TheHunter");
		Debug.Log("Ink Clue Test" + temp.tag);
		temp.GetComponent<NPCManager>().gatherClue();
	}

	//Add 'TheHiker' to clue list:
    private void addHikerClue(bool activate)
	{
		temp = GameObject.FindWithTag("TheHiker");
		Debug.Log("Ink Clue Test" + temp.tag);
		temp.GetComponent<NPCManager>().gatherClue();
	}

	//Add 'TheHippie' to clue list:
    private void addHippieClue(bool activate)
	{
		temp = GameObject.FindWithTag("TheHippie");
		Debug.Log("Ink Clue Test" + temp.tag);
		temp.GetComponent<NPCManager>().gatherClue();
	}

	//Add 'TheWizard' to clue list:
    private void addWizardClue(bool activate)
	{
		temp = GameObject.FindWithTag("TheWizard");
		Debug.Log("Ink Clue Test" + temp.tag);
		temp.GetComponent<NPCManager>().gatherClue();
	}

	//Add 'TheBear' to clue list:
    private void addBearClue(bool activate)
	{
		temp = GameObject.FindWithTag("TheBear");
		Debug.Log("Ink Clue Test" + temp.tag);
		temp.GetComponent<NPCManager>().gatherClue();
	}

	//Add 'TheFish' to clue list:
    private void addFishClue(bool activate)
	{
		temp = GameObject.FindWithTag("TheFish");
		Debug.Log("Ink Clue Test" + temp.tag);
		temp.GetComponent<NPCManager>().gatherClue();
	}

	//Add 'TheAxman' to clue list:
    private void addAxmanClue(bool activate)
	{
		temp = GameObject.FindWithTag("TheAxman");
		Debug.Log("Ink Clue Test" + temp.tag);
		temp.GetComponent<NPCManager>().gatherClue();
	}

	//Add 'TheCat' to clue list:
    private void addCatClue(bool activate)
	{
		temp = GameObject.FindWithTag("TheCat");
		Debug.Log("Ink Clue Test" + temp.tag);
		temp.GetComponent<NPCManager>().gatherClue();
	}



//The function for the Axman to check the clue list

    private bool checkAxmanClues(bool activate)
	{
		temp = GameObject.FindWithTag("TheAxman");
		Debug.Log("Ink Check Clue Test" + temp.tag);
		return temp.GetComponent<NPCManager>().checkClues();
	}



//The function for the Cat to check the player's inventory

    private bool checkCatCollectibles(bool activate)
	{
		temp = GameObject.FindWithTag("TheCat");
		Debug.Log("Ink Item Check Test for: " + temp.tag);
		return temp.GetComponent<NPCManager>().checkCollectibles();
	}
}
