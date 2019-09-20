using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FactoryEntities: MonoBehaviour, IFactory {

    // Use this for initialization
    float threshold = 0.3f;
    private IPowerUp powerUp;
    private IEnemy enemy;

    private void Start()
    {
        powerUp = GetComponent<IPowerUp>();
        enemy = GetComponent<IEnemy>(); 
    }

    bool HasSpawnEnemy()
    {
        float Rnd = Random.Range(0.8f, 1f);
        return (threshold <= Rnd);
    }

    public void SpawnEntiy(BaseTile tile)
    {
        Debug.Log("Factory");
        powerUp.ChanceToSpawnPowerUp(tile);
        enemy.ChanceToSpawnEnemy(tile);
    }
}
