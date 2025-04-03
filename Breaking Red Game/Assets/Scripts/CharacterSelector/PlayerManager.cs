using UnityEngine;
//using Cinemachine;

public class PlayerManager : MonoBehaviour
{
    void Start()
    {
        if (CharacterManager.selectedCharacter.characterPrefab != null)
        {
            GameObject existingPlayer = GameObject.FindGameObjectWithTag("Player");

            if (existingPlayer != null)
            {
                Vector3 playerPosition = existingPlayer.transform.position;
                Quaternion playerRotation = existingPlayer.transform.rotation;

                Destroy(existingPlayer);

                GameObject player = Instantiate(
                    CharacterManager.selectedCharacter.characterPrefab,
                    playerPosition,
                    playerRotation
                );

                player.GetComponent<Animator>().runtimeAnimatorController = CharacterManager.selectedCharacter.animatorController;

                // Enable PlayerController
                PlayerController playerController = player.GetComponent<PlayerController>();
                if (playerController != null)
                {
                    playerController.enabled = true;
                }

                // Enable gameplay animations
                Animator animator = player.GetComponent<Animator>();
                if (animator != null)
                {
                    animator.SetLayerWeight(animator.GetLayerIndex("Gameplay"), 1);
                }

                // Update Cinemachine target
                // CinemachineVirtualCamera vcam = FindObjectOfType<CinemachineVirtualCamera>();
                // if (vcam != null)
                // {
                //     vcam.Follow = player.transform;
                //     vcam.LookAt = player.transform;
                // }
                // else
                // {
                //     Debug.LogError("Cinemachine Virtual Camera not found.");
                // }
            }
            else
            {
                Debug.LogError("Player GameObject not found in the scene.");
            }
        }
        else
        {
            Debug.LogError("No character prefab was selected.");
        }
    }
}