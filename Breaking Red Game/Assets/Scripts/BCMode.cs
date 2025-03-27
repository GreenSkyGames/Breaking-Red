//BC mode script

using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class BCMODE : MonoBehaviour
{
 [SerializeField] private Toggle _toggle;
 private static BCMODE _instance; 

 void Awake()
 {
    if(_instance == null){
        _instance = this; 
        DontDestroyOnLoad(_toggle); //prevent from being destroyed when scenes swtich 
    }else{
        Destroy(_toggle); //only one instance exists
    }
 }

 void Start()
 {
    if(_toggle == null)
    {
        _toggle = GetComponent<Toggle>();
        if(_toggle == null)
        {
            Debug.LogError("Toggle component not found");
        }
    }

    bool savedState = PlayerPrefs.GetInt("BCMode", 0) == 1;

    _toggle.isOn = savedState;

    _toggle.onValueChanged.AddListener(OnToggleValueChanged);
 }

// When the BC togglge is enabled, making sure that the choice is saved for the rest of the game 
 public void OnToggleValueChanged(bool isON)
 {
    // Play the button click
    AudioManager.instance.Play("ClickSound");

    PlayerPrefs.SetInt("BCMode", isON ? 1 : 0);
    PlayerPrefs.Save();
    Debug.Log($"BC Mode toggled: {(isON ? "ON" : "Off")}"); 
 }
}
