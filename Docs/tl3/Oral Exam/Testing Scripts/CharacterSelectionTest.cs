using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using NUnit.Framework;

public class CharacterSelectionTest : MonoBehaviour
{
    public CharacterManager characterManager;
    public Button nextButton;
    public Button backButton;
    public int numberOfTests = 3;
    public float delayBetweenTests = 0.5f;
    public bool testNextButton = true;
    public bool testBackButton = true;
    public bool testEmptyDatabase = true;
    public bool testSingleCharacter = true;
    public bool testNullPrefab = true;
    public bool testNullAnimator = true;
    public bool testRapidInput = true;
    public bool testLargeDatabase = true;
    public bool testConcurrentSceneLoad = true;
    public bool testPrefabInstantiation = true;

    public int rapidInputIterations = 100;
    public float rapidInputDelay = 0.01f;  
    public int largeDatabaseSize = 100;   
    public float sceneLoadDelay = 0.1f;
    public int prefabSwitchCount = 50;

    private int characterCount;
    private int currentTestIteration = 0;
    private CharacterVariable[] originalCharacterList;
    private List<CharacterVariable> largeCharacterList; // To store the large database for Stress Test 2
    private bool sceneLoadTriggered = false;

    void Start()
    {
        // Ensure the CharacterManager and buttons are assigned
        if (characterManager == null)
        {
            Debug.LogError("CharacterManager is not assigned. Please assign it in the Inspector.");
            return;
        }
        if (nextButton == null || backButton == null)
        {
            Debug.LogError("Next or Back button is not assigned. Please assign them in the Inspector.");
            return;
        }

        // Get the number of characters from the CharacterDatabase through the manager.
        characterCount = characterManager.characterDB.CharacterCount;

        // Store the original character list to restore later
        originalCharacterList = new CharacterVariable[characterCount];
        if (characterManager.characterDB.character != null)
        {
            System.Array.Copy(characterManager.characterDB.character, originalCharacterList, characterCount);
        }

        // Start the test coroutine
        StartCoroutine(TestCharacterSelection());
    }

    IEnumerator TestCharacterSelection()
    {
        Debug.Log("Starting Character Selection Boundary and Stress Test...");

        for (currentTestIteration = 0; currentTestIteration < numberOfTests; currentTestIteration++)
        {
            Debug.Log($"Test Iteration: {currentTestIteration + 1}/{numberOfTests}");

            // 1. Test with Empty Character Database
            if (testEmptyDatabase)
            {
                yield return RunEmptyDatabaseTest();
            }

            // 2. Test with a Single Character
            if (testSingleCharacter)
            {
                yield return RunSingleCharacterTest();
            }

            // 3. Test with Null Character Prefabs
            if (testNullPrefab)
            {
                yield return RunNullPrefabTest();
            }

            // 4. Test with Null Animator Controller
            if (testNullAnimator)
            {
                yield return RunNullAnimatorTest();
            }

            // 5. Test Next Button
            if (testNextButton)
            {
                yield return RunNextButtonTest();
            }

            // 6. Test Back Button
            if (testBackButton)
            {
                yield return RunBackButtonTest();
            }

            // 7. Rapid Next/Back Button Input
            if (testRapidInput)
            {
                yield return RunRapidInputTest();
            }

            // 8. Large Character Database Test
            if (testLargeDatabase)
            {
                yield return RunLargeDatabaseTest();
            }

            // 9. Concurrent Scene Loading
            if (testConcurrentSceneLoad)
            {
                yield return RunConcurrentSceneLoadTest();
            }

            // 10. Prefab Instantiation Stress
            if (testPrefabInstantiation)
            {
                yield return RunPrefabInstantiationTest();
            }

            yield return new WaitForSeconds(delayBetweenTests);
        }
        Debug.Log("Character Selection Boundary and Stress Test Completed.");
        // Restore the original character list after all tests are done
        characterManager.characterDB.character = originalCharacterList;
    }

    IEnumerator RunNextButtonTest()
    {
        Debug.Log("Testing Next Button...");
        int initialOption = characterManager.getSelectedOption();
        for (int i = 0; i < characterCount + 2; i++)
        {
            characterManager.nextOption();
            Debug.Log($"  Next Option: Selected Option = {characterManager.getSelectedOption()}");
            yield return new WaitForSeconds(delayBetweenTests);
        }
        int finalOptionNext = characterManager.getSelectedOption();
        if (finalOptionNext % characterCount == initialOption % characterCount)
        {
            Debug.Log("Next Button Test Passed: Cycled Correctly");
        }
        else
        {
            Debug.LogError("Next Button Test Failed: Did not cycle correctly.");
        }
    }

