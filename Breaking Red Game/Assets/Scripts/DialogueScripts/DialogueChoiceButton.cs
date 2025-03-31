using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

/* Name: Todd Carter
 * Role: Team Lead 2 -- Software Architect
 *
 *	This is the script for the dialogue choice buttons.
 *	
 *  These methods set the text in accordance the choices from the
 *	choice list and the choice index.
*/
public class DialogueChoiceButton : MonoBehaviour, ISelectHandler
{
    [SerializeField] private Button button;

	[SerializeField] private TextMeshProUGUI choiceText;

	private int choiceIndex = -1;


	//This sets the text on the button
	public void setChoiceText(string choiceTextString)
	{
		choiceText.text = choiceTextString;
	}

	//This finds the choice index from the index.
	public void setChoiceIndex(int choiceIndex)
	{
		this.choiceIndex = choiceIndex;
	}

	//This sets the action for selecting the button.
	public void selectButton()
	{
        button.Select();
		
		AudioManager.instance.Play("ClickSound"); // Play the button click
	}

	//This determines what happens when the button selects,
	//in this case, advancing the choice index.
	public void OnSelect(BaseEventData eventData)
	{
		//dialogueEvents here is a variable under GameEventsManager
		GameEventsManager.instance.dialogueEvents.updateChoiceIndex(choiceIndex);
	}
}
