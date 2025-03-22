using Ink.Parsed;
using UnityEngine;

public class Weather
{
    public virtual int getRandomWeather()
    //public int getRandomWeather()
    {
        return 0;
    }
    
}
public class RandomWeather : Weather
{
    public override int getRandomWeather()
    //public int getRandomWeather()
    {
        return Random.Range(0, 2);  // 0 = rain, 1 = snow
    }
}

public class WeatherManager : MonoBehaviour
{
    public GameObject rainEffect;  // Rain particle effect
    public GameObject snowEffect;  // Snow particle effect
    Weather myweather;
    private void Start()
    {
        myweather = new RandomWeather();
        // Randomly choose weather at the start
        RandomWeather();

        // Change weather every 8 seconds
        InvokeRepeating("RandomWeather", 0f, 8f);
    }

    // Randomly choose to activate rain or snow
    
        public void RandomWeather(){
        int weatherChoice = myweather.getRandomWeather();
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
