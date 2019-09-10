using System;
using System.Collections.Generic;
using UnityEngine;

public class Exit : BaseTile {

    // Use this for initialization
    public static event Action onFoundPlayer;
    public event Action OnAddUnittOnTile;

    private void Awake()
    {
        OnAddUnittOnTile += FindPlayerOnTile;
        onFoundPlayer += FinishLevel;
    }


    public override void AddUnitOnTile(BaseUnit unitToAdd)
    {

        base.AddUnitOnTile(unitToAdd);
        OnAddUnittOnTile();
    }

    // Update is called once per frame
    void Update () {
		
	}
    void FindPlayerOnTile()
    {
        foreach (var unit in baseUnits)
        {
            
            if( unit.GetType() == typeof(Player))
            {
                Debug.Log("player on exit");
               onFoundPlayer();
                break;
            }
        }
      
       
    }
    void FinishLevel()
    {
        FindObjectOfType(typeof(Canvas));
        Debug.Log("Done");
        Debug.Log("Done");
        Debug.Log("Done");

    }
}
