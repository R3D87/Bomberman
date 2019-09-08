using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput),typeof(PlayerWeapon))]
public class Player : BaseUnit, IDamage
{
    ICharacterInput input;
    IWeaponFire weapon;
    
    public event Action onFire;

    int MaxHealth =5;
    int Health;
    
    private void Awake()
    {
        onFire += SpawnBomb;
      

    }

    private void Start()
    {
        Health = MaxHealth;
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

    public void TakeDamage(int damage)
    {
        Health -= damage;
        Debug.Log(damage);
        if (Health == 0)
            Destroy(gameObject);
    }

}
