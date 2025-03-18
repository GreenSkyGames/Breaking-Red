using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


public class CharacterManager : MonoBehaviour
{
    public CharacterDatabase characterDB;
    public SpriteRenderer characterSprite;

    private int selectedOption = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(!PlayerPrefs.HasKey("selectedOption")){
            selectedOption = 0;
        }
        else{
            Load();
        }
        UpdateCharacter(selectedOption);
    }

    public void NextOption(){
        // Play the button click
        AudioManager.instance.Play("ClickSound");

        selectedOption++;

        if(selectedOption >= characterDB.CharacterCount){
            selectedOption = 0;
        }

        UpdateCharacter(selectedOption);
        Save();
    }

    public void BackOption()
    {
        // Play the button click
        AudioManager.instance.Play("ClickSound");

        selectedOption--;

        if(selectedOption < 0){
            selectedOption = characterDB.CharacterCount - 1;
        }

        UpdateCharacter(selectedOption);
        Save();
    }

    private void UpdateCharacter(int selectedOption){
        CharacterVariable character = characterDB.GetCharacter(selectedOption);
        characterSprite.sprite = character.characterSprite;
    }

    private void Load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
    }

    private void Save()
    {
        PlayerPrefs.SetInt("selectedOption", selectedOption);
    }

    public void ChangeScene(string sceneID){
        // Play the button click
        AudioManager.instance.Play("ClickSound");
        // Fading out the MenuBGM and fading in the CabinBGM
        StartCoroutine(TransitionToGameScene());

    }

    private IEnumerator TransitionToGameScene()
    {
        // Fade out the MenuBGM
        StartCoroutine(AudioManager.instance.FadeOut("MenuBGM", 1.5f));
        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene("Level 1");
    }
}
