interface IWeaponFire
{
    void  Spawn(BaseUnit unit,BaseTile tile);
    int ModifierDamageRange { get; set; }
    int ModifierDamageValue { get; set; }
    int ModifierDamageDuration { get; set; }
}

interface ICharacterInput
{
    void ReadInput();
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
interface IAbility
{
    int DamageRange { get; }
    int DamageValue { get; }
    int DamageDuration { get; }
    int HealthIncrease { get;}
}   