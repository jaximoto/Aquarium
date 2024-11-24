using UnityEngine;
using System.Collections.Generic;

using static Item;
using static DayCycle;

namespace Tank
{
public class TankController : MonoBehaviour
{

    public TankModel model;
    public TankView view;
    void Awake()
    {
        ShopController.OnBuyTank += BuyHandler;
        DayCycle.OnNextDay += EndOfDayHandler;
    }
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.model = new();
    }


    public void BuyHandler(Item i)
    {
        model.myItems.Add(i); 
        
        if (i is Passive)
        { 
            BuyPassiveHandler((Passive)i);
        }
        else if (i is Consumable)
        { 
            BuyConsumableHandler((Consumable)i);
        }
        else if (i is Fish)
        {
            BuyFishHandler((Fish)i);
        }

        //Anus
    }

    void BuyPassiveHandler(Passive p)
    {

    }

    void BuyConsumableHandler(Consumable c)
    {

    }


    void BuyFishHandler(Fish f)
    {
        model.myFish.Add((Fish)f);
    }


    public void EndOfDayHandler()
    {
        Debug.Log("GOt here");
        //This will iterate through each element of the myItems list
        //and it should call a ModifyTank method that eah item should override
        //that method will handle modifying the tank model
        //We also call another method that updates all item
        foreach(Item item in model.myItems)
        {
            item.UpdateTank(ref this.model);
            item.UpdateSelf();
        }

    }

    // Update is called once per frame
    void Update()
    {
        view.RenderTankStats(model.statsDict);
    }
}
}
