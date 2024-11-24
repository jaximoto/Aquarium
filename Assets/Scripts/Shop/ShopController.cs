using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ShopController : MonoBehaviour
{
    ShopModel model;
    public static event Action<Dictionary<string, int>> OnBuyPlayer;
    public static event Action<Item> OnBuyTank;
    
    public void Buy(string itemName)
    {
        OnBuyPlayer?.Invoke(model.resourceMap[itemName]);
        Item i = ItemFactory(itemName);
        OnBuyTank?.Invoke(i);
    }
    

    private void Awake()
    {
        this.model = new();    
    }

    public Item ItemFactory(string itemName)
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Items/" + itemName);
        
        if (itemName == "FishPrime")
        {
            return Instantiate(prefab).gameObject.GetComponent<FishPrime>();
        }
        
        return null;
    }
}
