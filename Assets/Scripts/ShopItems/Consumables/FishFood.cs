using Tank;
using UnityEngine;

public class FishFood : Consumable
{
    public override void UpdateSelf(ref TankModel tankModel)
    {
        Destroy(gameObject);
        return;
    }


    public override void UpdateTank(ref TankModel tankModel)
    {
        foreach (Fish fish in tankModel.myFish)
        {
            fish.hunger += fishHungerMod;
            Debug.Log(fish.GetType().ToString() + fish.hunger);
        }

    }
}
