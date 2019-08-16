using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour {


  public struct Cell        
    {
        public GameObject Tile;
        public GameObject[] Object;
        public GameObject[] Unit;
    };

    GameObject[,] gameBoard;
    GameObject[,] blueprintGameboard;
    public GameObject Player;
 

    public ObjectTypes[] objectTypes;
    public TileTypes[] tileTypes;
    int width;
    int height;
    Dictionary<PaintType, TileType> TileTranslatorID = new Dictionary<PaintType, TileType>();
    Dictionary<PaintType, ObjectType> ObjectTranslatorID = new Dictionary<PaintType, ObjectType>();

    private void Start()
    {
       

        ObjectTranslatorID.Add(PaintType.Box, ObjectType.Box);

        TileTranslatorID.Add(PaintType.Box, TileType.Empty);
        TileTranslatorID.Add(PaintType.Empty, TileType.Empty);
        TileTranslatorID.Add(PaintType.Wall, TileType.Wall);
    }
    public void GetBluplirint(GameObject[,] blueprint)
    {

        SetSizeBaseOnBluprint(blueprint.GetLength(0), blueprint.GetLength(1));
       
        //blueprintGameboard = new GameObject[width, height];
        blueprintGameboard = blueprint;
       // ShowBluprintData();
       // TranslatePaintToGameBoard();
        Debug.ClearDeveloperConsole();
        gameBoard = new GameObject[width, height];
        StartCoroutine(ShowTileTranslate());
        StartCoroutine(ShowObjectTranslate());
      


    }
    void SetUpPlayer()
    {
        List<int> places = new List<int>();
        for (int i = 0; i < width * height; i++)
        {
            int x = i / width;
            int y = i % width;
            if (gameBoard[x, y].GetComponent<Empty>() != null)
            {
                places.Add(i);
                Debug.Log(x+ " "+y);
                
            }

        }
       
        int rand =  Random.Range(0, places.Count);
        int idx = places[rand];
        Vector3 tem_position = gameBoard[idx / width, idx % width].transform.position;
        tem_position += new Vector3(0, 0, -0.1f);
        gameBoard[idx / width, idx % width] = Instantiate(Player, tem_position, Quaternion.identity, transform);
    }

    GameObject Exit;
    void SetUpExit()
    {
        List<int> places = new List<int>();
        for (int i = 0; i < width * height; i++)
        {
            int x = i / width;
            int y = i % width;
            if (gameBoard[x, y].GetComponent<Obstacle>() != null)
            {
                places.Add(i);
                Debug.Log("Pfefw"+x + " " + y);

            }

        }

        int rand = Random.Range(0, places.Count);
        int idx = places[rand];
        Vector3 tem_position = gameBoard[idx / width, idx % width].transform.position;
        Destroy(gameBoard[idx / width, idx % width]);
        tem_position += new Vector3(0, 0, -0.1f);
       
        foreach (var item in tileTypes)
        {
            if (item.tileType == TileType.Exit)
            {
                Exit = item.baseTile;
            }
        }
      
        gameBoard[idx / width, idx % width] = Instantiate(Exit, tem_position, Quaternion.identity, transform);
    }
    void SetSizeBaseOnBluprint( int tempWidth, int tempHeight)
    {
        width = tempWidth;
        height = tempHeight;
        Debug.Log("width: " + width + " height: " + height);
    }

    public void ShowBluprintData()
    {
        for (int i = 0; i < width*height; i++)
        {
            int x = i / width;
            int y = i % width;
            Debug.Log("x: "+x+" y: "+y+
                " Fx: " + blueprintGameboard[x, y].GetComponent<PaintTile>().PositionX +
                " Fy: " + blueprintGameboard[x, y].GetComponent<PaintTile>().PositionY +
                " Color: " + blueprintGameboard[x, y].GetComponent<PaintTile>().PaintTileType);
        }
    }

    IEnumerator ShowTileTranslate()
    {

        for (int i = 0; i < width * height; i++)
        {

            int x = i / width;
            int y = i % width;

            PaintType paint = blueprintGameboard[x, y].GetComponent<PaintTile>().PaintTileType;

            Vector3 tem_position = blueprintGameboard[x, y].transform.position;

            for (int k = 0; k < tileTypes.Length; k++)
            {


                if (tileTypes[k].tileType == TileTranslatorID[paint])
                {

                    gameBoard[x, y] = Instantiate(tileTypes[k].baseTile, tem_position, Quaternion.identity, transform);
                }

                
            }
            yield return new WaitForSeconds(0.01f);
            ClearPaint(blueprintGameboard[x, y]);

        }
       yield return null;
    }
   

    
    IEnumerator ShowObjectTranslate()
    {
        for (int i = 0; i < width * height; i++)
        {
            int x = i / width;
            int y = i % width;

            PaintType paint = blueprintGameboard[x, y].GetComponent<PaintTile>().PaintTileType;

            Vector3 tem_position = blueprintGameboard[x, y].transform.position;

            for (int k = 0; k < objectTypes.Length; k++)
            {

                if (ObjectTranslatorID.ContainsKey(paint))
                {
                    Debug.Log(paint);
                    if (objectTypes[k].objectType == ObjectTranslatorID[paint])
                    {
                        tem_position += new Vector3(0, 0, -0.1f);
                        gameBoard[x, y] = Instantiate(objectTypes[k].baseObject, tem_position, Quaternion.identity, transform);
                    }
                }

            }
            yield return new WaitForSeconds(0.01f);
            ClearPaint(blueprintGameboard[x, y]);

        }
        SetUpPlayer();
       SetUpExit();
        yield return null;
    }
    void ClearPaint(GameObject gameObject)
    {

        if (!gameObject.activeSelf)
            Destroy(gameObject);
        else
            gameObject.gameObject.SetActive(false);
    }
}

    


