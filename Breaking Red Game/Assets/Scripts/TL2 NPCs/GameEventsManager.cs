using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;

/* Name: Todd Carter
 * Role: Team Lead 2 -- Software Architect
 *	This is the script for the GameEventsManager.
 *	The GameEventsManager primarily serves to create a new set of DialogueEvents.
 *	This functionality could be expanded to include other events.
 *
*/
public class GameEventsManager : MonoBehaviour
{
	public static GameEventsManager instance { get; private set; }

    public DialogueEvents dialogueEvents;

	//Using Awake, a new instance for dialogueEvents is created.
	private void Awake()
	{
		if(instance != null)
		{
			Debug.LogError("More than one Game Events Manager in that scene.");
		}
		instance = this;

		dialogueEvents = new DialogueEvents();
	}
}
