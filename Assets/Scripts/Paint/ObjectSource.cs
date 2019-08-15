using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSource : MonoBehaviour {

    [SerializeField]
   public int counter=10;
    public GameObject source;

    public PaintType paintType;

    public GameObject Source
    { get; set; }


    private void Start()
    {
        source.GetComponent<PaintTile>().PaintTileType = paintType;
 
    }

}
