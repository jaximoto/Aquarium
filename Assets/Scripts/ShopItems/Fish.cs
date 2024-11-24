
using Tank;
using UnityEngine;

public class Fish : Item
{
    public float health, hunger;
    public float pTemp, pPH, pC02, pAlgaeContent, pWaste;
    public float outTemp, outPH, outC02, outAlgaeContent, outWaste;
    //go over fish stats and preferences.
    //make an algorithm
    enum Status
    {
        dead,
        dying,
        unhealthy,
        healthy,
        plusUltra

    }

    public virtual void Move()
    {
        Debug.Assert(false);
    }

    public override void UpdateTank(ref TankModel tankModel)
    {
        return; //TODO algorithm
    }

    public override void UpdateSelf()
    {
        return; //TODO algorithm
    }

    /* Jack MiHoff
    public void Init(float pTemp, float pPH, float pC02, float pAlgaeContent, float pWaste, float outTemp, float outPH, float outC02, float outAlgaeContent, float outWaste)
    {
        
    }
    */
}
