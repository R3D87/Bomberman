
using UnityEngine;


[CreateAssetMenu(fileName = "TileTyps", menuName = "ScriptableObjects/TypTile", order = 1)]
public class TileTypes : ScriptableObject {

    public PaintType tileType;
    public GameObject tile;
}
