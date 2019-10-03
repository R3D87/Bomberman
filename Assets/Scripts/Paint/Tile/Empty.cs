using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Empty : BaseTile {

    int AmountDamage = 1;

    public override void AddUnitOnTile(BaseUnit unitToAdd)
    {
        base.AddUnitOnTile(unitToAdd);

        if (IsExplotionExecutingOnTile())
        {
            if (unitToAdd.GetComponent<IDamage>() != null)
            {
                unitToAdd.GetComponent<IDamage>().TakeDamage(AmountDamage);             
            }
        }
    }

    bool IsExplotionExecutingOnTile()
    {
        return baseObjects.Select(x => x.GetType() == typeof(ExplosionEffect)).Any();
    }
}
