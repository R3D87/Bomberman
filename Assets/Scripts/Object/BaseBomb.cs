﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseBomb : BaseObject, IDamage { 

    delegate int SimpleMethod();
    SimpleMethod[] simpleMethodForHeight;
    SimpleMethod[] simpleMethodForWidth;
    public event Action OnBombExplosion;
    int ExplotionRange = 1;
    int Damage = 1;
    float Timer = 0;
    int DetonationTime = 2;
    bool DoOnce = true;
   
    public ExplosionEffect effect;
    private  bool Quit = false;

   
    int IncreaseValue()
    {
        return  1;
    }
    int DecreaseValue()
    {
        return - 1;
    }

    int ConstatntValue()
    {
        return 0;
    }
     void ReloadScene()
    {
        Quit = true;
    }
 
    private void Start()
    {
        UIScript.OnReLoadDuringGame += ReloadScene;
        simpleMethodForHeight = new SimpleMethod[4] {
             new SimpleMethod(ConstatntValue),
             new SimpleMethod(ConstatntValue),
             new SimpleMethod(IncreaseValue),
             new SimpleMethod(DecreaseValue) } ;

         simpleMethodForWidth = new SimpleMethod[4] {
             new SimpleMethod(DecreaseValue),
             new SimpleMethod(IncreaseValue),
             new SimpleMethod(ConstatntValue),
             new SimpleMethod(ConstatntValue) };
    }
    void PropateExplotion(int x,int y, int limit,int idx)
    {
        if (limit >= ExplotionRange)
            return;

      BaseTile tileToCheck = tile.GetNeigbourInDirection(x,y);
        
        if (tileToCheck.GetType() == typeof(Wall))
            return;
        ExplosionEffect SFX = Instantiate(effect, tileToCheck.transform.position, Quaternion.identity);
        tileToCheck.AddObjectToTile(SFX);
        
        tileToCheck.GetComponent<IDamage>().TakeDamage(Damage);
        if (idx == -1)
            return;


        limit++;

        PropateExplotion(x + simpleMethodForHeight[idx](), y+ simpleMethodForWidth[idx](), limit,idx);
  
        
    }

    bool IsCountdownFinished(int detonationTime)
    {
        Timer += Time.deltaTime;
        return Timer >= detonationTime;
    }

    void ExplosionExecuted()
    {
        DoOnce = false;
    }
    bool WasExplotionExecuted()
    {
        return DoOnce;
    }
    private void Update()
    {

        if (IsCountdownFinished(DetonationTime) && WasExplotionExecuted())
        {
            Explotion();
          //  Debug.Log("timer:" + Timer);
        }
    }
    void Explotion()
    {
        
        int limit = 0;
        PropateExplotion(0, 0, limit, -1);
        for (int i = 0; i < 4; i++)
        {
            PropateExplotion(simpleMethodForHeight[i](), simpleMethodForWidth[i](), limit, i);
        }
        
      //  PropateExplotionInDirecion(0, 0,  limit);
      


        Debug.DrawRay(transform.position, 1.5f*Vector3.left, Color.red,int.MaxValue);
        Debug.DrawRay(transform.position, Vector3.right, Color.red, int.MaxValue);
        Debug.DrawRay(transform.position, Vector3.up, Color.red, int.MaxValue);
        Debug.DrawRay(transform.position, Vector3.down, Color.red, int.MaxValue);
        ExplosionExecuted();
       
    }
   override public void OnDestroy()
    {
        UIScript.OnReLoadDuringGame -= ReloadScene;
        if (!Quit)
            Explotion();
        if (OnBombExplosion != null)
            OnBombExplosion();
        base.OnDestroy();
        

    }


    public void modifierProperties(int modifierDamageRange, int modifierDamageDuration, int modifierDamageValue)
    {
        ExplotionRange += modifierDamageRange;
        Damage += modifierDamageValue;
    }

    public void TakeDamage(int damage)
    {  
        Destroy(gameObject,0.1f);
    }


    private void OnApplicationQuit()
    {
        Quit = true;
    }
}
