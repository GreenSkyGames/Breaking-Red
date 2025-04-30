using System.Collections;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.UI;

public class LockedPassage : NormalPassage
{
    [SerializeField] private bool isUnlocked = false; // Determines if the passageway is accessible
    [SerializeField] private GameObject doorOverlay; // Blocks visibility until unlocked
    [SerializeField] private SlidingDoor slidingDoor; // Reference to the SlidingDoor script
    [SerializeField] private SlidingDoor slidingDoor2; // Reference to the SlidingDoor script

    //private CanvasGroup doorOverlayInstance;
    //private GameObject slidingDoor1;
    //private GameObject slidingDoor2;

    /*protected override void Start()
    {
        base.Start();
        if (doorOverlay != null)
        {
            GameObject overlay = Instantiate(doorOverlay, transform); // Parent it to this object
            doorOverlayInstance = overlay.GetComponent<CanvasGroup>();

            if (doorOverlayInstance != null)
            {
                doorOverlayInstance.alpha = 1f;
                overlay.SetActive(true);
                Debug.Log("Door overlay instantiated and visible.");
            }
            else
            {
                Debug.LogWarning("Instantiated doorOverlay is missing CanvasGroup!");
            }
        }
        else
        {
            Debug.LogWarning("doorOverlayPrefab is not assigned!");
        }
    }*/

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) // Press "E" to unlock
        {
            revealPassageway(false);
        }
    }

    public void revealPassageway(bool wolfDead)
    {
        Debug.Log("wolfDead: " + wolfDead + " unlocked: " + isUnlocked);
        if (isUnlocked && !wolfDead)
        {
            Debug.Log("Got here");
            isUnlocked = true;
            StartCoroutine(wolfDeathWait());
            wolfDead = true;
        }
        else if (!isUnlocked)
        {
            isUnlocked = true;
            StartCoroutine(FadeOutDoorOverlay());
        }

    }

    private IEnumerator wolfDeathWait()
    {
        Debug.Log("Waiting");
        yield return new WaitForSeconds(4);
        StartCoroutine(FadeOutDoorOverlay());
    }

    private IEnumerator FadeOutDoorOverlay()
    {
        if (doorOverlay == null) yield break;

        CanvasGroup cg = doorOverlay.GetComponent<CanvasGroup>();
        if (cg == null) yield break;

        float duration = 1.5f;
        float elapsed = 0;

        while (elapsed < duration)
        {
            cg.alpha = Mathf.Lerp(1, 0, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        cg.alpha = 0;
        doorOverlay.SetActive(false); // Fully invisible, deactivate object
        if (slidingDoor != null)
        {
            slidingDoor.UnlockDoor(); // Set the sliding door's unlocked bool to true
        }
        if (slidingDoor2 != null)
        {
            slidingDoor2.UnlockDoor(); // Set the sliding door's unlocked bool to true
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (isUnlocked) // Only allow passage if unlocked
        {
            base.OnTriggerEnter2D(other);
        }
    }
}
