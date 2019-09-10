using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour,IWeaponFire
{

    public BaseBomb bomb;

    int modifierDamageRange;
    int modifierDamageValue;
    int modifierDamageDuration;
    int modifierMaxBombAmount;

    //-Interface
    public int ModifierDamageRange { get { return modifierDamageRange; } set {  modifierDamageRange = value; } }
    public int ModifierDamageValue { get { return modifierDamageValue; } set { modifierDamageValue = value; } }
    public int ModifierDamageDuration { get { return modifierDamageDuration; } set { modifierDamageDuration = value; }  }
    public int ModifierMaxBombAmount { get { return modifierMaxBombAmount; } set { modifierMaxBombAmount = value; MaxBombAmount += value;  } }

    //- Interface


  
  
    int MaxBombAmount = 1;
    int CurrentBombAmount=0; 
    
    bool CanSpawnBomb()
    {
        return MaxBombAmount > CurrentBombAmount;
    }

    void DecreasBombAmountAferExplosion()
    {
        CurrentBombAmount--;
    }
    void IncreaseBombAmountAfterSpawn()
    {
        CurrentBombAmount++;
    }

    public void Spawn(BaseUnit unit,BaseTile baseTile)
    {
        if (CanSpawnBomb())
        {
            BaseBomb bombInst = Instantiate(bomb, unit.transform.position, Quaternion.identity);
            InitBomb(ref bombInst, baseTile);
            bombInst.OnBombExplosion += DecreasBombAmountAferExplosion;
            IncreaseBombAmountAfterSpawn();
        }
    }

    void InitBomb(ref BaseBomb bombToInit, BaseTile tile )
    {
        bombToInit.tile = tile;
        tile.AddObjectToTile(bombToInit);
        bombToInit.modifierProperties(ModifierDamageRange, ModifierDamageDuration, ModifierDamageValue);
    }

}
