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
        public bool isActive;

        private void OnEnable()
        {
            Activate();
        }

        private void OnDisable()
        {
            Deactivate();
        }

        void Awake()
        {
            //ShopController.OnBuyTank += BuyHandler;
            DayCycle.OnNextDay += EndOfDayHandler;
        }


        void Activate()
        {
            isActive = true;
            ShopController.OnBuyTank += BuyHandler;
        }


        void Deactivate()
        {
            isActive = false;
            ShopController.OnBuyTank -= BuyHandler;
            view.KillAllFish(model.myFish);
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
        }


        void BuyPassiveHandler(Passive p)
        {
            //Check if one with same tag already
            List<Passive> toRemove = new();
            string ptag = p.gameObject.tag;
            foreach (Passive p2 in model.myPassives)
            {
                string p2tag = p2.gameObject.tag;
                //If yes, remove it
                if (p2tag==ptag)
                {
                    toRemove.Add(p2);
                }
            }

            foreach(Passive p2 in toRemove)
            {
                model.myPassives.Remove(p2);
                RemoveItem(p2);
            }

            model.myPassives.Add(p);
        }


        void BuyConsumableHandler(Consumable c)
        {

        }


        void BuyFishHandler(Fish f)
        {
            if (model.myFish.Count < model.maxFish)
            {
                model.myFish.Add(f);
            }
        }


        public void RemoveItem(Item item)
        {
            model.myItems.Remove(item);
            Destroy(item.gameObject);
        }


        public void EndOfDayHandler()
        {
            //This will iterate through each element of the myItems list
            //and it should call a ModifyTank method that eah item should override
            //that method will handle modifying the tank model
            //We also call another method that updates all item
            Debug.Log($"{gameObject.name}: Temp = {model.statsDict["Temp"]}");

            foreach(Item item in model.myItems)
            {
                Debug.Log(item.GetType().ToString());
                item.UpdateTank(ref this.model);
                item.UpdateSelf();
            }

            Debug.Log($"{gameObject.name}: C02 = {model.statsDict["CO2"]}");
            Debug.Log($"{gameObject.name}: Temp = {model.statsDict["Temp"]}");
        }


        // Update is called once per frame
        void Update()
        {
            view.RenderTankUI(model.statsDict, model.statsMaxDict);
            view.RenderFishUI(model.myFish);
            //view.RenderTankStats(model.statsDict);
        }

    }
}
