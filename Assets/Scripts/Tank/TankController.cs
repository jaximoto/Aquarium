using UnityEngine;
using System.Collections.Generic;

using static Item;
using static DayCycle;

namespace Tank
{
    public class TankController : MonoBehaviour
    {

        public TankModel model = null;
        public TankView view;
        public bool isActive;

        private void OnEnable()
        {
            Activate();
            if (model != null)
                view.RenderAllFish(model.myFish);
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
                view.RenderFishUI(f);
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

            foreach(Item item in model.myItems)
            {
                Debug.Log(item.GetType().ToString());
                item.UpdateTank(ref this.model);
               
            }
            foreach (Item item in model.myItems)
            {
                item.UpdateSelf(ref this.model);
            }

        }


        // Update is called once per frame
        void Update()
        {
            view.RenderTankUI(model.statsDict, model.statsMaxDict);
            //view.RenderTankStats(model.statsDict);
            if (isActive)
                view.UpdateFishUI(model.myFish);
        }

    }
}
