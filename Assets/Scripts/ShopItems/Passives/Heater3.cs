using Tank;
using UnityEngine;

public class Heater3 : Passive
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
