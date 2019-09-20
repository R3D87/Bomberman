
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class PowerUpManager: MonoBehaviour, IPowerUp
{ 

    public PowerUp[] powerUp;
    float threshold = 0.3f;
    int powerUpCounter=0;
    int MaxPowerUpOnBoard = 3;
    bool Quit = false;

    public void ChanceToSpawnPowerUp(BaseTile tile)
    {
        Debug.Log(" Interface PowerUp");
        if (HasSpawnPowerUp() && !Quit)
            SpawnPowerUp(tile);
    }

    void SpawnPowerUp(BaseTile baseTile)
    { 
        if (baseTile != null)
        {
            PowerUp Inst = Instantiate(RandomChosenPowerUp(), baseTile.transform.position, Quaternion.identity);
            baseTile.AddObjectToTile(Inst);
            Inst.OnPowerUpDestroy += DecreasePowerUpCounter;

            powerUpCounter++;
            Debug.Log("PowerUp");
        }
    }

    PowerUp RandomChosenPowerUp()
    {
        int AmountOfEnemyTypes = powerUp.Length;
        int ChosenOne = Random.Range(0, AmountOfEnemyTypes);
        return powerUp[ChosenOne];
    }

    bool HasSpawnPowerUp()
    {
        float Rnd = Random.Range(0, 1f);
        return (threshold >= Rnd) && MaxPowerUpOnBoard > powerUpCounter;
    }

    void DecreasePowerUpCounter()
    {
        powerUpCounter--;
    }

    private void OnApplicationQuit()
    {
        Quit = true;
    }
}




