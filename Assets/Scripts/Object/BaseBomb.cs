using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseBomb : BaseObject
{
   

    Queue<BaseTile> TilesQueue = new Queue<BaseTile>();
    Queue<BaseTile> downTilesQueue = new Queue<BaseTile>();
    Queue<BaseTile> leftTilesQueue = new Queue<BaseTile>();
    Queue<BaseTile> rightTilesQueue = new Queue<BaseTile>();
    
    int distanceExplotion = 1;
    int Damage = 1;
    float Timer = 0;
    private void Start()
    {
        //OnTakeDamge
    }

    void RangeTable(int range)
    {
        int tableLength = range * 2;
        int[] tab= new int[tableLength];
        for (int k=0, i = -range; i < range; i++)
        {
            if (i == 0)
                continue;
            tab[k] = i;
            k++;
        }
    }

    void CauseDamage(int Range)
    {

        SendDamgeToTile(0, 0, distanceExplotion);
    }

    void SendDamgeToTile(int x, int y,int limit)
    {
        if (distanceExplotion < limit)
            return;

        limit++;
        BaseTile tileToCheck = tile.GetNeigbourInDirection(x,y);
        if (tileToCheck.GetType() == typeof(Wall))
            return;
        tileToCheck.GetDamage(Damage);

        SendDamgeToTile(x - limit, y, limit);
        SendDamgeToTile(x + limit, y, limit);
        SendDamgeToTile(x, y - limit, limit);
        SendDamgeToTile(x, y + limit, limit);
    }
    



    private void Update()
    {
        Timer+=Time.deltaTime;
        if (Timer >= 3)
        {
            Explotion();
        }
    }
    void Explotion()
    {
        //GetTilesOnRange(distanceExplotion);
        SendDamgeToTile(0, 0, 0);

        Debug.DrawRay(transform.position, 1.5f*Vector3.left, Color.red,int.MaxValue);

        Debug.DrawRay(transform.position, Vector3.right, Color.red, int.MaxValue);
        Debug.DrawRay(transform.position, Vector3.up, Color.red, int.MaxValue);
        Debug.DrawRay(transform.position, Vector3.down, Color.red, int.MaxValue);

        Destroy(gameObject, 0.1f);
    }
    


    
}
