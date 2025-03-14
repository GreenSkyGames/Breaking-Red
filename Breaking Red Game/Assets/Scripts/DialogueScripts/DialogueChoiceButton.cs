using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class DialogueChoiceButton : MonoBehaviour, ISelectHandler
{
    [SerializeField] private Button button;

	[SerializeField] private TextMeshProUGUI choiceText;

	private int choiceIndex = -1;

	public void SetChoiceText(string choiceTextString)
	{
		choiceText.text = choiceTextString;
	}

	public void SetChoiceIndex(int choiceIndex)
	{
		this.choiceIndex = choiceIndex;
	}

	public void SelectButton()
	{
		button.Select();
	}

	public void OnSelect(BaseEventData eventData)
	{
		GameEventsManager.instance.dialogueEvents.UpdateChoiceIndex(choiceIndex);
	}
}
