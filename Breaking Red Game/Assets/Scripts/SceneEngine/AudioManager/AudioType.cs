using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

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