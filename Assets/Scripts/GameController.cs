using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using System;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public GameObject ActiveTank;
    public int currentIndex = 1;
    public GameObject LeftTank, MiddleTank, RightTank;
    public List<GameObject> tankList;

    public static event Action TankChanged;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        tankList = new() { LeftTank, MiddleTank, RightTank };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // TODOOOO Both funcs have to have event to call change tank event, make current tank turn tankstats off and all fish etc, and then turns on the tank we are on
    public void MoveRight()
    {
        Debug.Log($"Current Tank index = {currentIndex}");
        ActiveTank.SetActive(false);
        int newIndex = ++currentIndex;
        if (newIndex >= tankList.Count)
        {
            currentIndex = 0;
            ActiveTank = tankList[currentIndex];
        }
        else
        {
            currentIndex = newIndex;
            ActiveTank = tankList[currentIndex];
        }
        TankChanged?.Invoke();
        ActiveTank.SetActive(true);
    }

    public void MoveLeft()
    {
        Debug.Log($"Current Tank index = {currentIndex}");
        ActiveTank.SetActive(false);
        int newIndex = --currentIndex;
        if (newIndex < 0)
        {
            currentIndex = tankList.Count - 1;
            ActiveTank = tankList[currentIndex];
        }
        else
        {
            currentIndex = newIndex;
            ActiveTank = tankList[currentIndex];
        }
        TankChanged?.Invoke();
        ActiveTank.SetActive(true);
    }

   
    public void UpdateCurrTankUI()
    {

    }

}
