using System;
using System.Collections.Generic;
using Unity.VisualScripting;
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
            GameObject game = Instantiate(prefab).gameObject;
            FishPrime i = game.gameObject.GetComponent<FishPrime>();
            game.transform.SetParent(GameController.Instance.ActiveTank.transform, true);
            return i;
        }
        else if (itemName == "Cove")
        {
            GameObject game = Instantiate(prefab).gameObject;
            Cove i = game.gameObject.GetComponent<Cove>();
            game.transform.SetParent(GameController.Instance.ActiveTank.transform, true);
            return i;
        }
        else if (itemName == "Kelp")
        {
            GameObject game = Instantiate(prefab).gameObject;
            Kelp i = game.gameObject.GetComponent<Kelp>();
            game.transform.SetParent(GameController.Instance.ActiveTank.transform, true);
            return i;
        }

        return null;
    }
}
