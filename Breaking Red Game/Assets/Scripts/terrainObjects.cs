/*Name: Alex Senst
 * Role: Team Lead 2+ -- Software Architect
 * 
 * This file contains the definition for the Terrain Objects Class
 * This class specifies the overall behavior of all terrain objects and acts as the class most objects in the scene inherits from
 * It inherets from MonoBehavior
 */
using UnityEngine;

public class TerrainObjects : MonoBehaviour
{
    public Vector3 spawnPos;
    public virtual void Spawn()
    {
        transform.position = spawnPos;
    }
}
