using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Tank;

public class FishPrime : Fish
{
    public override void Move()
    {
        return;
    }

    public override void UpdateSelf()
    {
        return;
    }


    public override void UpdateTank(ref TankModel tankModel)
    {
        tankModel.IncrementStat("Temp", this.outTemp);
    }


    private void Update()
    {
        Move();
    }

}
