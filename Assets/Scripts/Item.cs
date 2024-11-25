using System.Collections.Generic;
using UnityEngine;
using Tank;

public class Item : MonoBehaviour
{
    public Dictionary<string, int> resourceCosts = new Dictionary<string, int>();

    public virtual void UpdateTank(ref TankModel tankModel)
    { Debug.Assert(false); }
    public virtual void UpdateSelf(ref TankModel tankModel)
    { Debug.Assert(false); }

}
