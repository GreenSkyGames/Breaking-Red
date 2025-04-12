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
        _behavior = _toggle.isOn ? new BCModeBehavior() : new ModeBehavior(); 

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
        Debug.Log("RunBehavior() was called"); //sanity check to know when function is called 
        Debug.Log("RB: DYNAMIC Execute() was called"); 
        _behavior.Execute(); //dynamic

        Debug.Log("RB: STATIC Describe() Was called"); 
        _behavior.Describe("Static Binding used here."); //static  

        //Oral Exam 
        Debug.Log("--------------Experimenting for ORAL EXAM---------------"); 
        ModeBehavior _behavior1 = new ModeBehavior(); 
        ModeBehavior _behavior2 = new BCModeBehavior(); 

        //1. Dynamically bound method  
        _behavior1.Execute(); // this calls the overridden method in BCModeBehavior
        // output: 

        //2. dynamic type  
        _behavior2.Execute(); // calls the base class method 
        // output: 

        //3. static bound method 
        _behavior2.Describe("Awesome"); //still cals the base class method

    }

    public class ModeBehavior
    {
        public virtual void Execute() // function to be overwritten later 
        {
            Debug.Log("ModeBehavior: Default BC Mode Behavior with virtual");
        } 

        public void Describe(string description) //static function 
        {
            Debug.Log("ModeBehavior: Description: " + description ); 
        }
    }

    public class BCModeBehavior: ModeBehavior // inhertits from ModeBehavior 
    {
        // override OR virtual 
        public override void Execute() // dynamic, replaces the Execute() 
        {
            Debug.Log("BCModeBehavior: Executing BC Mode ... Dynamic Binding with override. "); 
        }
    }
}

