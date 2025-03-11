using UnityEngine;

public class SceneEngine : MonoBehaviour
{
    private static SceneEngine instance;
    public SceneData sceneData;

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
    }

    public static SceneEngine GetInstance()
    {
        return instance;
    }

    public void TriggerEnvironment(EnvironmentData envData)
    {
        EnvironmentManager.GetInstance().UpdateEnvironment(envData);
    }

    public void TriggerAudio(AudioData audioData)
    {
        AudioManager.GetInstance().PlaySound(audioData);
    }
}
