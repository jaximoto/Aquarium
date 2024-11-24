using UnityEngine;
using System.Collections.Generic;

public class PlayerStatsController : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public PlayerStatsModel model;

    void Awake()
    {
        this.model = new PlayerStatsModel();
        //ShopController.OnBuyPlayer += model.Buy();
        ShopController.OnBuyPlayer += model.Buy;
        // TODO subscribe the model buy method to the shop OnBuy event
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CanBuy(Dictionary<string, int> costs)
    {
        foreach(KeyValuePair<string, int> pair in costs)
        {
            if (this.model.model[pair.Key] < pair.Value)
                return false;
        }
        return true;
    }

}
