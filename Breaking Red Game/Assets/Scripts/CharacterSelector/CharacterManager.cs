/*
 * Name:  Mark Eldridge
 * Role:  Main Character Customization
 * This file contains the definition for the CharacterManager class.
 * This class manages the character selection and scene transitions.
 * It inherits from MonoBehaviour.
 */

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class CharacterManager : MonoBehaviour
{
    public CharacterDatabase characterDB;
    public SpriteRenderer characterSprite;

    private int _selectedOption = 0;

    /*
     * This function is called before the first frame update.
     * It initializes the selected character option from PlayerPrefs.
     */
    void Start()
    {
        if (!PlayerPrefs.HasKey("selectedOption"))
        {
            _selectedOption = 0;
        }
        else
        {
            load();
        }
        updateCharacter(_selectedOption);
    }

    /*
     * This function handles the logic for selecting the next character option.
     * It increments the selected option, loops back to the first option if necessary,
     * updates the character, and saves the selection.
     */
    public void nextOption()
    {
        // Play the button click sound
        AudioManager.instance.Play("ClickSound");

        _selectedOption++;

        if (_selectedOption >= characterDB.CharacterCount)
        {
            _selectedOption = 0;
        }

        updateCharacter(_selectedOption);
        save();
    }

    /*
     * This function handles the logic for selecting the previous character option.
     * It decrements the selected option, loops back to the last option if necessary,
     * updates the character, and saves the selection.
     */
    public void backOption()
    {
        // Play the button click sound
        AudioManager.instance.Play("ClickSound");

        _selectedOption--;

        if (_selectedOption < 0)
        {
            _selectedOption = characterDB.CharacterCount - 1;
        }

        updateCharacter(_selectedOption);
        save();
    }

    /*
     * This function updates the character sprite based on the selected option.
     * @param selectedOption The index of the selected character.
     */
    private void updateCharacter(int selectedOption)
    {
        CharacterVariable character = characterDB.getCharacter(selectedOption);
        characterSprite.sprite = character.characterSprite;
    }

    /*
     * This function loads the selected character option from PlayerPrefs.
     */
    private void load()
    {
        _selectedOption = PlayerPrefs.GetInt("selectedOption");
    }

    /*
     * This function saves the selected character option to PlayerPrefs.
     */
    private void save()
    {
        PlayerPrefs.SetInt("selectedOption", _selectedOption);
    }

    /*
     * This function changes the scene.
     * @param sceneID The name of the scene to load.
     */
    public void changeScene(string sceneID)
    {
        // Play the button click sound
        AudioManager.instance.Play("ClickSound");
        // Fading out the MenuBGM and fading in the CabinBGM
        StartCoroutine(transitionToGameScene());
    }

    /*
     * This coroutine handles the transition to the game scene.
     * It fades out the menu background music and then loads the game scene.
     */
    private IEnumerator transitionToGameScene()
    {
        // Fade out the MenuBGM
        StartCoroutine(AudioManager.instance.FadeOut("MenuBGM", 1.5f));
        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene("Level 1");
    }
}