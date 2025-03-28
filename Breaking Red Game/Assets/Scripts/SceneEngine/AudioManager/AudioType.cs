using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/*
 * Name: Hengyi Tian
 * Role: TL5-- AI Specialist
 * This file contains the definition for the EnvironmentManager class.
 * It inherits from MonoBehaviour.
 */

[System.Serializable]
public class AudioType
{
    [HideInInspector]
    public AudioSource Source;
    public AudioClip Clip;
    public AudioMixerGroup Group;

    public string Name;

    [Range(0f, 1f)]
    public float Volume;
    [Range(0.1f, 5f)]
    public float Pitch;
    public bool Loop;
}