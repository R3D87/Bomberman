using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour,IEnemy
{
    public int MaxEnemyOnBoard = 3;
    public BaseEnemy[] Enemy;
    float threshold = 1f;//0.3f;
    int EnemyCounter = 0;
    bool Quit = false;

    public void ChanceToSpawnEnemy(BaseTile tile)
    {
        if (HasSpawnEnemy() && !Quit)
            SpawnEnemy(tile);
    }

    void SpawnEnemy(BaseTile baseTile)
    {
        if (baseTile != null)
        {
            EnemyCounter++;
            BaseEnemy Inst = Instantiate(RandomChosenEnemy(), baseTile.transform.position, Quaternion.identity);
            baseTile.AddUnitOnTile(Inst);
            Inst.onEnemyDestroy += DecreaseEnemies;
            Debug.Log("Intercace Enemy Manager");
        }
    }

    BaseEnemy RandomChosenEnemy()
    {
        int AmountOfEnemyTypes = Enemy.Length;
        int ChosenOne = Random.Range(0, AmountOfEnemyTypes);
        return Enemy[ChosenOne];
    }

    void DecreaseEnemies()
    {
        EnemyCounter--;
    }

    bool HasSpawnEnemy()
    {
        float Rnd = Random.Range(0, 1f);
        return (threshold >=Rnd) && MaxEnemyOnBoard > EnemyCounter;
    }



    private void OnApplicationQuit()
    {
        Quit = true;
    }
}
