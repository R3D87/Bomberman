using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour,IEnemy, ISpawnEntity
{

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
    void  SpawnEnemy(BaseTile baseTile)
    {
        
       
        if (baseTile != null)
        {
            EnemyCounter++;
            BaseEnemy Inst = Instantiate(Enemy, baseTile.transform.position, Quaternion.identity);
            baseTile.AddUnitOnTile(Inst);
            Inst.onEnemyDestroy += DecreaseEnemies;
        }
        
    }

    public void ChanceToSpawnEnemy(BaseTile tile)
    {
        Debug.Log("Intercace Enemy Manager");
        if (HasSpawnEnemy() && !Quit)
            
            SpawnEnemy(tile);
    }
    private void OnApplicationQuit()
    {
        Quit = true;
    }

    public void SpawnEntiy(BaseTile tile)
    {
        Debug.Log("Enemy Manager");
    }
}
