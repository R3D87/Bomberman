using System;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(EnemyController), typeof(Weapon), typeof(HealthBar))]
public class BaseEnemy : BaseUnit, IDamage
{
    ICharacterInput input;
    IWeaponFire weapon;
    IHealth healthBar;
    bool DamageSensitive = false;

    public event Action onFire;

    public event Action onEnemyDestroy;

    private void Awake()
    {
        MaxHealth = 10;
        Health = MaxHealth;
        onFire += SpawnBomb;

        input = GetComponent<ICharacterInput>();
        weapon = GetComponent<IWeaponFire>();
        healthBar = GetComponent<IHealth>();
        healthBar.MaxHealth(MaxHealth);
    }
    void Start()
    {

    }
    public void TakeDamage(int damage)
    {
        if (!DamageSensitive)
            damage = 0;

        Debug.Log("Damage Enemy: " + damage + "Enemy Health: " + Health);


        Health -= damage;


        if (Health <= 0)
        {
            Destroy(gameObject, 0.1f);
        }
        else
        {
            healthBar.DecreasingHealth(damage);
        }
    }

    void SpawnBomb()
    {
        weapon.Spawn(this, tile);
    }

    bool HasInputChanged()
    {

        return input.Horizontal != 0 || input.Vertical != 0;
    }
    public bool FireExectuting(bool inputFire)
    {
        return inputFire;
    }
    void RemoveDamageImmune()
    {
      
        if (!DamageSensitive)
            DamageSensitive = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (HasInputChanged())
        {
            
                Movement(input.Horizontal, input.Vertical);

            if (IsMoveExecuting())
                Invoke("RemoveDamageImmune", MoveDuration);



            // RemoveDamageImmune(IsMoveSuccessful);
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
        onEnemyDestroy = null;
    }
}
