using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseUnit : MonoBehaviour {

    // Use this for initialization
    public delegate void MovePreperation(Vector2Int coord);
    public static event MovePreperation OnMovePreparation;

    public delegate void DestroyBaseUnit(BaseUnit baseUnit);
    public event DestroyBaseUnit OnDestroyBaseUnit;

    Coroutine test;
    protected BaseTile tile;
    protected Vector2Int coord;

    public void SetBaseTile(BaseTile tileToSet)
    {
        tile = tileToSet;
    }
    private void Start()
    {
        coord = tile.PositionOnGrid;
      
    }
    public void SetCoord(Vector2Int coordToSet)
    {
        coord = coordToSet;
    }

    protected bool HasMovePremmission(int xDir, int yDir)
    {
        BaseTile tileToTyp = GetNeigbourInDirection(xDir, yDir);

        if (GetTileType(tileToTyp) == typeof(Wall))
            return false;

        if (tileToTyp.HasTileOccupied())
            return false;

        return true;
    }
    Type GetTileType(BaseTile basetile) 
    {  
        Type type = basetile.GetType();
        return type;
    }


    BaseTile GetNeigbourInDirection(int xDir, int yDir)
    {
        return tile.GetNeigbourInDirection(xDir, yDir);

    }
    protected void Movement(int xDir, int yDir)
    {
        if (HasMovePremmission(xDir, yDir))
        {
            Move(xDir, yDir);
        }
    }
    protected bool Move(int xDir, int yDir)
    {
        if (test==null) {
            BaseTile tempBaseTile = GetNeigbourInDirection(xDir, yDir);
            Vector3 TargetPosition = tempBaseTile.GetLocation();
            test = StartCoroutine(SmoothMovement(TargetPosition, 0.1f));
            tile.RemovUnitOnTile(this);

            tile = tempBaseTile;
            tile.AddUnitOnTile(this);

            return true;
        }
        return false;
      

    }

    protected IEnumerator SmoothMovement( Vector3 target, float duration)
    {
        float progress = 0;
        Vector3 StartPosition = transform.position;


        while (progress<=duration)
        {
            progress = progress + Time.deltaTime;
            float procent = Mathf.Clamp01(progress / duration);

            gameObject.transform.position= Vector3.Lerp(transform.position, target, procent);
   
            yield return null;
        }
        test = null;
       
    }
   private void OnDestroy()
    {
        if (OnDestroyBaseUnit != null)
            OnDestroyBaseUnit(this);
    }
}
