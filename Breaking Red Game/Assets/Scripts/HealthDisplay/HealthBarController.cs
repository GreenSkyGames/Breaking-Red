/*
 * Name:  Mark Eldridge
 * Role:   Main Character Customization
 * This file contains the definition for the HealthBarController class.
 * This class controls the health bar display.
 * It inherits from MonoBehaviour.
 */

using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public int health;
    public int maxHealth;

    public Sprite emptyHeart;
    public Sprite fullHeart;
    public Image[] hearts;

    public PlayerHealth playerHealth; // Allows communication between scripts

    /*
     * This function updates the health bar display.
     * It is called every frame.
     */
    void Update()
    {
        health = playerHealth.currentHealth;
        maxHealth = playerHealth.maxHealth;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i < maxHealth)
            {
                hearts[i].gameObject.SetActive(true);
            }
            else
            {
                hearts[i].gameObject.SetActive(false);
            }
        }
    }
}
// {
//     private GameObject[] heartContainers;
//     private Image[] heartFills;

//     public Transform heartsParent;
//     public GameObject heartContainerPrefab;

//     /*
//      * This function is called before the first frame update.
//      * It initializes the heart containers and updates the hearts HUD.
//      */
//     void start()
//     {
//         // Consider using lists here
//         heartContainers = new GameObject[(int)PlayerStats.Instance.MaxTotalHealth];
//         heartFills = new Image[(int)PlayerStats.Instance.MaxTotalHealth];

//         PlayerStats.Instance.onHealthChangedCallback += updateHeartsHUD;
//         instantiateHeartContainers();
//         updateHeartsHUD();
//     }

//     /*
//      * This function updates the hearts HUD.
//      */
//     public void updateHeartsHUD()
//     {
//         setHeartContainers();
//         setFilledHearts();
//     }

//     /*
//      * This function sets the heart containers.
//      */
//     void setHeartContainers()
//     {
//         for (int i = 0; i < heartContainers.Length; i++)
//         {
//             if (i < PlayerStats.Instance.MaxHealth)
//             {
//                 heartContainers[i].SetActive(true);
//             }
//             else
//             {
//                 heartContainers[i].SetActive(false);
//             }
//         }
//     }

//     /*
//      * This function sets the filled hearts.
//      */
//     void setFilledHearts()
//     {
//         for (int i = 0; i < heartFills.Length; i++)
//         {
//             if (i < PlayerStats.Instance.Health)
//             {
//                 heartFills[i].fillAmount = 1;
//             }
//             else
//             {
//                 heartFills[i].fillAmount = 0;
//             }
//         }

//         if (PlayerStats.Instance.Health % 1 != 0)
//         {
//             int lastPos = Mathf.FloorToInt(PlayerStats.Instance.Health);
//             heartFills[lastPos].fillAmount = PlayerStats.Instance.Health % 1;
//         }
//     }

//     /*
//      * This function instantiates the heart containers.
//      */
//     void instantiateHeartContainers()
//     {
//         for (int i = 0; i < PlayerStats.Instance.MaxTotalHealth; i++)
//         {
//             GameObject temp = Instantiate(heartContainerPrefab);
//             temp.transform.SetParent(heartsParent, false);
//             heartContainers[i] = temp;
//             heartFills[i] = temp.transform.Find("HeartFill").GetComponent<Image>();
//         }
//     }
// }