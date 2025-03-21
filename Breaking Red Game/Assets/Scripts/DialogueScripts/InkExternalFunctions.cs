using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;

public class InkExternalFunctions : MonoBehaviour
{
	public GameObject temp;

	public void bind(Story story)
	{
		//Each ink function needs a separate binding.
		story.BindExternalFunction("startWolfHostility", (bool activate) => startWolfHostility(activate));
		story.BindExternalFunction("stopWolfHostility", (bool activate) => stopWolfHostility(activate));
		story.BindExternalFunction("startPurpleTorchEnemyHostility", (bool activate) => startPurpleTorchEnemyHostility(activate));
		story.BindExternalFunction("stopPurpleTorchEnemyHostility", (bool activate) => stopPurpleTorchEnemyHostility(activate));
		story.BindExternalFunction("startBearHostility", (bool activate) => startBearHostility(activate));
		story.BindExternalFunction("stopBearHostility", (bool activate) => stopBearHostility(activate));

	}

	public void unbind(Story story)
	{
		story.UnbindExternalFunction("startWolfHostility");
		story.UnbindExternalFunction("stopWolfHostility");
		story.UnbindExternalFunction("startPurpleTorchEnemyHostility");
		story.UnbindExternalFunction("stopPurpleTorchEnemyHostility");
		story.UnbindExternalFunction("startBearHostility");
		story.UnbindExternalFunction("stopBearHostility");

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

    private void stopWolfHostility(bool activate)
	{
		//Debug.Log("Miracle2");
		temp = GameObject.FindWithTag("TheWolf");
		temp.GetComponent<NPCManager>().offHostility();
	}

    private void startPurpleTorchEnemyHostility(bool activate)
	{
		temp = GameObject.FindWithTag("PurpleTorchEnemy");
		temp.GetComponent<NPCManager>().onHostility();
	}

    private void stopPurpleTorchEnemyHostility(bool activate)
	{
		temp = GameObject.FindWithTag("PurpleTorchEnemy");
		temp.GetComponent<NPCManager>().offHostility();
	}

    private void startBearHostility(bool activate)
	{
		Debug.Log("Miracle");
		temp = GameObject.FindWithTag("TheBear");
		temp.GetComponent<NPCManager>().onHostility();
	}

    private void stopBearHostility(bool activate)
	{
		Debug.Log("Miracle2");
		temp = GameObject.FindWithTag("TheBear");
		temp.GetComponent<NPCManager>().offHostility();
	}
}