    IEnumerator RunBackButtonTest()
    {
        Debug.Log("Testing Back Button...");
        int initialOptionBack = characterManager.getSelectedOption();
        for (int i = 0; i < characterCount + 2; i++)
        {
            characterManager.backOption();
            Debug.Log($"  Back Option: Selected Option = {characterManager.getSelectedOption()}");
            yield return new WaitForSeconds(delayBetweenTests);
        }
        int finalOptionBack = characterManager.getSelectedOption();
        if (finalOptionBack % characterCount == initialOptionBack % characterCount)
        {
            Debug.Log("Back Button Test Passed: Cycled Correctly");
        }
        else
        {
            Debug.LogError("Back Button Test Failed: Did not cycle correctly.");
        }
    }

    IEnumerator RunEmptyDatabaseTest()
    {
        Debug.Log("Testing with Empty Character Database...");
        // Store the original character list
        CharacterVariable[] originalList = characterManager.characterDB.character;
        // Set the character database to empty
        characterManager.characterDB.character = new CharacterVariable[0];
        characterCount = 0; //update

        // Call Next and Back to see how it behaves
        characterManager.nextOption();
        characterManager.backOption();

        yield return new WaitForSeconds(delayBetweenTests);

        //check
        if (characterManager.characterDB.character != null && characterManager.characterDB.character.Length == 0)
        {
            Debug.Log("Empty DB Test Passed: Handled empty database correctly.");
        }
        else
        {
            Debug.LogError("Empty DB Test Failed:  CharacterManager did not handle empty database.");
        }

        // Restore the original character list
        characterManager.characterDB.character = originalList;
        characterCount = originalList.Length;
    }

    IEnumerator RunSingleCharacterTest()
    {
        Debug.Log("Testing with Single Character...");
        // Store the original character list
        CharacterVariable[] originalList = characterManager.characterDB.character;
        // Create a new array with only one character
        CharacterVariable[] singleCharacterList = new CharacterVariable[1];
        if (characterManager.characterDB.character != null && characterManager.characterDB.character.Length > 0)
        {
            singleCharacterList[0] = characterManager.characterDB.character[0];  // Copy the first character
        }
        characterManager.characterDB.character = singleCharacterList;
        characterCount = 1; //update

        // Call Next and Back
        characterManager.nextOption();
        characterManager.backOption();
        yield return new WaitForSeconds(delayBetweenTests);

        if (characterManager.getSelectedOption() == 0)
        {
            Debug.Log("Single Character Test Passed: Handled single character correctly.");
        }
        else
        {
            Debug.LogError("Single Character Test Failed: Did not handle single character correctly.");
        }

        // Restore the original character list
        characterManager.characterDB.character = originalList;
        characterCount = originalList.Length; //restore
    }

    IEnumerator RunNullPrefabTest()
    {
        Debug.Log("Testing with Null Character Prefab...");
        // Find the first non-null prefab, so we don't break everything.
        int firstValidIndex = -1;
        for (int i = 0; i < characterCount; i++)
        {
            if (characterManager.characterDB.character[i].characterPrefab != null)
            {
                firstValidIndex = i;
                break;
            }
        }

        if (firstValidIndex == -1)
        {
            Debug.LogWarning("Null Prefab Test Skipped: No non-null prefab found.");
            yield break;
        }
        // Store the original prefab
        GameObject originalPrefab = characterManager.characterDB.character[firstValidIndex].characterPrefab;
        // Set a prefab to null
        characterManager.characterDB.character[firstValidIndex].characterPrefab = null;

        // Call Next and Back
        characterManager.nextOption();
        characterManager.backOption();
        yield return new WaitForSeconds(delayBetweenTests);

        // Restore
        characterManager.characterDB.character[firstValidIndex].characterPrefab = originalPrefab;
        Debug.Log("Null Prefab Test: Check console for errors in CharacterManager.");
    }

    IEnumerator RunNullAnimatorTest()
    {
        Debug.Log("Testing with Null Animator Controller...");
        // Find the first non-null animator, so we don't break everything.
        int firstValidIndex = -1;
        for (int i = 0; i < characterCount; i++)
        {
            if (characterManager.characterDB.character[i].animatorController != null)
            {
                firstValidIndex = i;
                break;
            }
        }

        if (firstValidIndex == -1)
        {
            Debug.LogWarning("Null Animator Test Skipped: No non-null animator found.");
            yield break;
        }
        // Store the original animator
        RuntimeAnimatorController originalAnimator = characterManager.characterDB.character[firstValidIndex].animatorController;
        // Set a prefab to null
        characterManager.characterDB.character[firstValidIndex].animatorController = null;

        // Call Next and Back
        characterManager.nextOption();
        characterManager.backOption();
        yield return new WaitForSeconds(delayBetweenTests);

        // Restore
        characterManager.characterDB.character[firstValidIndex].animatorController = originalAnimator;
        Debug.Log("Null Animator Test: Check console for errors in CharacterManager.");
    }

