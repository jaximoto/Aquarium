using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class ShopModel
{
    public Dictionary<string, Dictionary<string, int>> resourceMap;
    IList<string> shopAvailable = new List<string>();
    // <ItemName : <ResourceName : ResourceValue>>

    public ShopModel()
    {
        resourceMap = new()
        {
            {"FishPrime", makeDict(new int[]{20, 5, 3}) },
            {"Cove", makeDict(new int[]{20, 0, 0}) },
            {"Kelp", makeDict(new int[]{10, 0, 0}) },
            {"FishFood", makeDict(new int[]{10, 0, 0}) },
            {"Fishbo", makeDict(new int[]{10, 0, 0}) },
            {"BubblerV1", makeDict(new int[]{30, 0, 0}) },
            {"BubblerV2", makeDict(new int[]{60, 0, 0}) },
            {"AirBlaster", makeDict(new int[]{120, 0, 0}) },
            {"AirBlasterDeluxe", makeDict(new int[]{180, 0, 0}) },
            {"Filtercano", makeDict(new int[]{250, 0, 10}) },
            {"Heater1", makeDict(new int[]{20, 0, 0}) },
            {"Heater2", makeDict(new int[]{40, 0, 0}) },
            {"Heater3", makeDict(new int[]{100, 0, 0}) },
            {"Cooler", makeDict(new int[]{30, 0, 0}) }

        };

    }
    private Dictionary<string, int> makeDict(int[] resource)
    {
        return new Dictionary<string, int>
            {
                { "money", resource[0] },
                { "fishOil", resource[1] },
                { "coral" , resource[2] }
            };
}
   


}
