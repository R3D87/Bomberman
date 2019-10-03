using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseObject : MonoBehaviour {

    public delegate void DestroyBaseObject(BaseObject baseObject);
    public event DestroyBaseObject OnDestroyBaseObject;
    protected  Vector2Int coord;
    public BaseTile tile;

	void Start ()
    {
        coord = tile.PositionOnGrid;
    }

    public void SetCoord(Vector2Int coordToSet)
    {
        coord = coordToSet;
    }

    virtual public void OnDestroy()
    {
        tile.RemoveObjectOnTile(this);

        if (OnDestroyBaseObject!=null)
             OnDestroyBaseObject(this);
               
    }
}
