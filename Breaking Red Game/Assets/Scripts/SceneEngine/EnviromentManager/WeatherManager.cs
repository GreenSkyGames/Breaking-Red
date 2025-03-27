/*
* Name: Hengyi Tian
* Role: TL5-- AI Specialist
* This file contains the definition for the EnvironmentManager class.
* It inherits from MonoBehaviour.
*/

using Ink.Parsed;
using UnityEngine;

// Provides a virtual method that can be overridden to return a random weather condition.
public class Weather
{
    // Virtual function to get random weather value
    public virtual int v_getRandomWeather()
    {
        return 0;
    }
}

// Derived class that overrides the virtual weather function to return a randomly generated weather value.
public class RandomWeather : Weather
{
    public override int v_getRandomWeather()
    {
        return Random.Range(0, 2);  // 0 = rain, 1 = snow
    }
}

// Random weather updates and change visual effects accordingly.
public class WeatherManager : MonoBehaviour
{
    public GameObject rainEffect;    // Rain particle effect
    public GameObject snowEffect;    // Snow particle effect

    private Weather _myWeather;      // Weather instance used to get current weather condition

    private void Start()
    {
        _myWeather = new RandomWeather();

        updateWeather();  // Set initial weather

        InvokeRepeating(nameof(updateWeather), 0f, 8f); // Change weather every 8 seconds
    }

    // Randomly selects a weather type (rain or snow) and activates the its visual effect.
    public void updateWeather()
    {
        int weatherChoice = _myWeather.v_getRandomWeather();

        if (weatherChoice == 0)
        {
            activateRain();
        }
        else
        {
            activateSnow();
        }
    }

    // Activates rain effect and deactivates snow effect.
    private void activateRain()
    {
        if (rainEffect != null)
        {
            rainEffect.SetActive(true);
        }

        if (snowEffect != null)
        {
            snowEffect.SetActive(false);
        }
    }

    // Activates snow effect and deactivates rain effect.
    private void activateSnow()
    {
        if (snowEffect != null)
        {
            snowEffect.SetActive(true);
        }

        if (rainEffect != null)
        {
            rainEffect.SetActive(false);
        }
    }
}
