// /*
//  *  Author: ariel oliveira [o.arielg@gmail.com]
//  */

using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public int health;
    public int maxHealth;

    public Sprite emptyHeart;
    public Sprite fullHeart;
    public Image[] hearts;

    public PlayerHealth playerHealth; //allows for communication between scripts

    void Update(){

        health = playerHealth.currentHealth;
        maxHealth = playerHealth.maxHealth;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                //hearts[i].gameObject.SetActive(true); // Corrected: Added parentheses
                hearts[i].sprite = fullHeart;
            }
            else{
                hearts[i].sprite = emptyHeart;
            }
            if (i < maxHealth)
            {
                hearts[i].gameObject.SetActive(true); // Corrected: Added parentheses
                //hearts[i].sprite = emptyHeart;
            }
            else
            {
                hearts[i].gameObject.SetActive(false); // Corrected: Added parentheses
            }
        }
    }
}
// {
//     private GameObject[] heartContainers;
//     private Image[] heartFills;

//     public Transform heartsParent;
//     public GameObject heartContainerPrefab;

//     private void Start()
//     {
//         // Should I use lists? Maybe :)
//         heartContainers = new GameObject[(int)PlayerStats.Instance.MaxTotalHealth];
//         heartFills = new Image[(int)PlayerStats.Instance.MaxTotalHealth];

//         PlayerStats.Instance.onHealthChangedCallback += UpdateHeartsHUD;
//         InstantiateHeartContainers();
//         UpdateHeartsHUD();
//     }

//     public void UpdateHeartsHUD()
//     {
//         SetHeartContainers();
//         SetFilledHearts();
//     }

//     void SetHeartContainers()
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

//     void SetFilledHearts()
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

//     void InstantiateHeartContainers()
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
