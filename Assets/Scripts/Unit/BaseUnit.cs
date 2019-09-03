using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseUnit : MonoBehaviour {

    // Use this for initialization
    public delegate void MovePreperation(Vector2Int coord);
    public static event MovePreperation OnMovePreparation;

    Coroutine test;
    public BaseTile tile;



    private void Start()
    {
       
      
    }
    protected bool HasMovePremmission(int xDir, int yDir)
    {
       
        return true;
    }
    BaseTile GetNeigbourInDirection(int xDir, int yDir)
    {
        return tile.GetNeigbourInDirection(xDir, yDir);

    }
    protected bool Move(int xDir, int yDir)
    {
        if (test==null) {
            BaseTile tempBaseTile = GetNeigbourInDirection(xDir, yDir);
            Vector3 TargetPosition = tile.GetNeighborLocation(tempBaseTile);
            test = StartCoroutine(SmoothMovement(TargetPosition, 0.1f));

            tile = tempBaseTile;
            return true;
        }
        return false;
      

    }

    protected IEnumerator SmoothMovement( Vector3 target, float duration)
    {
        float progress = 0;
        Vector3 StartPosition = transform.position;


        while (progress<=duration)
        {
            progress = progress + Time.deltaTime;
            float procent = Mathf.Clamp01(progress / duration);

            gameObject.transform.position= Vector3.Lerp(transform.position, target, procent);
   
            yield return null;
        }
        test = null;
       
    }

}
