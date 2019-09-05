using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour,IWeaponFire
{

    public BaseBomb bomb;
    
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
        
    }
}
