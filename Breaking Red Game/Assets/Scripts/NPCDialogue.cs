using UnityEngine;

[System.Serializable]
public class NPCDialogue
{
	public string name;

	[TextArea(3, 10)]
	public string[] sentences;

}
