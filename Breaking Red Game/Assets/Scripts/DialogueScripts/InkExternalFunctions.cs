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

	}

	//Turn on The Hunter hostility
    private void startHunterHostility(bool activate)
	{
		Debug.Log("Miracle");
		temp = GameObject.FindWithTag("TheHunter");
		temp.GetComponent<NPCManager>().onHostility();
	}

	//Turn off The Hunter hostility
    private void stopHunterHostility(bool activate)
	{
		Debug.Log("Miracle2");
		temp = GameObject.FindWithTag("TheHunter");
		temp.GetComponent<NPCManager>().offHostility();
	}

	//These are mostly for turning NPCs hostile or not.
	//Calling it as an event turns all of them hostile at once.
    private void startWolfHostility(bool activate)
	{
		//Debug.Log("Miracle");
		//GameEventsManager.instance.dialogueEvents.StartHostility();
		temp = GameObject.FindWithTag("TheWolf");
		temp.GetComponent<NPCManager>().onHostility();
	}

	//Turn off The Wolf hostility
    private void stopWolfHostility(bool activate)
	{
		//Debug.Log("Miracle2");
		temp = GameObject.FindWithTag("TheWolf");
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
		Debug.Log("Miracle");
		temp = GameObject.FindWithTag("TheBear");
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
		Debug.Log("Miracle");
		temp = GameObject.FindWithTag("TheWizard");
		temp.GetComponent<NPCManager>().onHostility();
	}

	//Turn off The Wizard hostility
    private void stopWizardHostility(bool activate)
	{
		Debug.Log("Miracle2");
		temp = GameObject.FindWithTag("TheWizard");
		temp.GetComponent<NPCManager>().offHostility();
	}
}
