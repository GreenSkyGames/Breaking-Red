using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/*
 * Name: Hengyi Tian
 * Role: TL5-- AI Specialist
 * 
 * This file contains the definition for the AudioType class.
 * The AudioType class manages audio sources, clips, mixer groups,name, volume, pitch and looping behaviors.
 * It is used to set up audio seetings for background musics and sound effects in different level.
 */

[System.Serializable]
public class AudioType
{
    [HideInInspector]
    public AudioSource Source; // Manage the playback of the AudioClip, but hidden from the inspector
    public AudioClip Clip; // The audio clip to be played
    public AudioMixerGroup Group; // The group of AudioMixerGroup

    public string Name; // The group of AudioMixerGroup

    [Range(0f, 1f)]
    public float Volume; // The volume of the audio, ranging from 0 to 1
    [Range(0.1f, 5f)]
    public float Pitch; // The pitch of the audio, ranging from 0.1 to 5

    public bool Loop; // If true, the audio will loop
}