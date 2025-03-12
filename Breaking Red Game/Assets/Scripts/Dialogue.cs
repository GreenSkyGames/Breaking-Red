using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Scriptable Objects/Dialogue")]
public class Dialogue : ScriptableObject
{
	//Allows creation of Dialogue objects to create scripts
    public NPCDialogue RootNode;
}
