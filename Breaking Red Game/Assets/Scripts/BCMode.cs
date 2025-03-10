//Liz Beltran 
//BC mode script

using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class BCMODE : MonoBehaviour
{
 [SerializeField] private Toggle toggle;

 void Start()
 {
    if(toggle == null)
    {
        toggle = GetComponent<Toggle>();
        if(toggle == null)
        {
            Debug.LogError("Toggle component not found");
        }
    }

    bool savedState = PlayerPrefs.GetInt("BCMode", 0) == 1;

    toggle.isOn = savedState;

    toggle.onValueChanged.AddListener(OnToggleValueChanged);
 }

 public void OnToggleValueChanged(bool isON)
 {
    // Play the button click
    AudioManager.instance.Play("ClickSound");

    PlayerPrefs.SetInt("BCMode", isON ? 1 : 0);
    PlayerPrefs.Save();
 }
}
