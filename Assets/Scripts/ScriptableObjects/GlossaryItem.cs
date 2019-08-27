using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GlossaryItem", menuName = "ScriptableObjects/GlossaryItem", order = 1)]
public class GlossaryItem : ScriptableObject
{
    public PaintType paintType;

    public TileType tileType;

    public ObjectType objectType;

    public UnitType unitType;
}




