using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour {

    public delegate void ReceivedTranslationAction();
    public  event ReceivedTranslationAction OnReceivedTranslationAction;


    BaseTile[,] gameBoard;
    int width;
    int height;
    private void Awake()
    {
        OnReceivedTranslationAction += InitBoardTile;
    }

    private void Start()
    {
      
    }
    public void ReceiveTranslation(BaseTile[,] baseTilesTable)
    {
        GetSizeArray(baseTilesTable);

        gameBoard = new BaseTile[width, height];
        gameBoard = baseTilesTable;
        Debug.Log(width + " " + height);

        if (OnReceivedTranslationAction != null)
            OnReceivedTranslationAction();
    }
    private void GetSizeArray(BaseTile[,] baseTilesTable)
    {
        width = baseTilesTable.GetLength(0);
        height = baseTilesTable.GetLength(1);
    }

    Vector2Int Convert1Dto2DCoord(int idx, int width)
    {
        Vector2Int Coord2d = new Vector2Int() ;
        Coord2d.x = idx / width;
        Coord2d.y = idx % width;
        return Coord2d;
    }
    void InitBoardTile()
    {
        for (int i = 0; i < height*width; i++)
        {
            Vector2Int Coord = Convert1Dto2DCoord(i, width);
            gameBoard[Coord.x, Coord.y].board = this;

            
        }
    }

    public  BaseTile GetNeighbourTile(int x, int y)
    {
        return gameBoard[x, y];
    }



   

    
   

}

    


