using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Obstacle : BaseObject, IDamage {


    
    int MaxHealth = 1;
    int Health;
    int debug = 0;
    IPowerUp powerUp;
    IEnemy enemy;

    void Start()
    {
       
        powerUp= GetComponentInParent<IPowerUp>();
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
            SpawnAfterDestroy();
            Destroy(gameObject);
        }
    }

    void SpawnAfterDestroy()
    {
        powerUp.ChanceToSpawnPowerUp(tile);
        enemy.ChanceToSpawnEnemy(tile);
    }
    public override void OnDestroy()
    {


        base.OnDestroy();
    }


}
