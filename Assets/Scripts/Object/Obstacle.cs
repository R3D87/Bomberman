using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Obstacle : BaseObject, IDamage {

    int MaxHealth = 1;
    int Health;
    int debug = 0;

    IFactory factory;
    private bool ExitApplication = false;

    void Start()
    {
        factory = GetComponentInParent<IFactory>();
        Health = MaxHealth;        
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        debug++;
        Debug.Log("Obstacle: " + debug);
        if (Health <= 0)
        {
            factory.SpawnEntiy(tile);
            Destroy(gameObject, 0.1f); 
        }
    }

    public override void OnDestroy()
    {     
        base.OnDestroy();
    }
}
