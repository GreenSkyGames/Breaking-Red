using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    public GameObject rainEffect;  // Rain particle effect
    public GameObject snowEffect;  // Snow particle effect

    private void Start()
    {
        // Randomly choose weather at the start
        RandomWeather();

        // Change weather every 5 seconds
        InvokeRepeating("RandomWeather", 0f, 5f);  // First call immediately, repeat every 5 seconds
    }

    // Randomly choose to activate rain or snow
    private void RandomWeather()
    {
        int weatherChoice = Random.Range(0, 2);  // 0 = rain, 1 = snow

        if (weatherChoice == 0)
        {
            // Activate rain and deactivate snow
            ActivateRain();
        }
        else
        {
            // Activate snow and deactivate rain
            ActivateSnow();
        }
    }

    // Activate rain effect and deactivate snow effect
    private void ActivateRain()
    {
        if (rainEffect != null)
        {
            rainEffect.SetActive(true);  // Activate rain
        }

        if (snowEffect != null)
        {
            snowEffect.SetActive(false);  // Deactivate snow
        }
    }

    // Activate snow effect and deactivate rain effect
    private void ActivateSnow()
    {
        if (snowEffect != null)
        {
            snowEffect.SetActive(true);  // Activate snow
        }

        if (rainEffect != null)
        {
            rainEffect.SetActive(false);  // Deactivate rain
        }
    }
}
