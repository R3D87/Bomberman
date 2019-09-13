using System;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(EnemyController), typeof(Weapon))]
public class BaseEnemy : BaseUnit, IDamage
{
    ICharacterInput input;
    IWeaponFire weapon;

    public event Action onFire;

    public event Action onEnemyDestroy;
    void Start()
    {
        MaxHealth = 2;
        onFire += SpawnBomb;
        input = GetComponent<ICharacterInput>();
        weapon = GetComponent<IWeaponFire>();
    }
    public void TakeDamage(int damage)
    {
        Health -= damage;
        Debug.Log(damage);
        if (Health <= 0)
            Destroy(gameObject);
    }

    void SpawnBomb()
    {
        weapon.Spawn(this, tile);
    }

    bool HasInputChanged()
    {

        return input.Horizontal != 0 || input.Vertical != 0;
    }
   
    // Update is called once per frame
    void Update()
    {
        if (HasInputChanged())
        {
            Movement(input.Horizontal, input.Vertical);
            
            // Debug.Log("X: " + input.Horizontal + " Y: " + input.Vertical);
        }
        if (input.Fire)
        {
            onFire();
        }

    }
    public bool HasOpportunityToMove(int xDir, int yDir)
    {
        return HasMovePremmission(xDir, yDir);
    }
    public BaseTile GetTileInDirection(int xDir, int yDir)
    {
        return GetNeigbourInDirection(xDir, yDir);
    }
    public Vector2Int GetCoord()
    {
        return coord;
    }
    public override void OnDestroy()
    {
        onEnemyDestroy();
        base.OnDestroy();
    }
}
