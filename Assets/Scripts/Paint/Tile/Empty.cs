using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Empty : BaseTile {


    public override void AddUnitOnTile(BaseUnit unitToAdd)
    {
        base.AddUnitOnTile(unitToAdd);

        if (IsExplotionExecutingOnTile())
        {
            if (unitToAdd.GetComponent<IDamage>() != null)
            {
             //   unitToAdd.GetComponent<IDamage>().TakeDamage(1);
                Debug.Log("Take damage form Fire");
            }

        }

    }

    bool IsExplotionExecutingOnTile()
    {
        return baseObjects.Select(x => x.GetType() == typeof(ExplosionEffect)).Any();
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
