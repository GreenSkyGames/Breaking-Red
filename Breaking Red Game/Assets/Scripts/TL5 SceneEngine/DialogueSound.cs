using System.Collections.Generic;
using UnityEngine;

/*
 * Name: Hengyi Tian
 * Role: TL5-- AI Specialist
 * 
 * This file contains the definition for the DialogueSound class.
 * The DialogueSound class manages the playback of NPC dialogue sounds.
 * It allows adding and playing dialogue sounds for different NPCs.
 * The class follows the Singleton design pattern to ensure only one instance of it exists across scenes.
 */

public class DialogueSound : MonoBehaviour
{
    public static DialogueSound instance; // Singleton instance
    private Dictionary<string, AudioClip> npcDialogueClips = new Dictionary<string, AudioClip>(); // Dictionary to store NPC's dialogue audio clips
    private AudioSource audioSource; // The AudioSource component responsible for playing all NPC dialogue clips

    private void Awake()
    {
        if (instance == null)
        {
            instance = this; // Assign this instance to the static 'instance' field
            DontDestroyOnLoad(gameObject); // Ensure the object persists across scene changes
        }
        else
        {
            Destroy(gameObject); // Destroy any duplicate instances to ensure only one instance exists
        }
    }

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

    // This function is called when add NPCs' dialogue audios clip to dictionary
    public void AddNpcDialogueClip(string npcName, AudioClip clip)
    {
        // Check if the dialogue for the given NPC name already exists
        if (!npcDialogueClips.ContainsKey(npcName))
        {
            npcDialogueClips.Add(npcName, clip); // Add the new dialogue clip for the NPC
        }
        else
        {
            Debug.LogWarning("Dialogue audio for " + npcName + " already exists.");
        }
    }

    // This function is called when play the dialogue sound for a given NPC by name
    public void PlayDialogueSound(string npcName)
    {
        // Check if the AudioSource has been initialized
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource has been destroyed or not initialized.");
            return;
        }
        // Check if the NPC has a dialogue audio clip
        if (npcDialogueClips.ContainsKey(npcName))
        {
            audioSource.clip = npcDialogueClips[npcName]; // Set the audio clip to the corresponding NPC's dialogue
            audioSource.Play(); // Play the audio clip
        }
        else
        {
            Debug.LogWarning("No dialogue audio found for NPC: " + npcName);
        }
    }

}
