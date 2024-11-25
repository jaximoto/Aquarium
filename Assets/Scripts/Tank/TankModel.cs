using UnityEngine;
using System.Collections.Generic;
using static Fish;
using static Item;

namespace Tank
{
public class TankModel 
{

    //TODO: make a stat abstract class that checks limits when stats are modified
    public Dictionary<string, float> statsDict;
    public Dictionary<string, float> statsMaxDict;

    public List<Fish> myFish;
    
    public List<Item> myItems;

    // All stats are floats because fuck you
    public TankModel()
    {
        statsDict = new Dictionary<string, float> 
        {
            // random values for now
            {"CO2", 100},
            {"Temp", 5},
            {"Waste", 10},
            {"PH", 5},
            {"Algae", 10}
        };

        statsMaxDict = new Dictionary<string, float> 
        {
            // random values for now
            {"CO2", 1000},
            {"Temp", 50},
            {"Waste", 100},
            {"PH", 14},
            {"Algae", 100}
        };

        myItems = new List<Item>();
        myFish = new List<Fish>();
    }


    public float GetStat(string name)
    {
        Debug.Assert(statsDict.ContainsKey(name));
        return statsDict[name];
    }


    public void SetStat(string name, float val)
    {
        Debug.Assert(statsDict.ContainsKey(name));
        statsDict[name] = val;
    }
    
    public void IncrementStat(string name, float val)
    {
        Debug.Assert(statsDict.ContainsKey(name));
        statsDict[name] += val;
        if (statsDict[name] < 0)
            {
                statsDict[name] = 0;
            }

        
    }

}
}
