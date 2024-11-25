using Tank;
using UnityEngine;

public class BubblerV1 : Passive
{
    public override void UpdateTank(ref TankModel tankModel)
    {
        tankModel.IncrementStat("CO2", this.CO2Mod);
    }
}
