using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translator : MonoBehaviour {


    GameObject[,] blueprintGameboard;
    BaseTile[,] TileOnGrid;
    Glossary glossary;
    GameObject RootObject;
    List<Vector2> boxPaintCoord = new List<Vector2>();
    List<Vector2> emptyPaintCoord = new List<Vector2>();
    List<Vector2> wallPaintCoord = new List<Vector2>();
    List<Vector2> playerPaintCoord = new List<Vector2>();
    List<Vector2> exitPaintCoord = new List<Vector2>();
    int width;
    int height;

    public delegate void FinishBuildingAction();
    public event FinishBuildingAction OnFinishBuilding;

    public delegate void SendTranslationAction();
    public event SendTranslationAction OnSendTranslation;


    public delegate void FinishedSentTranslationAction();
    public event FinishedSentTranslationAction OnFinishedSentTranslation;




    private void Start()
    {
        
        gameObject.AddComponent<Glossary>();
        glossary = GetComponent<Glossary>();
        OnFinishBuilding += DestroyGlossary;
        OnFinishedSentTranslation += DisassembleTranslator;


        CreateRootObject();
    }


    public void GetBlueprint(GameObject[,] blueprint)
    {

        SetSizeBaseOnBluprint(blueprint.GetLength(0), blueprint.GetLength(1));

        blueprintGameboard = blueprint;
        TileOnGrid = new BaseTile[width, height];


        Debug.Log("Translator");


    }
    void SetSizeBaseOnBluprint(int tempWidth, int tempHeight)
    {
        width = tempWidth;
        height = tempHeight;
        Debug.Log("width: " + width + " height: " + height);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
           
            Translate();
        }

    }
    void AddExit()
    {
        List<int> Boxlist = new List<int>();
        for (int i = 0; i < width * height; i++)
        {
            int x = i / width;
            int y = i % width;
            if (blueprintGameboard[x, y].GetComponent<PaintTile>().PaintTileType == PaintType.Box)
            {
                Boxlist.Add(i);
            }
        }
        int max = Boxlist.Count;
        int idx = Random.Range(0, max);
        int value = Boxlist[idx];
    }

    Vector2Int ConvertCoordTo2D(int i)
    {
        Vector2Int position2D = new Vector2Int();
        
        position2D.x = i / width;
        position2D.y = i % width;

        return position2D;
    }

    void CollectData(PaintType paint, Vector2 coord)
    {
        switch (paint)
        {
            case PaintType.Empty:
                emptyPaintCoord.Add(coord);
                break;
            case PaintType.Wall:
                wallPaintCoord.Add(coord);
                break;
            case PaintType.Box:
                boxPaintCoord.Add(coord);
                break;
            case PaintType.Player:
                playerPaintCoord.Add(coord);
                break;
            case PaintType.Exit:
                exitPaintCoord.Add(coord);
                break;
            default:
                break;
        }

    }

    void Translate()
    {

        Debug.ClearDeveloperConsole();
        for (int i = 0; i < width * height; i++)
        {

            int x = ConvertCoordTo2D(i).x;
            int y = ConvertCoordTo2D(i).y;

            PaintType paint = blueprintGameboard[x, y].GetComponent<PaintTile>().PaintTileType;
            CollectData(paint, ConvertCoordTo2D(i));
            //  print(i+ " "+x+" "+y);
        }
        DeployGameEntity(PaintType.Exit, ref boxPaintCoord);
        DeployGameEntity(PaintType.Player, ref emptyPaintCoord);
    

        for (int i = 0; i < width * height; i++)
        {
          
            CreateTile(i);
          //  Destroy(blueprintGameboard[ConvertCoordTo2D(i).x, ConvertCoordTo2D(i).y],0.2f);
        }

        if (OnSendTranslation != null)
            OnSendTranslation();



    }

    PaintType GetPaintType(int i)
    {
        PaintType paint = blueprintGameboard[ConvertCoordTo2D(i).x, ConvertCoordTo2D(i).y].GetComponent<PaintTile>().PaintTileType;
        return paint;
        
    }
    PaintType GetPaintType(int x,int y)
    {
        PaintType paint = blueprintGameboard[x,y].GetComponent<PaintTile>().PaintTileType;
        return paint;

    }
    Vector2 ChooseRandomPosition(List<Vector2> sourceList)
    {
        int length = sourceList.Count;
        int idx = Random.Range(0, length);
        Vector2 coord = sourceList[idx];
        return coord;
    }

    bool ValidChosenPosition(Vector2 coord)
    { return true; }

    void DeployGameEntity(PaintType type, ref List<Vector2> sourceList)
    {
        Vector2 coord;
        do
        {
           coord = ChooseRandomPosition(sourceList);
        } while (!ValidChosenPosition(coord));
        SetGameEntity(coord, type, ref sourceList);
    }
    void SetGameEntity( Vector2 coord, PaintType type, ref List<Vector2> sourceList)
    {
        blueprintGameboard[(int)coord.x, (int)coord.y].GetComponent<PaintTile>().PaintTileType = type;
       // Debug.Log(coord+" "+ type);
        CollectData(type, coord);
        sourceList.Remove(coord);
    }
    void CreateRootObject()
    {
        RootObject  = new GameObject();
        RootObject.name = "Root";
    }
    void CreateTile(int i)
    {
        PaintType type = GetPaintType(i);
       // Debug.Log(glossary.GetPrefabTile(type));
        int x = ConvertCoordTo2D(i).x;
        int y = ConvertCoordTo2D(i).y;
        TileOnGrid[x,y]= Instantiate(glossary.GetPrefabTile(type), blueprintGameboard[x, y].transform.position, Quaternion.identity,RootObject.transform);
        TileOnGrid[x, y].PositionOnGrid = new Vector2Int(x, y);
        DeployObject(ref TileOnGrid[x, y]);
        DeployUnit(ref TileOnGrid[x, y]);
       

    }
    void DeployObject(ref BaseTile baseTile)
    {
        Vector2Int position = baseTile.PositionOnGrid;
        PaintType type = GetPaintType(position.x,position.y);
        //Debug.Log(type);
        if (glossary.GetPrefabObject(type) == null)
            return;
        BaseObject baseObject = Instantiate(glossary.GetPrefabObject(type), baseTile.transform.position, Quaternion.identity,RootObject.transform);
        baseTile.AddObjectToTile(baseObject);
    }
    void DeployUnit(ref BaseTile baseTile)
    {
        Vector2Int position = baseTile.PositionOnGrid;
        PaintType type = GetPaintType(position.x, position.y);
        //Debug.Log(type);
        if (glossary.GetPrefabUnit(type) == null)
            return;
        BaseUnit baseUnit = Instantiate(glossary.GetPrefabUnit(type), baseTile.transform.position, Quaternion.identity, RootObject.transform);
        baseTile.AddUnitOnTile(baseUnit);
    }

    public BaseTile[,] SendTranslation()
    {
        return TileOnGrid;

    }
    public void DisassembleTranslator()
    {
        if (OnFinishBuilding != null)
            OnFinishBuilding();
    }
    void DestroyGlossary()
    {
        Destroy(glossary);
    }


    
}
