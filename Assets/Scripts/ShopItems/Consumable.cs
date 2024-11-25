using Tank;
using UnityEngine;

public class Consumable : Item
{
    public float fishStatMod, fishHungerMod, tempMod, PHMod, CO2Mod, algaeMod, wasteMod;

    public override void UpdateSelf(ref TankModel tankModel)
    {

        tankModel.myItems.Remove(this);
        Destroy(this.gameObject);
    }

}
