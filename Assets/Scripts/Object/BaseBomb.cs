using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseBomb : BaseObject, IDamage { 


    public event Action OnBombExploded;
    public event Action OnBombExplotion;
    public ExplosionEffect effect;
    int[] Height;
    int[] Width;

    int ExplotionRange = 1;
    int Damage = 1;
    float Timer = 0;
    int DetonationTime = 2;
    bool DoOnce = true;
    bool isDebug = false;

    private  bool Quit = false;


    void ReloadScene()
    {
        Quit = true;
    }
 
    private void Start()
    {
        UIScript.OnReLoadDuringGame += ReloadScene;
        OnBombExplotion += Explotion;
    }
    void PropateExplotion(int x,int y, int limit,int xStep,int yStep)
    {

        Debug.Log("X: " + x + " Y: " + y);
        if (limit >= ExplotionRange)
            return;

        BaseTile tileToCheck = tile.GetNeigbourInDirection(x,y);
        
        if (tileToCheck.GetType() == typeof(Wall))
            return;
        ExplosionEffect SFX = Instantiate(effect, tileToCheck.transform.position, Quaternion.identity);
        tileToCheck.AddObjectToTile(SFX);
        
        tileToCheck.GetComponent<IDamage>().TakeDamage(Damage);
        if (xStep == 0 && yStep == 0)
            return;
       
        limit++;
  
        PropateExplotion(x + xStep, y+ yStep, limit,xStep,yStep); 
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
        if (IsCountdownFinished(DetonationTime) && OnBombExplotion!=null)
        {
            OnBombExplotion();
            OnBombExplotion = null;
        }
    }

    void Explotion()
    {     
        int limit = 0;
        PropateExplotion(0, 0, limit, 0,0);
        PropateExplotion(0, -1, limit, 0,-1);
        PropateExplotion(0, 1, limit, 0,-1);
        PropateExplotion(-1, 0, limit, -1,0);
        PropateExplotion(1, 0, limit, 1,0);

        if (isDebug)
        {
            Debug.DrawRay(transform.position, 1.5f * Vector3.left, Color.red, int.MaxValue);
            Debug.DrawRay(transform.position, Vector3.right, Color.red, int.MaxValue);
            Debug.DrawRay(transform.position, Vector3.up, Color.red, int.MaxValue);
            Debug.DrawRay(transform.position, Vector3.down, Color.red, int.MaxValue);
        }
    }

    override public void OnDestroy()
    {
        UIScript.OnReLoadDuringGame -= ReloadScene;
        if (!Quit && OnBombExplotion != null)
            OnBombExplotion();
       
       if (OnBombExploded != null)
            OnBombExploded();

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
