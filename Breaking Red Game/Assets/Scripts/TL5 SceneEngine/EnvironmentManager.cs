using Unity.VisualScripting;
using UnityEngine;

/*
 * Name:  Hengyi Tian
 * Role:   TL5 -- AI Specialist
 * This file contains the definition for the EnvironmentManager class.
 * The EnvironmentManager class manages the environment settings, including weather and background.
 */

public class EnvironmentManager : MonoBehaviour
{
    private static EnvironmentManager instance; // Singleton instance of the EnvironmentManager
    private EnvironmentData environmentData; // Holds the current environment data

    void Awake()
    {
        // Ensure there is only one instance of the EnvironmentManager
        if (instance == null)
        {
            instance = this; // Assign the current instance as the singleton instance
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances to maintain a single instance
        }

        environmentData = new EnvironmentData(); // Initialize environment data
    }

    // GetInstance returns the singleton instance of the EnvironmentManager
    public static EnvironmentManager GetInstance()
    {
        return instance;
    }

    // UpdateEnvironment allows updating the environment data
    public void UpdateEnvironment(EnvironmentData envData)
    {
        environmentData = envData;
        Debug.Log($"Environment Updated: Weather={envData.weather}, Background={envData.background}");
    }

    // GetEnvironmentData returns the current environment data
    public EnvironmentData GetEnvironmentData()
    {
        return environmentData;
    }
}

// Class to hold the environment data
[System.Serializable]
public class EnvironmentData
{
    public string weather;
    public string background;
}
