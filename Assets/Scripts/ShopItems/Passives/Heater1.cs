using Tank;
using UnityEngine;

public class Heater1 : Passive
{

    public override void UpdateTank(ref TankModel tankModel)
    {
        tankModel.IncrementStat("Temp", this.tempMod);
    }
    
}
