using UnityEngine;

public class Level1 : LevelLoader
{
    public override void LoadLevel()
    {
        //NormalPassage normalPassage = NormalPassage();
        //normalPassage.Spawn(new Vector3(287.33f, -4.88f, 0), "cavetiles_2107")
        terrainList.Add(new NormalPassage(new Vector3(287.33f, -4.88f, 0f), "Cavetiles_2107"));
    }
}
