
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class PowerUpManager: MonoBehaviour, IPowerUp 
{

    public PowerUp up;
    int threshold = 1;
    int powerUpCounter;
    bool Quit = false;

    bool HasSpawnPowerUp()
    {
        int Rnd = Random.Range(0, 1);
        return (threshold <= Rnd);
        
    }
    void SpawnPowerUp(BaseTile baseTile)
    {
        PowerUp Inst = Instantiate(up, baseTile.transform.position, Quaternion.identity, gameObject.transform);
        baseTile.AddObjectToTile(Inst);
    }

    public void ChanceToSpawnPowerUp( BaseTile tile)
    {
        if (HasSpawnPowerUp() && !Quit)
            SpawnPowerUp(tile);
    }
    private void OnApplicationQuit()
    {
        Quit = true;
    }
}




