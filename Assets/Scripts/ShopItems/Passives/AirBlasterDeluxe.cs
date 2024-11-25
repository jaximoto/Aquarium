using Tank;
using UnityEngine;

public class AirBlasterDeluxe : Passive
{
    public override void UpdateSelf()
    {
        return;
    }


    public override void UpdateTank(ref TankModel tankModel)
    {
        tankModel.IncrementStat("CO2", this.CO2Mod);
    }
}
