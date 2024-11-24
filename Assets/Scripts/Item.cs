using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    public Dictionary<string, int> resourceCosts = new Dictionary<string, int>();
    public float tempModifier, PHModifier, CO2Modifier, algaeModifier, wasteModifier;
}
