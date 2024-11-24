using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DayCycle : MonoBehaviour
{
    int dayCounter = 0;
    public Button NextDay;
    public static event Action OnNextDay;  

    public void nextDay()
    {
        OnNextDay?.Invoke();
        NextDay.interactable = false;
        Debug.Log("next DaY!");
    }
}
