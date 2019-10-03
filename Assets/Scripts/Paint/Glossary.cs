using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum PaintType
{
    Empty,
    Wall,
    Box,
    Exit,
    Player
};
public enum TileType
{
    Exit,
    Wall,
    Empty,
};

public enum ObjectType
{
    Box,
    Bomb,
    PowerUp,
    Empty
};
public enum UnitType
{
    Enemy,
    Player,
    Empty
};

public class Glossary : MonoBehaviour {

    [SerializeField]
    GlossaryItem[] glossaryItems;

    [SerializeField]
    TileTypes02[] tileTypes;

    [SerializeField]
    ObjectTypes[] objectTypes;

    [SerializeField]
    UnitTypes[] unitTyps;

    string[] pathToGlossaryItems = new[] { "Assets/Scripts/ScriptableObjects/Glossary" };
    string[] pathToTileItems = new[] { "Assets/Scripts/ScriptableObjects/Tiles" };
    string[] pathToObjectItems = new[]{"Assets/Scripts/ScriptableObjects/Objects"};
    string[] pathUnitItems = new[] { "Assets/Scripts/ScriptableObjects/Units" };

    private void Awake()
    {
        CreateGlossarySet();
        CreateTilesSet();
        CreateObjectsSet();
        CreateUnitsSet();
    }

    void CreateGlossarySet()
    {
        string[] guids;
        guids = AssetDatabase.FindAssets("Glossary", pathToGlossaryItems);
        int size = guids.Length;
        glossaryItems = new GlossaryItem[size];
        for (int i = 0; i < size; i++)
        {
            glossaryItems[i]= (GlossaryItem) AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guids[i]), typeof(ScriptableObject));
        }
    }

    void CreateTilesSet()
    {
        string[] guids;
        guids = AssetDatabase.FindAssets("Tile4",  pathToTileItems );
        int size = guids.Length;
        tileTypes = new TileTypes02[size];
        for (int i = 0; i < size; i++)
        {
            tileTypes[i] = (TileTypes02)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guids[i]), typeof(ScriptableObject));
        }
    }

    void CreateObjectsSet()
    {
        string[] guids;
        guids = AssetDatabase.FindAssets("Object", pathToObjectItems);
        int size = guids.Length;
        objectTypes = new ObjectTypes[size];
        for (int i = 0; i < size; i++)
        {
            objectTypes[i] = (ObjectTypes)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guids[i]), typeof(ScriptableObject));
        }
    }

    void CreateUnitsSet()
    {
        string[] guids;
        guids = AssetDatabase.FindAssets("Unit", pathUnitItems);
        int size = guids.Length;
        unitTyps = new UnitTypes[size];
        for (int i = 0; i < size; i++)
        {
            unitTyps[i] = (UnitTypes)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guids[i]), typeof(ScriptableObject));
        }
    }

    public GlossaryItem GetGlossaryItem(PaintType paint)
    {
        foreach (var item in glossaryItems)
        {
            if (item.paintType == paint)
            { return item; }
        }    
        return null;
    }

    public BaseTile GetPrefabTile(PaintType paint)
    {
        GlossaryItem glossary = GetGlossaryItem(paint);
        foreach (var typ in tileTypes)
        {
            if (typ.tileType == glossary.tileType)
                return typ.baseTile; 
        }
        return null;
    }

    public BaseObject GetPrefabObject(PaintType paint)
    {
        GlossaryItem glossary = GetGlossaryItem(paint);
        foreach (var typ in objectTypes)
        {
            if (typ.objectType == glossary.objectType)
                return typ.baseObject;
        }
        return null;
    }

    public  BaseUnit GetPrefabUnit(PaintType paint)
    {
        GlossaryItem glossary = GetGlossaryItem(paint);
        foreach (var typ in unitTyps)
        {
            if (typ.unitType == glossary.unitType)
                return typ.baseUnit;
        }
        return null;
    }
}
