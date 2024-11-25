using UnityEngine;
using Tank;

public class PurpleFish : Fish
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
        tankModel.IncrementStat("Temp", this.outTemp);
    }


    private void Update()
    {
        Move();
    }
    
}
