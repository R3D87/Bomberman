using System;
using System.Collections.Generic;
using UnityEngine;

public class Exit : BaseTile {

    public static event Action onFoundPlayer;
    public event Action OnAddUnittOnTile;

    private void Awake()
    {
        OnAddUnittOnTile += FindPlayerOnTile;
    }


    public override void AddUnitOnTile(BaseUnit unitToAdd)
    {
        base.AddUnitOnTile(unitToAdd);
        OnAddUnittOnTile();
    }

    void FindPlayerOnTile()
    {
        foreach (var unit in baseUnits)
        {          
            if(unit.GetType() == typeof(Player))
            {
               onFoundPlayer();
               break;
            }
        } 
    }
}
