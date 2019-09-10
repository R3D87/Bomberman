using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PaintTile : MonoBehaviour {

    [SerializeField]
    int positionX;
    [SerializeField]
    int positionY;
    [SerializeField]
    PaintType paintTileType;

    public UnityAction DestroyPaintTile;
    
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

	
	// Update is called once per frame
	void Update () {
		
	}

    
}
