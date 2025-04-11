using UnityEngine;

public class testDynamicBinding : MonoBehaviour
{
    public GameObject normalPassagePrefab;
    public GameObject lockedPassagePrefab;
    public GameObject damagingEnvPrefab;
    public GameObject movingPlatformPrefab;
    public GameObject movingPlatformTilePrefab;
    public GameObject slidingDoorPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LevelLoader b = new Level2();  // declared as base, constructed as derived
        Level2 c = new Level2();       // declared and constructed as derived

        Debug.Log("Calling methods on 'b':");
        b.loadLevel(normalPassagePrefab, lockedPassagePrefab, damagingEnvPrefab, movingPlatformPrefab, movingPlatformTilePrefab, slidingDoorPrefab);      // "Loading level from level2" dyanmic
        b.staticMethod(normalPassagePrefab);   // "Static method from LevelLoader" static

        Debug.Log("Calling methods on 'c':");
        c.loadLevel(normalPassagePrefab, lockedPassagePrefab, damagingEnvPrefab, movingPlatformPrefab, movingPlatformTilePrefab, slidingDoorPrefab);      // "Loading level from level2"
        c.staticMethod(normalPassagePrefab);   // "Static method from level2"
    }
}
