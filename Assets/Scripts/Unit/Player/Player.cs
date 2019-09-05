using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput),typeof(PlayerWeapon))]
public class Player : BaseUnit
{
    ICharacterInput input;
    IWeaponFire weapon;
    
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
            Debug.Log("X: " + input.Horizontal + " Y: " + input.Vertical);
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
}
