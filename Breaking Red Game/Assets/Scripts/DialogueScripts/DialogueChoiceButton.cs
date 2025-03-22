using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class DialogueChoiceButton : MonoBehaviour, ISelectHandler
{
    [SerializeField] private Button button;

	[SerializeField] private TextMeshProUGUI choiceText;

	private int choiceIndex = -1;

	public void setChoiceText(string choiceTextString)
	{
		choiceText.text = choiceTextString;
	}

	public void setChoiceIndex(int choiceIndex)
	{
		this.choiceIndex = choiceIndex;
	}

	public void selectButton()
	{
		button.Select();
	}

	public void OnSelect(BaseEventData eventData)
	{
		//dialogueEvents here is a variable under GameEventsManager
		GameEventsManager.instance.dialogueEvents.updateChoiceIndex(choiceIndex);
	}
}
