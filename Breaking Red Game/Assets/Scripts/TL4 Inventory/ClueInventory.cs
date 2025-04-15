using UnityEngine;
using UnityEngine.UI;

public class ClueInventory: InventoryBase
{
    public override int v_getInventorySize(int cluesGathered)
    {
        Debug.Log($"cluesGathered = " + cluesGathered);
        if (cluesGathered >= 4)
        {
            Debug.Log("returning 5");
            return 5;
        }
        else if (cluesGathered >= 2)
        {
            return 4;
        }
        return base.v_getInventorySize(cluesGathered);
    }
}
