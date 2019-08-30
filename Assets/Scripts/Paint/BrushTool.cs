using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushTool : MonoBehaviour
{
    // Use this for initialization
    GameObject Brush;
    GameObject Source;
    GameBuilder gameBuilder;
    public GameObject EraseBrush;
    LayerMask maskCanvas, maskPicker;
    bool[] isDragActive, downInPreviousFrame;
    PaintType BrushType;

    void Start()
    {
        maskCanvas = LayerMask.GetMask("Paintable");
        maskPicker = LayerMask.GetMask("Source");
        isDragActive = new bool[] { false, false };
        downInPreviousFrame = new bool[] { false, false};
        gameBuilder = gameObject.GetComponent<GameBuilder>() ;
        
    }
    Vector3 GetPosition(RaycastHit hit)
    {
        return hit.collider.gameObject.transform.position;
    }
    GameObject GetGameObject(RaycastHit hit)
    {
        return hit.collider.gameObject;
    }
    Vector2 GetPositionInArray(GameObject gameObject)
    {
        Vector2 position2D;
        position2D.x = gameObject.GetComponent<PaintTile>().PositionX;
        position2D.y = gameObject.GetComponent<PaintTile>().PositionY;

        return position2D;
    }

    void Paint()
    {

        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    Debug.Log(mouseRay.direction);
        Debug.DrawRay(mouseRay.origin, mouseRay.direction, Color.red, int.MaxValue);
        RaycastHit hit;
        // Add Object on Canvas
        if (Physics.Raycast(mouseRay, out hit, int.MaxValue, maskCanvas))
        {
            if (Brush != null)
            {
                PaintType brushType= Brush.GetComponent<PaintTile>().PaintTileType;
                GameObject objectToDestroy = GetGameObject(hit);
                Vector3 position = GetPosition(hit);
                Vector2 PositionInArray = GetPositionInArray(objectToDestroy);

                GameObject Ink = Instantiate(Brush, position, Quaternion.identity,gameObject.transform);
                Ink.GetComponent<PaintTile>().PositionX = (int)PositionInArray.x;
                Ink.GetComponent<PaintTile>().PositionY = (int)PositionInArray.y;
                Ink.GetComponent<PaintTile>().PaintTileType = brushType;
                gameBuilder.AddElemntToPaint((int)PositionInArray.x, (int)PositionInArray.y, Ink);

                Destroy(hit.collider.gameObject);

               
            }

        }
        // Take Color form Source
        if (Physics.Raycast(mouseRay, out hit, int.MaxValue, maskPicker))
        {

            Source = hit.collider.gameObject;
            
            Brush = Source.GetComponent<ObjectSource>().source;
           
        }
    }
    void Erase()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
      //  Debug.Log(mouseRay.direction);
        Debug.DrawRay(mouseRay.origin, mouseRay.direction, Color.red, int.MaxValue);
        RaycastHit hit;

        if (Physics.Raycast(mouseRay, out hit, int.MaxValue, maskCanvas))
        {

            PaintType brushType = EraseBrush.GetComponent<PaintTile>().PaintTileType;
            GameObject objectToDestroy = GetGameObject(hit);
            Vector3 position = GetPosition(hit);
            Vector2 PositionInArray = GetPositionInArray(objectToDestroy);

            GameObject Ink = Instantiate(EraseBrush, position, Quaternion.identity, gameObject.transform);
            Ink.GetComponent<PaintTile>().PositionX = (int)PositionInArray.x;
            Ink.GetComponent<PaintTile>().PositionY = (int)PositionInArray.y;
            Ink.GetComponent<PaintTile>().PaintTileType = brushType;
            gameBuilder.AddElemntToPaint((int)PositionInArray.x, (int)PositionInArray.y, Ink);
            Destroy(hit.collider.gameObject);

        }
    }
    void OnDragging(int i)
    {
        if (i == 0)
            Paint();
        else
            Erase();


    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < isDragActive.Length; i++)
        {
            if (Input.GetMouseButton(i))
            {
                if (downInPreviousFrame[i])
                {
                    if (isDragActive[i])
                    {
                        OnDragging(i);
                    }
                    else
                    {
                        isDragActive[i] = true;
                       
                    }
                }
                downInPreviousFrame[i] = true;
            }
            else
            {
                if (isDragActive[i])
                {
                    isDragActive[i] = false;
                 
                }
                downInPreviousFrame[i] = false;
            }
        }

    }

}