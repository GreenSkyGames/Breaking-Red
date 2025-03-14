using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    private static EnvironmentManager instance;
    private EnvironmentData environmentData;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        environmentData = new EnvironmentData();
    }

    public static EnvironmentManager GetInstance()
    {
        return instance;
    }

    public void UpdateEnvironment(EnvironmentData envData)
    {
        environmentData = envData;
        Debug.Log($"Environment Updated: Weather={envData.weather}, Background={envData.background}, Tiles={envData.tiles}");
    }

    public EnvironmentData GetEnvironmentData()
    {
        return environmentData;
    }
}

[System.Serializable]
public class EnvironmentData
{
    public string weather;
    public string background;
    public string tiles;
}
