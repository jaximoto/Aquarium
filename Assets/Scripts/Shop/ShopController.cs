using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

using static PlayerStatsController;

public class ShopController : MonoBehaviour
{
    ShopModel model;
    public static event Action<Dictionary<string, int>> OnBuyPlayer;
    public static event Action<Item> OnBuyTank;

    public PlayerStatsController playerController;
    
    public void Buy(string itemName)
    {
        if (playerController.CanBuy(model.resourceMap[itemName]))
        {
            OnBuyPlayer?.Invoke(model.resourceMap[itemName]);
            Item i = ItemFactory(itemName);
            OnBuyTank?.Invoke(i);
        }
        //else call shop view to say "not enough money"
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
        else if (itemName == "FishFood")
        {
            GameObject game = Instantiate(prefab).gameObject;
            FishFood i = game.gameObject.GetComponent<FishFood>();
            game.transform.SetParent(GameController.Instance.ActiveTank.transform, true);
            return i;
        }
        else if (itemName == "Fishbo")
        {
            GameObject game = Instantiate(prefab).gameObject;
            Fishbo i = game.gameObject.GetComponent<Fishbo>();
            game.transform.SetParent(GameController.Instance.ActiveTank.transform, true);
            return i;
        }
        else if (itemName == "BubblerV1")
        {
            GameObject game = Instantiate(prefab).gameObject;
            BubblerV1 i = game.gameObject.GetComponent<BubblerV1>();
            game.transform.SetParent(GameController.Instance.ActiveTank.transform, true);
            return i;
        }

        else if (itemName == "BubblerV2")
        {
            GameObject game = Instantiate(prefab).gameObject;
            BubblerV2 i = game.gameObject.GetComponent<BubblerV2>();
            game.transform.SetParent(GameController.Instance.ActiveTank.transform, true);
            return i;
        }

        else if (itemName == "AirBlaster")
        {
            GameObject game = Instantiate(prefab).gameObject;
            AirBlaster i = game.gameObject.GetComponent<AirBlaster>();
            game.transform.SetParent(GameController.Instance.ActiveTank.transform, true);
            return i;
        }

        else if (itemName == "AirBlasterDeluxe")
        {
            GameObject game = Instantiate(prefab).gameObject;
            AirBlasterDeluxe i = game.gameObject.GetComponent<AirBlasterDeluxe>();
            game.transform.SetParent(GameController.Instance.ActiveTank.transform, true);
            return i;
        }

        else if (itemName == "Filtercano")
        {
            GameObject game = Instantiate(prefab).gameObject;
            Filtercano i = game.gameObject.GetComponent<Filtercano>();
            game.transform.SetParent(GameController.Instance.ActiveTank.transform, true);
            return i;
        }

        else if (itemName == "Heater1")
        {
            GameObject game = Instantiate(prefab).gameObject;
            Heater1 i = game.gameObject.GetComponent<Heater1>();
            game.transform.SetParent(GameController.Instance.ActiveTank.transform, true);
            return i;
        }
        else if (itemName == "Heater2")
        {
            GameObject game = Instantiate(prefab).gameObject;
            Heater2 i = game.gameObject.GetComponent<Heater2>();
            game.transform.SetParent(GameController.Instance.ActiveTank.transform, true);
            return i;
        }
        else if (itemName == "Heater3")
        {
            GameObject game = Instantiate(prefab).gameObject;
            Heater3 i = game.gameObject.GetComponent<Heater3>();
            game.transform.SetParent(GameController.Instance.ActiveTank.transform, true);
            return i;
        }
        else if (itemName == "Cooler")
        {
            GameObject game = Instantiate(prefab).gameObject;
            Cooler i = game.gameObject.GetComponent<Cooler>();
            game.transform.SetParent(GameController.Instance.ActiveTank.transform, true);
            return i;
        }
        return null;
    }
}
