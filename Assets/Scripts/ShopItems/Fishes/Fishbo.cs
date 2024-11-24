using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Tank;

public class Fishbo : Fish
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
        tankModel.IncrementStat("PH", this.outPH);
    }


    private void Update()
    {
        Move();
    }

}