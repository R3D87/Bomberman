using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TypTile", menuName = "ScriptableObjects/TypTile", order = 1)]
public class TileTypes : ScriptableObject
{
    public TileType tileType;
    public GameObject baseTile;
    public GameObject baseObject;
}
