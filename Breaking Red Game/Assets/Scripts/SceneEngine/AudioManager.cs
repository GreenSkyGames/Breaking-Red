using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    // 音效文件
    public AudioClip bgMusicL1;
    public AudioClip bgMusicL2;
    public AudioClip rainSound;
    public AudioClip windSound;
    public AudioClip footstepSound;
    public AudioClip birdSound;
    public AudioClip bugSound;

    private AudioSource audioSource;

    void Awake()
    {
        // 确保只有一个 AudioManager 实例
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // 获取 AudioSource 组件来播放音效
        audioSource = GetComponent<AudioSource>();
    }

    // 播放背景音乐
    public void Start()
    {
        audioSource.clip = bgMusicL1;
        audioSource.Play();
    }

    // 播放天气音效（风声）
    public void playWindSound()
    {
        audioSource.clip = windSound;
        audioSource.loop = true;  // 循环播放
        audioSource.Play();
    }

    public void playRainSound()
    {
        audioSource.clip = rainSound;
        audioSource.loop = true;
        audioSource.Play();
    }

    // 播放脚步音效
    public void playFootstepSound()
    {
        audioSource.PlayOneShot(footstepSound);  // 使用 PlayOneShot 播放一次脚步声
    }

    // 播放场景音效（鸟叫、虫鸣）
    public void playBirdSound()
    {
        audioSource.clip = birdSound;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void playBugSound()
    {
        audioSource.clip = bugSound;
        audioSource.loop = true;
        audioSource.Play();
    }

    // 停止当前播放的音效
    public void stopSound()
    {
        audioSource.Stop();
    }

    // 你可以为不同场景/天气切换不同音效
    public void changeWeatherSound(string weatherType)
    {
        stopSound();  // 停止当前音效
        if (weatherType == "rain")
        {
            playRainSound();
        }
        else if (weatherType == "wind")
        {
            playWindSound();
        }
    }
}
