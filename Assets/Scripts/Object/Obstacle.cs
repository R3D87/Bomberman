using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Obstacle : BaseObject, IDamage {


    
    int MaxHealth = 1;
    int Health;
    int debug = 0;
    IPowerUp powerUp;

    void Start()
    {
       
        powerUp= GetComponentInParent<IPowerUp>();
        Debug.Log(powerUp);
        Health = MaxHealth;
      
        
    }
    


    public void TakeDamage(int damage)
    {
        Health -= damage;
        debug++;
        Debug.Log("Obstacle: " + debug);
        if (Health <= 0)
            Destroy(gameObject);
    }

    public override void OnDestroy()
    {
        powerUp.ChanceToSpawnPowerUp(tile);
        Debug.Log("dupa");
        base.OnDestroy();
        
    }
    // Use this for initialization





    // Update is called once per frame
    void Update () {
		
	}


}
