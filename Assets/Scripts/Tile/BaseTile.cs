using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BaseTile : MonoBehaviour {


    public BaseObject[] baseObject;
    public BaseUnit[] baseUnit;

    //Vector2 positionOnGrid;
    public Vector2 PositionOnGrid
    {
        get;// { return positionOnGrid; }
        set; //{ positionOnGrid = value; }
    }


    bool occupied;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
