using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall :BaseTile {

    public override bool OccupieRequest() { return false; }

    public override void AddObjectToTile(BaseObject objectToAdd) { }

    public override void AddUnitOnTile(BaseUnit unitToAdd) { }

    public override void RemoveObjectOnTile(BaseObject objectToRemove) { }

    public override void RemovUnitOnTile(BaseUnit unitToRemove) { }

    public override bool CanBeEntered() { return false; }
}
