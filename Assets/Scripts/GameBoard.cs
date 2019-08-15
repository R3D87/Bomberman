using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour {


    GameObject[,] gameBoard;
    GameObject[,] blueprintGameboard;

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
    public void ShowGameBoardData()
    {
        for (int i = 0; i < width * height; i++)
        {
            int x = i / width;
            int y = i % width;
            Debug.Log("x: " + x + " y: " + y +
                " Fx: " + blueprintGameboard[x, y].GetComponent<PaintTile>().PositionX +
                " Fy: " + blueprintGameboard[x, y].GetComponent<PaintTile>().PositionY +
                " Color: " + gameBoard[x, y].GetComponent<PaintTile>().PaintTileType);
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
            Destroy(blueprintGameboard[x, y]);

        }
       yield return null;
    }
   

    
    void TranslatePaintToGameBoard()
    {
        gameBoard = new GameObject[width, height];

        for (int k = 0; k < tileTypes.Length; k++)
        {
            for (int i = 0; i < width * height; i++)
            {
                int x = i / width;
                int y = i % width;

                PaintType paint = blueprintGameboard[x, y].GetComponent<PaintTile>().PaintTileType;
                if (tileTypes[k].tileType == TileTranslatorID[paint])
                {
                    gameBoard[x, y] = tileTypes[k].baseObject;
                    
                    
                }
            }
        }
    }    
}

    


