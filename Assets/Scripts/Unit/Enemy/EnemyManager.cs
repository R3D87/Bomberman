using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour,IEnemy {

    public BaseEnemy Enemy;
    float threshold = 0.3f;
   
    bool Quit = false;

    public  int MaxEnemyOnBoard = 3;
    int EnemyCounter = 0;

    void DecreaseEnemies()
    {
        EnemyCounter--;
    }

    bool HasSpawnEnemy()
    {
        float Rnd = Random.Range(0, 1f);
        return (threshold >=Rnd) && MaxEnemyOnBoard > EnemyCounter;

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
