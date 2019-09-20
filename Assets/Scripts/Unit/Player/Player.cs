﻿using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput),typeof(Weapon))]
public class Player : BaseUnit, IDamage
{
    ICharacterInput input;
    IWeaponFire weapon;
    IAbility ability;
    
    public event Action onFire;
    public static event Action onPlayerDestroy;
  
    private void Awake()
    {
        MaxHealth = 1;
        Health = MaxHealth;
        onFire += SpawnBomb;
        MoveDuration = 0.1f;
        
    }

    private void Start()
    {  
        input = GetComponent<ICharacterInput>();
        weapon = GetComponent<IWeaponFire>();
    }

    bool HasInputChanged()
    {
        return input.Horizontal != 0 || input.Vertical != 0;
    }

    private void Update()
    {
        if ( HasInputChanged())
        {
            Movement( input.Horizontal, input.Vertical);
        }
        
        if (input.Fire)
        {
            onFire();
        }
    }

    void SpawnBomb()
    {
        weapon.Spawn(this, tile);
    }
   
    void UpgradeAbility(IAbility abilityToPropagate)
    {
        Debug.Log("Health: " + Health);
        Health += abilityToPropagate.HealthIncrease;
        weapon.ModifierDamageDuration += abilityToPropagate.DamageDuration;
        weapon.ModifierDamageRange += abilityToPropagate.DamageRange;
        weapon.ModifierDamageValue += abilityToPropagate.DamageValue;
        weapon.ModifierMaxBombAmount += abilityToPropagate.MaxBombAmountIncrease;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        Debug.Log("Player Damage:"+damage);
        if (Health <= 0)
            if (onPlayerDestroy != null)
                onPlayerDestroy();
        Destroy(gameObject,0.1f);
    }

    public override void TakePowerUp(PowerUp powerUp)
    {
        ability = powerUp.GetComponent<IAbility>();
        if(ability!=null)
            UpgradeAbility(ability);
        base.TakePowerUp(powerUp);
    }
    public override void OnDestroy()
    {
       
        base.OnDestroy();
    }
}
