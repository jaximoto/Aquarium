using UnityEngine;
using Tank;

public class GreenFish : Fish
{
   

    public override void UpdateSelf()
    {
        return;
    }


    public override void UpdateTank(ref TankModel tankModel)
    {
        tankModel.IncrementStat("Algae", this.outAlgaeContent);
        tankModel.IncrementStat("Waste", this.outWaste);
    }


   
    
}
