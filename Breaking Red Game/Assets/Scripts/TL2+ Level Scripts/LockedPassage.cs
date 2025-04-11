using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LockedPassage : NormalPassage
{
    [SerializeField] private bool isUnlocked = false; // Determines if the passageway is accessible
    [SerializeField] private CanvasGroup doorOverlay; // Blocks visibility until unlocked
    [SerializeField] private SlidingDoor slidingDoor; // Reference to the SlidingDoor script
    [SerializeField] private SlidingDoor slidingDoor2; // Reference to the SlidingDoor script

    /*public LockedPassage(Vector3 pos, string sprite) : base(pos, sprite)
    {
        // Initialization specific to LockedPassage
    }*/

    private void Start()
    {
        if (doorOverlay != null)
        {
            doorOverlay.alpha = 1; // Fully visible (blocking passageway)
        }
    }

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
        if (!isUnlocked && !wolfDead)
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
        float duration = 1.5f;
        float elapsed = 0;

        while (elapsed < duration)
        {
            doorOverlay.alpha = Mathf.Lerp(1, 0, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        doorOverlay.alpha = 0;
        doorOverlay.gameObject.SetActive(false); // Fully invisible, deactivate object
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
