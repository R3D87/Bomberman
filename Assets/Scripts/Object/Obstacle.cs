using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : BaseObject, IDamage{



    int MaxHealth = 1;
    int Health;
    int debug = 0;
    void Start()
    {
        
        Health = MaxHealth;

    }


    public void TakeDamage(int damage)
    {
        Health -= damage;
        debug++;
        Debug.Log("Obstacle: " + debug);
        if (Health == 0)
            Destroy(gameObject);
    }
    private void OnDestroy()
    {
        tile.RemoveObjectOnTile(this);
    }

    // Use this for initialization




    // Update is called once per frame
    void Update () {
		
	}
}