    IEnumerator RunRapidInputTest()
    {
        Debug.Log("Testing Rapid Next/Back Button Input...");
        int initialOption = characterManager.getSelectedOption();
        for (int i = 0; i < rapidInputIterations; i++)
        {
            nextButton.onClick.Invoke(); // Simulate button press
            yield return new WaitForSeconds(rapidInputDelay);
        }
        int finalOptionNext = characterManager.getSelectedOption();
        Debug.Log($"  Initial Option: {initialOption}, Final Option: {finalOptionNext}");
        //add assert
        Assert.AreEqual(initialOption % characterCount, finalOptionNext % characterCount, "Rapid Next Button Test Failed: Did not cycle correctly.");
        Debug.Log("Rapid Next Button Test Passed: Cycled Correctly");

        yield return new WaitForSeconds(delayBetweenTests);

        initialOption = characterManager.getSelectedOption();
        for (int i = 0; i < rapidInputIterations; i++)
        {
            backButton.onClick.Invoke();
            yield return new WaitForSeconds(rapidInputDelay);
        }
        finalOptionNext = characterManager.getSelectedOption();
        Debug.Log($"  Initial Option: {initialOption}, Final Option: {finalOptionNext}");
        //add assert
        Assert.AreEqual(initialOption % characterCount, finalOptionNext % characterCount, "Rapid Back Button Test Failed: Did not cycle correctly.");
        Debug.Log("Rapid Back Button Test Passed: Cycled Correctly");
    }

    IEnumerator RunLargeDatabaseTest()
    {
        Debug.Log("Testing with Large Character Database...");
        // Store the original character list
        originalCharacterList = characterManager.characterDB.character;
        // Create a large character list
        largeCharacterList = new List<CharacterVariable>();
        for (int i = 0; i < largeDatabaseSize; i++)
        {
            // Duplicate existing characters or create new ones as needed for your game
            int index = i % characterCount; // Cycle through existing characters
            largeCharacterList.Add(characterManager.characterDB.character[index]);
        }
        characterManager.characterDB.character = largeCharacterList.ToArray();
        characterCount = largeDatabaseSize;

        //measure time
        float startTime = Time.realtimeSinceStartup;
        for (int i = 0; i < characterCount; i++)
        {
            characterManager.nextOption();
            yield return null; // Allow a frame to pass
        }
        float endTime = Time.realtimeSinceStartup;
        float timeToIterate = endTime - startTime;
        Debug.Log($"Time to iterate through {largeDatabaseSize} characters: {timeToIterate} seconds");
        Assert.Less(timeToIterate, 5f, "Large Database Test Failed: Iteration took too long."); //set reasonable time
        Debug.Log("Large Character Database Test Passed: Iteration time is within acceptable limits.");

        // Restore original database
        characterManager.characterDB.character = originalCharacterList;
        characterCount = originalCharacterList.Length;
        largeCharacterList.Clear();
    }

    IEnumerator RunConcurrentSceneLoadTest()
    {
        Debug.Log("Testing Concurrent Scene Loading...");
        // Trigger scene load after a short delay
        yield return new WaitForSeconds(sceneLoadDelay);
        sceneLoadTriggered = true;
        characterManager.changeScene("Level 1"); // Load your gameplay scene here

        // Simulate rapid button presses during scene loading
        int initialOption = characterManager.getSelectedOption();
        for (int i = 0; i < 20; i++) // Press a few times
        {
            nextButton.onClick.Invoke();
            yield return null;
        }
        int finalOption = characterManager.getSelectedOption();
        Debug.Log($"Initial option: {initialOption}, Final Option: {finalOption}, Scene Loaded: {sceneLoadTriggered}");
        Assert.IsTrue(sceneLoadTriggered, "Concurrent Scene Load Test Failed: Scene did not load.");
        // You might need more sophisticated checks to ensure correct character is loaded in the new scene.
        yield return new WaitForSeconds(5f); //wait

    }

    IEnumerator RunPrefabInstantiationTest()
    {
        Debug.Log("Testing Prefab Instantiation Stress...");
        List<GameObject> instantiatedObjects = new List<GameObject>();
        float startTime = Time.realtimeSinceStartup;
        for (int i = 0; i < prefabSwitchCount; i++)
        {
            // Alternate between two characters for rapid instantiation/destruction
            int charIndex = i % 2;
            CharacterVariable character = characterManager.characterDB.getCharacter(charIndex);
            GameObject newCharacterDisplay = Instantiate(character.characterPrefab, characterManager.characterDisplayArea.position, characterManager.characterDisplayArea.rotation, characterManager.characterDisplayArea);
            instantiatedObjects.Add(newCharacterDisplay);
            yield return null; // Allow a frame
            if (i > 0)
            {
                Destroy(instantiatedObjects[i-1]);
            }
        }
        float endTime = Time.realtimeSinceStartup;
        float timeTaken = endTime - startTime;
        Debug.Log($"Time to instantiate and destroy {prefabSwitchCount} prefabs: {timeTaken} seconds");
        Assert.Less(timeTaken, 5f, "Prefab Instantiation Test Failed: Took too long.");
        Debug.Log("Prefab Instantiation Test Passed: Within acceptable time.");
        foreach(GameObject obj in instantiatedObjects)
        {
            Destroy(obj);
        }
        instantiatedObjects.Clear();
    }
}
