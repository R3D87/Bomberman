﻿using System;
using System.Linq;
using UnityEngine;



public class GameBuilder : MonoBehaviour {

    // Use this for initialization
    [Range(6, 20)]
    public int userWidth = 15;
    [Range(6, 20)]
    public int userHeigh = 16;
    public float threshold = 0.7f;
    public GameObject EmptyCanvas;
    public GameObject Wall;
    public GameObject Obstacle;
    int heigh = 18;
    int width;
    GameObject[,] Paint;


    TileTypes tileTypes;

    public  event Action OnPaintingRandomBoard = delegate { } ;

    private void Awake()
    {
       
       OnPaintingRandomBoard += FillBoard;
    }
    public void PaintingRandomBoard()
    {
        OnPaintingRandomBoard();
    }
    void Start()
    {
        
        width = userWidth;
        heigh = userHeigh;

        width += (width % 2 == 0) ? 0 : 1;
        heigh += (heigh % 2 == 0) ? 0 : 1;
        Paint = new GameObject[width + 1, heigh + 1];

        for (int x = 0; x <= width; x++)
        {
            for (int y = 0; y <= heigh; y++)
            {
                Vector3 position = new Vector3(x - width / 2, y - heigh / 2, 0);

                if (y == 0 || x == 0 || x == width || y == heigh)
                {
                    Paint[x, y] = Instantiate(Wall, position, Quaternion.identity, gameObject.transform);
                    Paint[x, y].layer = LayerMask.NameToLayer("Default");
                    Paint[x, y].GetComponent<PaintTile>().PaintTileConstructor(x, y, PaintType.Wall);


                }
                else
                {
                    Paint[x, y] = Instantiate(EmptyCanvas, position, Quaternion.identity, gameObject.transform);
                    Paint[x, y].GetComponent<PaintTile>().PaintTileConstructor(x, y, PaintType.Empty);
                }
            }
        }
        UIScript.OnConditionCheck += IsFulFillStartGameCondition;

    }

    void convert1DCoordTo2D(int i, out int x, out int y)
    {
        x = i / width;
        y = i % width;
    }
    int convert2DcoordTo1D(int x, int y)
    {
        return x * width + y % width;
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            OnPaintingRandomBoard();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            for (int i = 0; i < width * heigh; i++)
            {
                int x = i / width;
                int y = i % width;
                Debug.Log(Paint[x, y].GetComponent<PaintTile>().PositionX + " " + Paint[x, y].GetComponent<PaintTile>().PositionY + " " + Paint[x, y].GetComponent<PaintTile>().PaintTileType);
            }


        }

    }

    bool IsFulFillStartGameCondition()
    {
        bool isBoxOnBoard = false;
        bool isEmptyTileOnBoard = false;
       
        for (int i = 0; i < width * heigh; i++)
        {
            int x = i / width;
            int y = i % width;
            if (Paint[x, y].GetComponent<PaintTile>().PaintTileType == PaintType.Box)
            {
                isBoxOnBoard = true;
            }
            if (Paint[x, y].GetComponent<PaintTile>().PaintTileType == PaintType.Empty)
            {
                isEmptyTileOnBoard = true;
            }
        }
        return isBoxOnBoard && isEmptyTileOnBoard;
    }
    void FillBoard()
    {

        for (int x = 1; x < width; x++)
        {
            for (int y = 1; y < heigh; y++)
            {

                Vector3 vector = new Vector3(x - width / 2, y - heigh / 2, 0);

                if (x % 2 == 0 && y % 2 == 0)
                {
                    Destroy(Paint[x, y]);

                    Paint[x, y] = Instantiate(Wall, vector, Quaternion.identity, gameObject.transform);
                    Paint[x, y].GetComponent<PaintTile>().PaintTileConstructor(x, y, PaintType.Wall);
                }
                else if (x == 1 && y == 1 || x == 1 && y == heigh - 1 || x == width - 1 && y == 1 || x == width - 1 && y == heigh - 1)
                {

                }
                else
                {
                    if (IsAllowedToSetObstacle())
                    {
                        Destroy(Paint[x, y]);
                        Paint[x, y] = Instantiate(Obstacle, vector, Quaternion.identity, gameObject.transform);
                        Paint[x, y].GetComponent<PaintTile>().PaintTileConstructor(x, y, PaintType.Box);


                    }
                    else
                    {
                        Destroy(Paint[x, y]);
                        Paint[x, y] = Instantiate(EmptyCanvas, vector, Quaternion.identity, gameObject.transform);
                        Paint[x, y].GetComponent<PaintTile>().PaintTileConstructor(x, y, PaintType.Empty);
                    }
                }

            }
        }
    }


    bool IsAllowedToSetObstacle()
    {
        float rnd = UnityEngine.Random.value;
        
        return (rnd >= threshold) ? true : false;

    }

    public GameObject[,] SendGameBoardBluprint()
    {
        return Paint;
    }

    public void AddElemntToPaint(int x, int y, GameObject gameObject)
    {
        Paint[x, y] = gameObject;
    }



}
