using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FactoryEntities: MonoBehaviour {

    // Use this for initialization
    float threshold = 0.3f;
    ISpawnEntity spawnEntity;
    private IPowerUp powerUp;
    private IEnemy enemy;

    private void Start()
    {
        powerUp = GetComponent<IPowerUp>();
        enemy = GetComponent<IEnemy>();
        var spawnEntity = FindObjectsOfType<MonoBehaviour>().OfType<ISpawnEntity>();
    }
    bool HasSpawnEnemy()
    {
        float Rnd = Random.Range(0.8f, 1f);
        return (threshold <= Rnd);

    }
    void SpawnEntity()
    {
        
    }
    IEnumerator WaitForChangedEntitiesOcuppiedTile(BaseTile tile)
    {
        yield return new WaitForSeconds(1f);
        if (HasSpawnEnemy())
        {
            Debug.Log("Factory");
            powerUp.ChanceToSpawnPowerUp(tile);
            enemy.ChanceToSpawnEnemy(tile);

        }
        
    }
    public void SpawnOpportunity(BaseTile tile)
    {
        StartCoroutine(WaitForChangedEntitiesOcuppiedTile(tile));


    }
}
