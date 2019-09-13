using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour,IEnemy {




    public BaseEnemy Enemy;
    int threshold = 0;
    int powerUpCounter;
    bool Quit = false;
    int MaxEnemyOnBoard = 3;
    int EnemyCounter = 0;

    void DecreaseEnemies()
    {
        EnemyCounter--;
    }
        bool HasMaxEnemyOnBoard()
    {
        return MaxEnemyOnBoard <= EnemyCounter;
    }
    bool HasSpawnEnemy()
    {
        int Rnd = Random.Range(0, 1);
        return (threshold <= Rnd) && !HasMaxEnemyOnBoard();

    }
    void SpawnEnemy(BaseTile baseTile)
    {
        EnemyCounter++;
        BaseEnemy Inst = Instantiate(Enemy, baseTile.transform.position, Quaternion.identity, gameObject.transform);
        baseTile.AddUnitOnTile(Inst);
        Inst.onEnemyDestroy += DecreaseEnemies;
    }

    public void ChanceToSpawnEnemy(BaseTile tile)
    {
        if (HasSpawnEnemy() && !Quit)
            SpawnEnemy(tile);
    }
    private void OnApplicationQuit()
    {
        Quit = true;
    }
}
