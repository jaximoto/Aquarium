using Tank;
using UnityEngine;

public class Cove : Passive
{
    public override void UpdateSelf()
    {
        return;
    }


    public override void UpdateTank(ref TankModel tankModel)
    {
        foreach (Fish fish in tankModel.myFish)
        {
            fish.fishStatus += fishStatMod;
            Debug.Log(fish.GetType().ToString() +  fish.fishStatus);
        }
        
    }
}
