using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    /// <summary>
    /// set health to value.
    /// </summary>
    /// <param name="num"></param>
    public void SetHealth(float num)
    {
        slider.value = num;
    }

    /// <summary>
    /// initializes health with max value and full health.
    /// </summary>
    /// <param name="num"></param>
    public void InitHealth(float num)
    {
        slider.maxValue = num;
        SetHealth(num);
    }

    /// <summary>
    /// increases max and current health by given value.
    /// </summary>
    /// <param name="num"></param>
    public void IncreaseHealth(float num)
    {
        slider.maxValue += num;
        slider.value += num;
    }
}
