using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LockedPassage : NormalPassage
{
    [SerializeField] private bool isUnlocked = false; // Determines if the passageway is accessible
    [SerializeField] private CanvasGroup doorOverlay; // Blocks visibility until unlocked
    [SerializeField] private SlidingDoor slidingDoor; // Reference to the SlidingDoor script

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
            revealPassageway();
        }
    }

    public void revealPassageway()
    {
        if (!isUnlocked)
        {
            isUnlocked = true;
            StartCoroutine(FadeOutDoorOverlay());
        }

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
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (isUnlocked) // Only allow passage if unlocked
        {
            base.OnTriggerEnter2D(other);
        }
    }
}
