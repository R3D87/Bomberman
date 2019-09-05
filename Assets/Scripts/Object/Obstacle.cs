using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : BaseObject, IDamage{



    int MaxHealth = 1;
    int Health;
    void Start()
    {
        
        Health = MaxHealth;

    }


    public void TakeDamage(int damage)
    {
        Health -= damage;
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
