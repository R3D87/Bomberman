using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(BaseEnemy))]
public class EnemyController : MonoBehaviour, ICharacterInput
{
    BaseEnemy enemy;

    public int Horizontal { get; private set; }
    public int Vertical { get; private set; }
    public bool Fire { get; private set; }
    Coroutine FireCoroutine, MoveCoroutine;
    List<BaseTile> TilesToCheck = new List<BaseTile>();

    List<int[]> InputPreset;
    
   
    
    private void OnEnable()
    {
        InputPreset = new List<int[]> {
           new int[] { 0, 1 },
           new int[] { 0,-1 },
           new int[] { 1, 0 },
           new int[] { -1,0 } };
        enemy = GetComponent<BaseEnemy>();
        Horizontal = 0;
        Vertical = 0;
       
   
    }
   
    private void Start()
    {
        StartCoroutine(MakeDecision());
    }

    IEnumerator MakeDecision()
    {
        bool skipFirstStep = true; ;
        while (true)
        {
            Debug.Log("---");

            Movement();
            if (!skipFirstStep)
                Shoot();
            skipFirstStep = false;

            yield return new WaitForSeconds(0.5f);
        }

    }

    bool Movement()
    {
        Vector2Int Input = new Vector2Int(Horizontal,Vertical);
        Vector2Int PrevInput = Input * -1;

        int NumberOfMovePosibilities;

        TilesToCheck = HarvestData();
        TilesToCheck = RemoveOccupiedTile(TilesToCheck);
        NumberOfMovePosibilities = TilesToCheck.Count;

        if (NumberOfMovePosibilities == 1)
        {
            Input = ConvertTileToInput(TilesToCheck[0]);
            Horizontal = Input.x;
            Vertical = Input.y;
            return true;
        }
       
        BaseTile prevTile = enemy.GetTileInDirection(PrevInput.x, PrevInput.y);
        TilesToCheck.Remove(prevTile);
        if (HasTakePowerOpportunity(TilesToCheck))
        {
            Input = ConvertTileToInput(TakeTileWithPowerUp(TilesToCheck));
        }
        else
        {
            Input = ConvertTileToInput(TilesToCheck[Random.Range(0, TilesToCheck.Count)]);
        }
       
         Horizontal = Input.x;
         Vertical = Input.y;

        return true;
    }

    void Shoot()
    {
        List<BaseTile> TilesToInvestigate = new List<BaseTile>();
        TilesToInvestigate = HarvestData();
        Fire = HasBombToSpawnOpportunity(TilesToInvestigate);

    }

    List<BaseTile> HarvestData()
    {
        List<BaseTile> TilesData = new List<BaseTile>();
        foreach (int[] direction in InputPreset)
        {
            TilesData.Add(enemy.GetTileInDirection(direction[0], direction[1]));
        }
        return TilesData;
    }

    List<BaseTile> RemoveTemporaryBlockedTiles(List<BaseTile> TileToSelect)
    {
        TileToSelect.RemoveAll(i => i.HasTileOccupied() == true);
  
        return TileToSelect;
    }
    List<BaseTile> RemoveOccupiedTile(List<BaseTile> TileToSelect)
    {
         TileToSelect.RemoveAll(x => x.CanBeEntered()==false);
         return TileToSelect;
    }

    bool HasBombToSpawnOpportunity(List<BaseTile> TilesToInspect )
    {
        foreach (BaseTile tileToInspect in TilesToInspect)
        {
            if(tileToInspect.HasTileOccupied())
            {
                Debug.Log("Fire");
                return true;
            }
        }
        return false;
        
    }

    BaseTile TakeTileWithPowerUp( List<BaseTile> TilesToInvestigate)
    {
        return TilesToInvestigate.Find(x => x.HasTilePowerUp() == true);
    }

    bool HasTakePowerOpportunity(List<BaseTile> TilesToInvestigate)
    {
        return TilesToInvestigate.Find(x => x.HasTilePowerUp() == true) != null;
    }

    Vector2Int ConvertTileToInput(BaseTile tile )
    {
        return tile.PositionOnGrid - enemy.GetCoord();
    }

    public void ReadInput()
    {
        throw new System.NotImplementedException();
    }
}
