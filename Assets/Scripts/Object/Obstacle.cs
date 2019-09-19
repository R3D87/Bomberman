using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Obstacle : BaseObject, IDamage {


    public event Action<BaseTile> OnDestroyObstacle;
    int MaxHealth = 1;
    int Health;
    int debug = 0;
    IPowerUp powerUp;
    IEnemy enemy;
    FactoryEntities factory;
    private bool ExitApplication = false;

    void Start()
    {
        factory = GetComponentInParent<FactoryEntities>();
        OnDestroyObstacle += factory.SpawnOpportunity;
        powerUp = GetComponentInParent<IPowerUp>();
        enemy = GetComponentInParent<IEnemy>();
       

        Health = MaxHealth;
      
        
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        debug++;
        Debug.Log("Obstacle: " + debug);
        if (Health <= 0)
        {
            if (OnDestroyObstacle != null)
                OnDestroyObstacle(tile);
            Destroy(gameObject, 0.1f); 
        }
    }

    void SpawnAfterDestroy(BaseTile tile)
    {
        Debug.Log("Destroy");
     
      powerUp.ChanceToSpawnPowerUp(tile);
       enemy.ChanceToSpawnEnemy(tile);
        
    }
    public override void OnDestroy()
    {
       
        base.OnDestroy();


    }
    

}
