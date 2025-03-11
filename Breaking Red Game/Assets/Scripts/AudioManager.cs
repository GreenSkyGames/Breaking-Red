using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    private AudioData audioData;

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

        audioData = new AudioData();
    }

    public static AudioManager GetInstance()
    {
        return instance;
    }

    public void PlaySound(AudioData data)
    {
        audioData = data;
        Debug.Log($"Playing BGM: {data.bgMusic}, Sound Effect: {data.soundEffect}");
    }

    public void StopSound()
    {
        Debug.Log("Stopping all sounds.");
        audioData = new AudioData(); // 复位音效数据，避免残留影响
    }

    public AudioData GetAudioData()
    {
        return audioData;
    }
}

[System.Serializable]
public class AudioData
{
    public string bgMusic;
    public string soundEffect;
}
