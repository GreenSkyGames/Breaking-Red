using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PauseGameMenu : MonoBehaviour
{
    public static bool isPaused = false; 
    public GameObject PauseMenu; // pause menu object to be connected with script 

    void Start()
    {
        PauseMenu.SetActive(false); // Ensure the pause menu is not visible at start
        LoadGame(); // Load game state if there is any saved data at the start
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                ResumeGame(); 
            }
            else
            {
                PauseGame(); 
            }
        } 
    }

    public void ResumeGame()
{
    PauseMenu.SetActive(false); // Hides the pause menu
    Time.timeScale = 1f; // Resumes normal time flow
    isPaused = false; // Updates the paused state
    Debug.Log("Game Resumed"); // Optional: for debugging
}


    void PauseGame()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f; // pausing the game 
        isPaused = true;
    }

    public void LoadMenu()
    {
        Debug.Log("Loading menu.");
        SceneManager.LoadScene("Start Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game.");
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Uncomment this line if testing in the Unity Editor
        #endif
    }

    public void SaveGame()
    {
        // Assume that you're saving player's position
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerPrefs.SetFloat("PlayerPosX", player.transform.position.x);
            PlayerPrefs.SetFloat("PlayerPosY", player.transform.position.y);
            PlayerPrefs.SetFloat("PlayerPosZ", player.transform.position.z);
            PlayerPrefs.Save();
            Debug.Log("Game Saved: Player's position saved.");
        }
        else
        {
            Debug.Log("Save failed: No player object found.");
        }
    }

    public void LoadGame()
    {
        // Check if saved data exists
        if (PlayerPrefs.HasKey("PlayerPosX"))
        {
            float x = PlayerPrefs.GetFloat("PlayerPosX");
            float y = PlayerPrefs.GetFloat("PlayerPosY");
            float z = PlayerPrefs.GetFloat("PlayerPosZ");
            Vector3 savedPosition = new Vector3(x, y, z);

            // Load player's position
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.transform.position = savedPosition;
                Debug.Log("Game Loaded: Player's position restored.");
            }
            else
            {
                Debug.Log("Load failed: No player object found.");
            }
        }
        else
        {
            Debug.Log("No save data to load.");
        }
    }
}
