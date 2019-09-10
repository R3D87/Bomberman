using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : BaseObject, IDamage, IAbility { 
    int MaxHealth = 1;
    int Health;
    int damageRange=1;
    int damageValue;
    int damageDuration;
    int healthIncrease;
    //-Interface
    public int DamageRange { get { return damageRange; } }
    public int DamageValue { get { return damageValue; } }
    public int DamageDuration { get { return damageDuration; } }
    public int HealthIncrease { get { return healthIncrease; } }
    //- Interface
    private void Start()
    {
        Health = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
            Destroy(gameObject);
    }


}
