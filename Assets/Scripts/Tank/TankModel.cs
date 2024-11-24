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
            {"PH", 10},
            {"Algae", 10}
        };
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
    

}
}
