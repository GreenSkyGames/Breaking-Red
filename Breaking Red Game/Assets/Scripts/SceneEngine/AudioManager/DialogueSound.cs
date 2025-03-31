using System.Collections.Generic;
using UnityEngine;

public class DialogueSound : MonoBehaviour
{
    public static DialogueSound instance;  // Singleton instance
    private Dictionary<string, AudioClip> npcDialogueClips = new Dictionary<string, AudioClip>();  // Store NPC's audio clips
    private AudioSource audioSource;  // The single AudioSource for playing all NPC audio clips

    // Awake is called when the script instance is loaded
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Ensure it persists across scenes
        }
        else
        {
            Destroy(gameObject);  // Destroy duplicate if exists
        }
    }

    // Start initializes the AudioSource
    private void Start()
    {
        // Ensure the AudioSource component is initialized
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();  // Get the AudioSource component if not already set
        }
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not found or initialized!");
        }
    }

    // Add NPC's dialogue audio clip to the dictionary
    public void AddNpcDialogueClip(string npcName, AudioClip clip)
    {
        if (!npcDialogueClips.ContainsKey(npcName))
        {
            npcDialogueClips.Add(npcName, clip);
        }
        else
        {
            Debug.LogWarning("Dialogue audio for " + npcName + " already exists.");
        }
    }

    public void PlayDialogueSound(string npcName)
    {
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource has been destroyed or not initialized.");
            return;
        }
        if (npcDialogueClips.ContainsKey(npcName))
        {
            audioSource.clip = npcDialogueClips[npcName];
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("No dialogue audio found for NPC: " + npcName);
        }
    }

}
