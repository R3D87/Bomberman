using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseBomb : BaseObject
{
   

   
    int ExplotionRange = 1;
    int Damage = 1;
    float Timer = 1;
    int DetonationTime = 3;

 


    void PropateExplotionInDirecion(int x, int y,  int limit)
    {
        if (limit > ExplotionRange)
            return;
        BaseTile tileToCheck = tile.GetNeigbourInDirection(x,y);
        if (tileToCheck.GetType() == typeof(Wall))
            return;
        tileToCheck.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        tileToCheck.GetDamage(Damage);

        limit++;
        PropateExplotionInDirecion(x - 1, y,  limit);
        PropateExplotionInDirecion(x + 1, y,  limit);
        PropateExplotionInDirecion(x, y-1,  limit);
        PropateExplotionInDirecion(x, y+1,  limit);
      
    }
    bool IsCountdownFinished(int detonationTime)
    {
        Timer += Time.deltaTime;
        return Timer >= detonationTime;
    }
    private void Update()
    {

        if (IsCountdownFinished(DetonationTime))
        {
            Destroy(gameObject, 0.1f);
            Debug.Log("timer:" + Timer);
        }
    }
    void Explotion()
    {

        int limit = 0;
        PropateExplotionInDirecion(0, 0,  limit);



        Debug.DrawRay(transform.position, 1.5f*Vector3.left, Color.red,int.MaxValue);
        Debug.DrawRay(transform.position, Vector3.right, Color.red, int.MaxValue);
        Debug.DrawRay(transform.position, Vector3.up, Color.red, int.MaxValue);
        Debug.DrawRay(transform.position, Vector3.down, Color.red, int.MaxValue);

       
    }
   override public void OnDestroy()
    {
        base.OnDestroy();
        Explotion();
    }




}
