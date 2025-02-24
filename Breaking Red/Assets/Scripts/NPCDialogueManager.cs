using UnityEngine;
using System;
using System.IO;

public class NPCDialogueManager
{
    public static string[] ReadTextFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            return File.ReadAllLines(filePath); // Reads all lines into an array
        }
        else
        {
            Debug.Log("Error: File not found at " + filePath);
            return new string[0]; // Return an empty array if the file doesn't exist
        }
    }
}
