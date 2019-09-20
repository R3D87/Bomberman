using System;
using System.Collections.Generic;
using UnityEngine;



public class BaseTile : MonoBehaviour, IDamage {



    public event Action<BaseUnit> OnEnterUnitOnTile;
    public event Action<PowerUp> OnTakePowerUp;

    protected List<BaseObject> baseObjects = new List<BaseObject>();
    protected List<BaseUnit> baseUnits = new List<BaseUnit>();
    public Vector2Int PositionOnGrid { get; set; }
    bool occupied;
    public GameBoard board;


    private void OnEnable()
    {
        OnEnterUnitOnTile += FindObjectToTake;
       
    }

    public  bool HasTileOccupied()
    {

            foreach (BaseObject itemObject in baseObjects)
            {

                if (itemObject.GetType() == typeof(Obstacle))
                {
                    return true;
                }
            
            }
        
            return false;
    }

    public bool HasTilePowerUp()
    {

        foreach (BaseObject itemObject in baseObjects)
        {

            if (itemObject.GetType() == typeof(PowerUp))
            {
                return true;
            }

        }

        return false;
    }

    virtual public bool OccupieRequest()
    {
        return true;
    }

    virtual public void AddObjectToTile(BaseObject objectToAdd)
    {

        if (objectToAdd == null)
            return;
        baseObjects.Add(objectToAdd);
        objectToAdd.tile = this;
        objectToAdd.SetCoord(PositionOnGrid);
        objectToAdd.OnDestroyBaseObject += RemoveObjectOnTile;
       
    }

    virtual public void AddUnitOnTile(BaseUnit unitToAdd)
    {
        if (unitToAdd == null)
            return;
        OnTakePowerUp += unitToAdd.TakePowerUp;
        unitToAdd.SetBaseTile(this);
        unitToAdd.SetCoord(PositionOnGrid);
        baseUnits.Add(unitToAdd);
        OnEnterUnitOnTile(unitToAdd);
        unitToAdd.OnDestroyBaseUnit += RemovUnitOnTile;
       
    }

    virtual public void RemoveObjectOnTile(BaseObject objectToRemove)
    {
        objectToRemove.OnDestroyBaseObject -= RemoveObjectOnTile;
        baseObjects.Remove(objectToRemove);
    }

    virtual public void RemovUnitOnTile(BaseUnit unitToRemove)
    {
        unitToRemove.OnDestroyBaseUnit -= RemovUnitOnTile;
        OnTakePowerUp -= unitToRemove.TakePowerUp;
        baseUnits.Remove(unitToRemove);
    }
    
    public BaseTile GetNeigbourInDirection(int x, int y)
    {
        Vector2Int Coord = ConvertDirectionTo2dCoord(x, y);
        //Debug.Log(Coord);
      
        return board.GetNeighbourTile(Coord.x, Coord.y);
    }

    public Vector3 GetLocation()
    {
        return transform.position;
    }

    Vector2Int ConvertDirectionTo2dCoord(int xDir, int yDir)
    {
        return new Vector2Int(PositionOnGrid.x + xDir, PositionOnGrid.y + yDir);
    }

    void FindObjectToTake(BaseUnit unit)
    {
        PowerUp power = FindPowerUp();
        if (power != null)
        {
            Debug.Log(power.name);
            OnTakePowerUp(power);
            Destroy(FindPowerUp().gameObject,0.1f);
        }
    }
    PowerUp FindPowerUp()
    {
        foreach  (BaseObject objectOnTile in baseObjects)
        {
            if (objectOnTile.GetType() == typeof(PowerUp))
            {
                
                return objectOnTile as PowerUp;
            }
        }
        return null;
    }

    public void TakeDamage(int damage)
    {
        List<BaseObject> ObjectsListForTakeDamage = new List<BaseObject>(baseObjects);
        foreach (var item in ObjectsListForTakeDamage)
        {
           
            if (item.GetComponent<IDamage>() != null)
            {
                item.GetComponent<IDamage>().TakeDamage(damage);
            }
        }

        List<BaseUnit> UnitsListForTakeDamage = new List<BaseUnit>(baseUnits);
        foreach (var item in UnitsListForTakeDamage)
        {
            if (item.GetComponent<IDamage>() != null)
            {
                item.GetComponent<IDamage>().TakeDamage(damage);
            }
        }
         
    }
}
