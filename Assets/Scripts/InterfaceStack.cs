interface IWeaponFire
{
    void  Spawn(BaseUnit unit,BaseTile tile);
    int ModifierDamageRange { get; set; }
    int ModifierDamageValue { get; set; }
    int ModifierDamageDuration { get; set; }
    int ModifierMaxBombAmount { get; set; }
}

interface ICharacterInput
{
   
    int Horizontal { get;  }
    int Vertical { get; }
    bool Fire { get;  }
    // float Speed { get; }
}
interface IDamage
{
   
    void TakeDamage(int damage);
    
}
interface IPowerUp
{
    void ChanceToSpawnPowerUp(BaseTile tile);
}
interface IEnemy
{
    void ChanceToSpawnEnemy(BaseTile tile);
}
interface IAbility
{
    int DamageRange { get; }
    int DamageValue { get; }
    int DamageDuration { get; }
    int HealthIncrease { get;}
    int MaxBombAmountIncrease { get; }
}

interface IFactory
{
    void SpawnEntiy(BaseTile tile);
}
