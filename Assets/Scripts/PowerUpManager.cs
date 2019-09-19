
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class PowerUpManager: MonoBehaviour, IPowerUp 
{

    public PowerUp up;
    float threshold = 0.3f;
    int powerUpCounter=0;
    int MaxPowerUpOnBoard = 3;
    bool Quit = false;
    private void Start()
    {
        
    }

    bool HasSpawnPowerUp()
    {
        float Rnd = Random.Range(0, 1f);
        return (threshold >= Rnd) && MaxPowerUpOnBoard > powerUpCounter;
        
    }
    void SpawnPowerUp(BaseTile baseTile)
    {
        if (baseTile != null)
        {
            PowerUp Inst = Instantiate(up, baseTile.transform.position, Quaternion.identity);
            baseTile.AddObjectToTile(Inst);
            Inst.OnPowerUpDestroy += DecreasePowerUpCounter;

            powerUpCounter++;
            Debug.Log("PowerUp");
        }
    }
    void DecreasePowerUpCounter()
    {
        powerUpCounter--;
    }

    public void ChanceToSpawnPowerUp( BaseTile tile)
    {
        
        if (HasSpawnPowerUp() && !Quit  )
            SpawnPowerUp(tile);
    }
    private void OnApplicationQuit()
    {
        Quit = true;
    }
}




