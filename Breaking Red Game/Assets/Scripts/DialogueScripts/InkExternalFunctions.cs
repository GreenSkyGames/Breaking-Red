using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;

public class InkExternalFunctions : MonoBehaviour
{
	public GameObject temp;

	public void Bind(Story story)
	{
		//Each ink function needs a separate binding.
		story.BindExternalFunction("StartWolfHostility", (bool activate) => StartWolfHostility(activate));
		story.BindExternalFunction("StopWolfHostility", (bool activate) => StopWolfHostility(activate));
		story.BindExternalFunction("StartPurpleTorchEnemyHostility", (bool activate) => StartPurpleTorchEnemyHostility(activate));
		story.BindExternalFunction("StopPurpleTorchEnemyHostility", (bool activate) => StopPurpleTorchEnemyHostility(activate));
		story.BindExternalFunction("StartBearHostility", (bool activate) => StartBearHostility(activate));
		story.BindExternalFunction("StopBearHostility", (bool activate) => StopBearHostility(activate));

	}

	public void Unbind(Story story)
	{
		story.UnbindExternalFunction("StartWolfHostility");
		story.UnbindExternalFunction("StopWolfHostility");
		story.UnbindExternalFunction("StartPurpleTorchEnemyHostility");
		story.UnbindExternalFunction("StopPurpleTorchEnemyHostility");
		story.UnbindExternalFunction("StartBearHostility");
		story.UnbindExternalFunction("StopBearHostility");

	}

	//These are mostly for turning NPCs hostile or not.
	//Calling it as an event turns all of them hostile at once.
    private void StartWolfHostility(bool activate)
	{
		//Debug.Log("Miracle");
		//GameEventsManager.instance.dialogueEvents.StartHostility();
		temp = GameObject.FindWithTag("TheWolf");
		temp.GetComponent<NPCManager>().onHostility();
	}

    private void StopWolfHostility(bool activate)
	{
		//Debug.Log("Miracle2");
		temp = GameObject.FindWithTag("TheWolf");
		temp.GetComponent<NPCManager>().offHostility();
	}

    private void StartPurpleTorchEnemyHostility(bool activate)
	{
		temp = GameObject.FindWithTag("PurpleTorchEnemy");
		temp.GetComponent<NPCManager>().onHostility();
	}

    private void StopPurpleTorchEnemyHostility(bool activate)
	{
		temp = GameObject.FindWithTag("PurpleTorchEnemy");
		temp.GetComponent<NPCManager>().offHostility();
	}

    private void StartBearHostility(bool activate)
	{
		Debug.Log("Miracle");
		temp = GameObject.FindWithTag("TheBear");
		temp.GetComponent<NPCManager>().onHostility();
	}

    private void StopBearHostility(bool activate)
	{
		Debug.Log("Miracle2");
		temp = GameObject.FindWithTag("TheBear");
		temp.GetComponent<NPCManager>().offHostility();
	}
}
