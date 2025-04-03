using UnityEngine;

public class CanvasDontDestroy : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);  // Keep this canvas across scene loads
    }
}
