using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class BaseObject : MonoBehaviour {

    public delegate void DestroyBaseObject(BaseObject baseObject);
    public event DestroyBaseObject OnDestroyBaseObject;
    
    protected  Vector2Int coord;
    public BaseTile tile;

   
    // Use this for initialization
	void Start ()
    {
        coord = tile.PositionOnGrid;
     

    }
    public void SetCoord(Vector2Int coordToSet)
    {
        coord = coordToSet;
    }

    // Update is called once per frame
    void Update () {
		
	}
    private void OnDestroy()
    {
        if(OnDestroyBaseObject!=null)
        OnDestroyBaseObject(this);
    }
}
