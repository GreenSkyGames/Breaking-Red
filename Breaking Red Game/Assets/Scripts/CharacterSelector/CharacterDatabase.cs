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

    public CharacterVariable GetCharacter(int index){
        return character[index];
    }
}
