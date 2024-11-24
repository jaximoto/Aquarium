
using Tank;
using UnityEngine;

public class Fish : Item
{
    public float health, hunger;
    public float pTemp, pPH, pC02, pAlgaeContent, pWaste;
    public float outTemp, outPH, outC02, outAlgaeContent, outWaste;
    //go over fish stats and preferences.
    //make an algorithm
    public float fishStatus = 0.65f;
    enum Status
    {
        dead, //fish is dead, fish health points == 0
        dying, //fish is losing health, fishStatus == [0,0.25]
        unhealthy, //fish is holdin steady & give 1/2 gold, fishStatus == [0.25,0.5]
        healthy, //fish is healin & give 100% gold, fishStatus == [0.5,0.75]
        plusUltra //fish is healin & givin bonus resources == [0.75,1]

    }

    public virtual void Move()
    {
        Debug.Assert(false);
    }

}
