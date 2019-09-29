using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall :BaseTile {

    // Use this for initialization
    public override bool OccupieRequest()
    {
        return false;
    }

    public override void AddObjectToTile(BaseObject objectToAdd)
    {
        Debug.Log("InValid AddObjectToTile:" + objectToAdd);
    }
    public override void AddUnitOnTile(BaseUnit unitToAdd)
    {
        Debug.Log("InValid AddUnitOnTile:" + unitToAdd);
    }
    public override void RemoveObjectOnTile(BaseObject objectToRemove)
    {
        Debug.Log("InValid RemoveObjectOnTile:" + objectToRemove);
    }
    public override void RemovUnitOnTile(BaseUnit unitToRemove)
    {
        Debug.Log("InValid RemovUnitOnTile:" + unitToRemove);
    }
    public override bool CanBeEntered()
    {
        return false;
    }
}
