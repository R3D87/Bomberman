using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerUp : BaseObject, IDamage, IAbility { 
    int MaxHealth = 1;
    int Health;
    //-Interface
    int damageRange;
    int damageValue;
    int damageDuration;
    int healthIncrease;
    int maxBombAmountIncrease=1;

    public event Action OnPowerUpDestroy;

    public int DamageRange { get { return damageRange; } }
    public int DamageValue { get { return damageValue; } }
    public int DamageDuration { get { return damageDuration; } }
    public int HealthIncrease { get { return healthIncrease; } }
    public int MaxBombAmountIncrease { get { return maxBombAmountIncrease; } }
    //- Interface
    private void Start()
    {

        Health = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            OnPowerUpDestroy();
            Destroy(gameObject,0.1f);
        }
    }


}
