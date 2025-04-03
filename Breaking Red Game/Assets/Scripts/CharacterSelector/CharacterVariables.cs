/*
 * Name:  Mark Eldridge
 * Role:   Main Character Customization
 * This file contains the definition for the CharacterVariable class.
 * This class stores the sprite data for a character.
 */

using UnityEngine;

[System.Serializable]
public class CharacterVariable
{
    public GameObject characterPrefab; // Added prefab
    public RuntimeAnimatorController animatorController; // Added animator controller
    //public Sprite characterSprite;
    public string characterName;
}