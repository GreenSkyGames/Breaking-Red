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
    public Vector3 position;
    public string spriteName;

    public virtual GameObject Build(GameObject prefab)
    {
        GameObject obj = Instantiate(prefab, position, Quaternion.identity);
        TerrainObjects terrain = obj.GetComponent<TerrainObjects>();
        if(terrain == null)
        {
            terrain = obj.AddComponent<TerrainObjects>();
        }
        terrain.position = position;
        terrain.spriteName = spriteName;

        return obj;
    }

    public void Spawn(Vector3 position)
    {
        this.position = position;
    }

    public void setSprite(string spriteName)
    {
        //Load and set the sprite based on spriteName
        this.spriteName = spriteName;
        Sprite sprite = Resources.Load<Sprite>($"LevelSprites/{spriteName}");
        if (sprite != null)
        {
            GetComponent<SpriteRenderer>().sprite = sprite;
        }
        else
        {
            Debug.LogWarning($"Sprite '{spriteName} not found in Resources!");
        }
    }
    public virtual void interact(Collider2D other)
    {
        Debug.Log("Default terrain object interaction");
    }
}
