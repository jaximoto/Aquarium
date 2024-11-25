using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DayCycle : MonoBehaviour
{
    public static int dayCounter = 0;
    public Button NextDay;
    public static event Action OnNextDay;  

    public void nextDay()
    {
        OnNextDay?.Invoke();
        NextDay.interactable = false;
        dayCounter++;
        Debug.Log("next DaY!");
        StartCoroutine(ButtonBuffer(NextDay));
    }
    
    IEnumerator ButtonBuffer(Button button)
    {
        yield return new WaitForSeconds(0.5f);
        button.interactable = true;
    }
}
