using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.XR;

public class PlayerStatsModel {


    public Dictionary<string, int> model = new();

    public PlayerStatsModel()
    {
        // Extend resources in this constructor when adding new ones
        model["money"] = 500; 
        model["fishOil"] = 500; 
        model["coral"] = 500; 
    }

    public void ChangeStat(string type, int value)
    {
        Debug.Log(type);
        Debug.Log(value);
        Assert.IsTrue(model.ContainsKey(type));
        model[type] += value;
    }

    public void Buy(Dictionary<string, int> resources)
    {
        foreach(string s in resources.Keys)
        {
            ChangeStat(s, -resources[s]);
        }   
    }



}
