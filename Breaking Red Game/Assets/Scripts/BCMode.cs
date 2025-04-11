//BC mode script

/* Name: Liz Beltran 
 * Role: Team Lead 6 --Version Control Manager
 *	This is the script for BC Mode. This script focuses on the
 * BC Mode option being saved and remembered for the rest of the game 
 * and also to minimize player damage in the game. 
*/

using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class BCMODE : MonoBehaviour
{
    [SerializeField] private Toggle _toggle;
    private static BCMODE _instance; 
    public static BCMODE Instance => _instance; 
    private ModeBehavior _behavior; 

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

        //setting initial behavior 
        _behavior = _toggle.isOn ? new BCModeBehavior() : new BCModeBehavior(); 

        RunBehavior(); // both bindings used here, when the screen is first initialized 
    }

    // When the BC togglge is enabled, making sure that the choice is saved for the rest of the game 
    public void OnToggleValueChanged(bool isON)
    {
        // Play the button click
        AudioManager.instance.Play("ClickSound");

        PlayerPrefs.SetInt("BCMode", isON ? 1 : 0);
        PlayerPrefs.Save();

        //switching bahavior using dynamic binding 
        _behavior = isON? new BCModeBehavior() : new ModeBehavior(); 
        Debug.Log($"BC Mode toggled: {(isON ? "ON" : "Off")}"); 

        RunBehavior(); //both methods used here, when toggle is changed 
    }

    //static and dynamic binding in use 
    void RunBehavior()
    {
        Debug.Log("RunBehavior() was called"); 
        _behavior.Execute(); //dynamic

        _behavior.Describe("Static Binding used here."); //static 

    }

    public class ModeBehavior
    {
        public virtual void Execute() //dynamic 
        {
            Debug.Log("Default BC Mode Behavior");
        } 

        public void Describe(string description) //static 
        {
            Debug.Log("Description: " + description ); 
        }
    }

    public class BCModeBehavior: ModeBehavior
    {
        public override void Execute()
        {
            Debug.Log("Executing BC Mode ... Specific Behavior"); 
        }
    }
}

