using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.XR;

public class PlayerStatsModel {


    Dictionary<string, int> model = new();

    public PlayerStatsModel()
    {
        // Extend resources in this constructor when adding new ones
        model["money"] = 200; 
    }

    public void ChangeStat(string type, int value)
    {
        Assert.IsTrue(model.ContainsKey(type));
        model[type] += value;
    }

    public void Buy(Item i)
    {
       
        foreach(string s in i.resourceCosts.Keys)
        {
            ChangeStat(s, i.resourceCosts[s]);
        }
        
    }
}
