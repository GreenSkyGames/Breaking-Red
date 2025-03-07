using UnityEngine;

public class FootstepController : MonoBehaviour
{
    private AudioManager audioManager;
    private bool isMoving = false;

    void Start()
    {

        audioManager = AudioManager.instance; 
    }

    void Update()
    {
        bool currentlyMoving = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;

        if (isMoving != currentlyMoving)
        {
            isMoving = currentlyMoving;

            if (isMoving)
            {
                audioManager.playFootstepSound();
            }
            else
            {
                audioManager.stopSound();
            }
        }
    }
}
