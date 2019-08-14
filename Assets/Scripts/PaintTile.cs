using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintTile : MonoBehaviour {

    int positionX;
    int positionY;
    PaintType paintTileType;

    
    public void PaintTileConstructor(int PositionX, int PositionY, PaintType type)
    {
        positionX = PositionX;
        positionY = PositionY;
        paintTileType = type;
    }

    public void PaintTileConstructor(PaintType type)
    {
        paintTileType = type;
    }

    public int PositionX
    {
        get { return positionX; } set { positionX = value; }
    }
    public int PositionY
    {
        get{ return positionY;} set{ positionY = value;}
    }
    public PaintType PaintTileType
    {
        get { return paintTileType; } set { paintTileType = value;  }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
