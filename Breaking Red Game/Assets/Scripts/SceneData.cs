using UnityEngine;

[System.Serializable]
public class SceneData
{
    public string levelName;
    public string sceneName;
    public EnvironmentManager environmentManager;
    public AudioManager audioManager;

    public string GetLevelName()
    {
        return levelName;
    }

    public string GetSceneName()
    {
        return sceneName;
    }

    public EnvironmentManager GetEnvironmentManager()
    {
        return environmentManager;
    }

    public AudioManager GetAudioManager()
    {
        return audioManager;
    }
}
