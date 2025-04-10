/*Name: Alex Senst
 * Role: Team Lead 2+ -- Software Architect
 * 
 * This file contains the definition for the Terrain Objects Class
 * This class specifies the overall behavior of all terrain objects and acts as the class most objects in the scene inherits from
 * It inherets from MonoBehavior
 */
using UnityEngine;
using UnityEngine.UIElements;

public class TerrainObjects : MonoBehaviour
{
    public virtual void Interact()
    {
        Debug.Log("Terrain object interacts");
    }
    public Vector3 spawnPos;

    public string spriteName;
    
    public TerrainObjects(Vector3 pos, string sprite)
    {
        spawnPos = pos;
        spriteName = sprite;

    }
    
    /*public virtual void Spawn()
    {
        transform.position = spawnPos;
        GetComponent<SpriteRenderer>().sprite = terrainSprite;
    }*/
}
