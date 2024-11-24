
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
}
