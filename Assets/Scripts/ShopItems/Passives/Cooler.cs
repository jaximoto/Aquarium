using Tank;
using UnityEngine;

public class Cooler: Passive
{

    public override void UpdateSelf()
    {
        return;
    }


    public override void UpdateTank(ref TankModel tankModel)
    {
        tankModel.IncrementStat("Temp", this.tempMod);
    }
    
}