interface IWeaponFire
{
    void  Spawn(BaseUnit unit,BaseTile tile);
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
