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
            {"Fishbo", makeDict(new int[]{10, 0, 0}) }
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
