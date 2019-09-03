using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BaseTile : MonoBehaviour {



    protected List<BaseObject> baseObjects = new List<BaseObject>();
    protected List<BaseUnit> baseUnits = new List<BaseUnit>();
    public Vector2Int PositionOnGrid { get; set; }
    bool occupied;
    public GameBoard board;

    

    virtual public bool OccupieRequest()
    {
        return true;
    }

    virtual public void AddObjectToTile(BaseObject objectToAdd)
    {
        baseObjects.Add(objectToAdd);
        objectToAdd.tile = this;
        objectToAdd.OnDestroyBaseObject += RemoveObjectOnTile;
    }

    virtual public void AddUnitOnTile(BaseUnit unitToAdd)
    {
        unitToAdd.tile = this;
        baseUnits.Add(unitToAdd);
    }

    virtual public void RemoveObjectOnTile(BaseObject objectToRemove)
    {
        objectToRemove.OnDestroyBaseObject -= RemoveObjectOnTile;
        baseObjects.Remove(objectToRemove);

    }

    virtual public void RemovUnitOnTile(BaseUnit unitToRemove)
    {
        baseUnits.Remove(unitToRemove);
    }

    Vector2Int ConvertDirectionTo2dCoord(int xDir, int yDir)
    {
        return new Vector2Int(PositionOnGrid.x + xDir, PositionOnGrid.y + yDir);
    }
    public BaseTile GetNeigbourInDirection(int x, int y)
    {
        Vector2Int Coord = ConvertDirectionTo2dCoord(x, y);
        Debug.Log(Coord);
      
        return board.GetNeighbourTile(Coord.x, Coord.y);
    }
    public Vector3 GetNeighborLocation(BaseTile tile)
    {
        return tile.transform.position;
    }
 
    // Update is called once per frame
    void Update () {
		
	}
}
