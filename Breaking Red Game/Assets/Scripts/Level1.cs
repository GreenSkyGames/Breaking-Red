using System.Runtime.CompilerServices;
using UnityEngine;

public class Level1 : LevelLoader
{
    public override void LoadLevel()
    {
        //NormalPassage normalPassage = NormalPassage();
        //normalPassage.Spawn(new Vector3(287.33f, -4.88f, 0), "cavetiles_2107")
        //terrainList.Add(new NormalPassage(new Vector3(287.33f, -4.88f, 0f), "Cavetiles_2107"));

        CreateNormalPassage("House_tileset_30", new Vector3(-24.73f, -5.656f, 0));
        CreateNormalPassage("House_Red_4", new Vector3(81.26f, 13.343f, 0));
        CreateNormalPassage("House_red_5", new Vector3(82.26f, 13.343f, 0));

        CreateLockedPassage("cavetiles_2107", new Vector3(94.26f, 4.343f, 0));

        CreateSlidingDoor("Tree_45", new Vector3(6.62f, 1.422f, 0), -2f, 1f);
        CreateSlidingDoor("Tree_45", new Vector3(6.58f, -0.417f, 0), 2f, 1f);
    }
    private void CreateNormalPassage(string spriteName, Vector3 position)
    {
        GameObject obj = Object.Instantiate(Resources.Load<GameObject>("NormalPassagePrefab"), position, Quaternion.identity);
        var passage = obj.GetComponent<NormalPassage>();
        passage.spriteName = spriteName;
        passage.SetSprite(spriteName);
    }

    private void CreateLockedPassage(string spriteName, Vector3 position)
    {
        GameObject obj = Object.Instantiate(Resources.Load<GameObject>("LockedPassagePrefab"), position, Quaternion.identity);
        var passage = obj.GetComponent<LockedPassage>();
        passage.spriteName = spriteName;
        passage.SetSprite(spriteName);
    }

    private void CreateDamagingEnv(string spriteName, Vector3 position)
    {
        GameObject obj = Object.Instantiate(Resources.Load<GameObject>("DamagingEnvPrefab"), position, Quaternion.identity);
        var env = obj.GetComponent<DamagingEnv>();
        env.spriteName = spriteName;
        env.SetSprite(spriteName);
    }
        
    private void CreateSlidingDoor(string spriteName, Vector3 position, float verticalGoal, float moveTime)
    {
        GameObject obj = Object.Instantiate(Resources.Load<GameObject>("SlidingDoorPrefab"), position, Quaternion.identity);
        var door = obj.GetComponent<SlidingDoor>();
        door.spriteName = spriteName;
        door.SetSprite(spriteName);
                /* Vector3 goal = new Vector3(position.x, position.y + verticalGoal, position.z);
        door.startPosition = position;
        door.endPosition = goal;
        door.speed = Mathf.Abs(verticalGoal / moveTime);*/
    }
}
