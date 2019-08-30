using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour {

    public delegate void ReceivedTranslationAction();
    public  event ReceivedTranslationAction OnReceivedTranslationAction;


    BaseTile[,] gameBoard;
    int width;
    int height;


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

    





   

    
   

}

    


