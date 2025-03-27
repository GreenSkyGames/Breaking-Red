/*
* Name:  Mark Eldridge
* Role:   Main Character Customization
* This file contains the definition for the CharacterDatabase class.
* This class manages the character data.
*/

using UnityEngine;

[CreateAssetMenu]
public class CharacterDatabase : ScriptableObject
{
    public CharacterVariable[] character;

    public int CharacterCount
    {
        get{
            return character.Length;
        }
    }

    /*
    * This function retrieves a character from the database by index.
    * It takes an integer index as a parameter and returns the CharacterVariable at that index.
    */
    public CharacterVariable getCharacter(int index)
    {
        return character[index];
    }
}