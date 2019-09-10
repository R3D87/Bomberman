using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour,IWeaponFire
{

    public BaseBomb bomb;

    int modifierDamageRange;
    int modifierDamageValue;
    int modifierDamageDuration;
 
    //-Interface
    public int ModifierDamageRange { get { return modifierDamageRange; } set {  modifierDamageRange = value; } }
    public int ModifierDamageValue { get { return modifierDamageValue; } set { modifierDamageValue = value; } }
    public int ModifierDamageDuration { get { return modifierDamageDuration; } set { modifierDamageDuration = value; }  }
   
    //- Interface


    [SerializeField]
    float spawnRate = 1;
    float nextFireTime = 0;
    
    private void Start()
    {
        
    }
    private void Update()
    {
        
    }
    bool CanSpawnBomb()
    {
        return Time.time >= nextFireTime;
    }
    void SpawnBomb()
    {
        nextFireTime = Time.time + spawnRate;
      
    }

    public void Spawn(BaseUnit unit,BaseTile baseTile)
    {
       BaseBomb bombInst = Instantiate(bomb, unit.transform.position, Quaternion.identity);
        InitBomb(ref bombInst, baseTile);



    }
    void InitBomb(ref BaseBomb bombToInit, BaseTile tile )
    {
        bombToInit.tile = tile;
        tile.AddObjectToTile(bombToInit);
        bombToInit.modifierProperties(ModifierDamageRange, ModifierDamageDuration, ModifierDamageValue);

    }

}
