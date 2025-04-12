//TL6 Test 
//Evaluating the existence of the:
// Pause menu, start menu scene, Victory scene, Game Over scene, and credits scene 

using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement; 

public class UITests
{
    //Test to see if the start menu exists in the build settings 
    [Test]
    public void StartMenuSceneExistsInBuildSettings()
    {
        string sceneName = "StartMenu";

        bool sceneFound = false;

        // Loop through all scenes in build settings
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneNameFromPath = System.IO.Path.GetFileNameWithoutExtension(path);
            if (sceneNameFromPath == sceneName)
            {
                sceneFound = true;
                break;
            }
        }

        Assert.IsTrue(sceneFound, $"Scene '{sceneName}' is not included in the Build Settings.");
    }

    //Test to see if the game over scene exists in the build settings 
    [Test]
    public void GameOverSceneExistsInBuildSettings()
    {
        string sceneName = "GameOver";

        bool sceneFound = false;

        // Loop through all scenes in build settings
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneNameFromPath = System.IO.Path.GetFileNameWithoutExtension(path);
            if (sceneNameFromPath == sceneName)
            {
                sceneFound = true;
                break;
            }
        }

        Assert.IsTrue(sceneFound, $"Scene '{sceneName}' is not included in the Build Settings.");
    }

    //Test to see if the victory scene exists in the build settings 
    [Test]
    public void VictorySceneExistsInBuildSettings()
    {
        string sceneName = "Victory";

        bool sceneFound = false;

        // Loop through all scenes in build settings
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneNameFromPath = System.IO.Path.GetFileNameWithoutExtension(path);
            if (sceneNameFromPath == sceneName)
            {
                sceneFound = true;
                break;
            }
        }

        Assert.IsTrue(sceneFound, $"Scene '{sceneName}' is not included in the Build Settings.");
    }
    //Test to see if the credit scene exists in the build settings 
    [Test]
    public void CreditSceneExistsInBuildSettings()
    {
        string sceneName = "Credits";

        bool sceneFound = false;

        // Loop through all scenes in build settings
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneNameFromPath = System.IO.Path.GetFileNameWithoutExtension(path);
            if (sceneNameFromPath == sceneName)
            {
                sceneFound = true;
                break;
            }
        }

        Assert.IsTrue(sceneFound, $"Scene '{sceneName}' is not included in the Build Settings.");
    }

    //test to see if pause canvas exists in the scene 
    [Test]
    public void PauseExist() //PauseActiveSimplePasses 
    {
        // Use the Assert class to test conditions
        GameObject pauseCanvas = GameObject.Find("PauseCanvas");
        Assert.IsNotNull(pauseCanvas, "PauseCanvas does not exist in the scene.");
    }

    // test to see if the Pause menu even exists, in the heirarchy 
    [UnityTest]
    public IEnumerator PauseFound()
    {
        yield return null;
        GameObject pauseCanvas = GameObject.Find("PauseCanvas");

        Assert.IsNotNull(pauseCanvas, "PauseCanvas was not found after waiting a frame.");

        // check if it's active in the hierarchy (visible)
        Assert.IsTrue(pauseCanvas.activeInHierarchy, "PauseCanvas exists but is not active.");
    }
} 