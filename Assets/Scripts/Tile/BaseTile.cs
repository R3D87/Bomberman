using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BaseTile : MonoBehaviour {


    protected List<BaseObject> baseObjects = new List<BaseObject>();
    protected List<BaseUnit> baseUnits = new List<BaseUnit>();
    public Vector2Int PositionOnGrid { get; set; }
    bool occupied;
    GameBoard board;


   virtual public void AddObjectToTile(BaseObject objectToAdd)
    {
        baseObjects.Add(objectToAdd);
    }

    virtual public void AddUnitOnTile(BaseUnit unitToAdd)
    {
        baseUnits.Add(unitToAdd);
    }

    virtual public void RemoveObjectOnTile(BaseObject objectToRemove)
    {
        baseObjects.Remove(objectToRemove);
    }

    virtual public void RemovUnitOnTile(BaseUnit unitToRemove)
    {
        baseUnits.Remove(unitToRemove);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
