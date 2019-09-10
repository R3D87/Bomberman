using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput),typeof(PlayerWeapon))]
public class Player : BaseUnit, IDamage
{
    ICharacterInput input;
    IWeaponFire weapon;
    IAbility ability;
    
    public event Action onFire;

 
    private void Awake()
    {
        onFire += SpawnBomb;
      

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
           // Debug.Log("X: " + input.Horizontal + " Y: " + input.Vertical);
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
        Debug.Log(damage);
        if (Health == 0)
            Destroy(gameObject);
    }
    public override void TakePowerUp(PowerUp powerUp)
    {
        ability = powerUp.GetComponent<IAbility>();
        if(ability!=null)
            UpgradeAbility(ability);
        base.TakePowerUp(powerUp);
    }

}
