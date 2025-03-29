using Ink.Parsed;
using UnityEngine;

/*
 * Name: Hengyi Tian
 * Role: TL5-- AI Specialist
 * This file contains the definition for the WeatherManager class and related classes.
 * WeatherManager controls weather changes and activates related visual effects.
 * Using a random weather generator and managing rain and snow effects.
 * It inherits from MonoBehaviour.
 */

// Provides a virtual method that can be overridden to return a random weather condition.
public class Weather
{
    // This virtual function return a random weather condition. It can be overridden by subclasses to provide specific weather
    public virtual int v_getRandomWeather()
    {
        return 0; // No specific weather taht can be overridden
    }
}

// Subclass that overrides the virtual weather function to return a randomly generated weather value.
public class RandomWeather : Weather
{
    // This override of the v_getRandomWeather function generates a rondom weather type by returing either 0 or 1
    public override int v_getRandomWeather()
    {
        return Random.Range(0, 2);  // 0 = rain, 1 = snow
    }
}

// Random weather updates and change visual effects accordingly
public class WeatherManager : MonoBehaviour
{
    public GameObject rainEffect;    // Rain particle effect
    public GameObject snowEffect;    // Snow particle effect

    private Weather _myWeather;      // Current weather condition

    private void Start()
    {
        _myWeather = new RandomWeather(); // Initialize scene weather with random generator

        updateWeather();  // Set initial weather

        InvokeRepeating(nameof(updateWeather), 0f, 8f); // Change weather every 8 seconds
    }

    // This function choose a random weather effect, rain or snow, and activate the its visual effect
    public void updateWeather()
    {
        int weatherChoice = _myWeather.v_getRandomWeather();

        if (weatherChoice == 0)
        {
            activateRain(); // Active rain effect
        }
        else
        {
            activateSnow(); // Active snow effect
        }
    }

    // Activate rain effect and hide snow effect
    private void activateRain()
    {
        if (rainEffect != null)
        {
            rainEffect.SetActive(true); // Show rain effect
        }
        if (snowEffect != null)
        {
            snowEffect.SetActive(false); // Hide snow effect
        }
    }

    // Activate snow effect and hide rain effect
    private void activateSnow()
    {
        if (snowEffect != null)
        {
            snowEffect.SetActive(true); // Show snow effect
        }
        if (rainEffect != null)
        {
            rainEffect.SetActive(false); // Hide rain effect
        }
    }
}
