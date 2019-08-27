using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translator : MonoBehaviour {


    GameObject[,] blueprintGameboard;
    BaseTile[,] TileOnGrid;
    Glossary glossary;
    List<Vector2> boxPaintCoord = new List<Vector2>();
    List<Vector2> emptyPaintCoord = new List<Vector2>();
    List<Vector2> wallPaintCoord = new List<Vector2>();
    List<Vector2> playerPaintCoord = new List<Vector2>();
    List<Vector2> exitPaintCoord = new List<Vector2>();
    int width;
    int height;

    private void Start()
    {
        gameObject.AddComponent<Glossary>();
        glossary = GetComponent<Glossary>();
    }


    public void GetBlueprint(GameObject[,] blueprint)
    {

        SetSizeBaseOnBluprint(blueprint.GetLength(0), blueprint.GetLength(1));

        blueprintGameboard = blueprint;



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
            TileOnGrid = new BaseTile[width, height];
            TileIns();
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

    Vector2 ConvertCoordTo2D(int i)
    {
        Vector2 position2D;

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

    void TileIns()
    {

        Debug.ClearDeveloperConsole();
        for (int i = 0; i < width * height; i++)
        {

            int x = (int)ConvertCoordTo2D(i).x;
            int y = (int)ConvertCoordTo2D(i).y;

            PaintType paint = blueprintGameboard[x, y].GetComponent<PaintTile>().PaintTileType;
            CollectData(paint, ConvertCoordTo2D(i));
            //  print(i+ " "+x+" "+y);
        }
        DeployGameEntity(PaintType.Exit, ref boxPaintCoord);
        DeployGameEntity(PaintType.Player, ref emptyPaintCoord);
        print("It's ok");

        for (int i = 0; i < width * height; i++)
        {
          
            CreateTile(i);
        }
       


    }
    PaintType GetPaintType(int i)
    {
        PaintType paint = blueprintGameboard[(int)ConvertCoordTo2D(i).x, (int)ConvertCoordTo2D(i).y].GetComponent<PaintTile>().PaintTileType;
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
        Debug.Log(coord+" "+ type);
        CollectData(type, coord);
        sourceList.Remove(coord);
    }
    void CreateTile(int i)
    {
        PaintType type = GetPaintType(i);
        Debug.Log(glossary.GetPrefabTile(type));
        int x = (int)ConvertCoordTo2D(i).x;
        int y = (int)ConvertCoordTo2D(i).y;
        TileOnGrid[x,y]= Instantiate(glossary.GetPrefabTile(type), blueprintGameboard[x, y].transform.position, Quaternion.identity);
        
    }
    //  Debug.Log(   glossary.GetPrefabTile( glossary.GetGlossaryItem(paint)));


    // Debug.ClearDeveloperConsole();
    //Debug.Log("x: " + x + " y: " + y + " Pait: " + paint);

    //    Vector3 tem_position = blueprintGameboard[x, y].transform.position;


    // TileOnGrid[x, y]= Instantiate(tile,tem_position,Quaternion.identity); 

}
