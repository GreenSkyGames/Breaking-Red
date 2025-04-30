using UnityEngine;

public class MobileUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       gameObject.SetActive(Application.isMobilePlatform); //mobile controls on mobile app.  
    }

}
