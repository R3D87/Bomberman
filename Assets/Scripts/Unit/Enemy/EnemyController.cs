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
    List<BaseTile> TilesToCheck = new List<BaseTile>();
    bool Movement()
    {
      
        int RND = Random.Range(0, 4);
        int[] Direction = InputPreset[RND];
        int NumberOfMovePosibilities;
        TilesToCheck = HarvestData();
        
        TilesToCheck = RemoveTemporaryBlockedTiles(TilesToCheck);
        NumberOfMovePosibilities = TilesToCheck.Count;
        
       // Vector2Int Input = new Vector2Int();
            Vector2Int Input = ConvertTileToInput(TilesToCheck[Random.Range(0,TilesToCheck.Count)]);
            Horizontal = Input.x;
            Vertical = Input.y;

        return true;
   

    }
 
    List<BaseTile> HarvestData()
    {
        List<BaseTile> TilesData = new List<BaseTile>();
        foreach (int[] direction in InputPreset)
        {
            TilesData.Add(enemy.GetTileInDirection(direction[0],direction[1]));
        }

        return RemovePermanentBlockedTiles(TilesData);
    }
    List<BaseTile> RemoveTemporaryBlockedTiles(List<BaseTile> TileToSelect)
    {
        TileToSelect.RemoveAll(i => i.HasTileOccupied() == true);
  
        return TileToSelect;
    }
    List<BaseTile> RemovePermanentBlockedTiles(List<BaseTile> TileToSelect)
    {
         TileToSelect.RemoveAll(x => x.GetType() == typeof(Wall));
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

 /*   BaseTile HasTakePowerOpportunity()
    {
        BaseTile TileWithPowerUp = null;
        if (TilesToInvestigate.Find(x => x.HasTilePowerUp() == true) != null)
        {
            TileWithPowerUp = TilesToInvestigate.Find(x => x.HasTilePowerUp() == true);
        }
        return TileWithPowerUp;
    }*/
    Vector2Int ConvertTileToInput(BaseTile tile )
    {
        return tile.PositionOnGrid - enemy.GetCoord();
    }
   void SelectDirection()
    {

        // RemovePermanentBlockedTiles();

        int NumberOfMovePosibilities = 0;
       // if (NumberOfMovePosibilities == 1)
           // return TilesToInvestigate[0];


        Dictionary<BaseTile, int> TileRating = new Dictionary<BaseTile, int>();
        Vector2Int previousInput = new Vector2Int(Horizontal, Vertical);

        if (enemy.HasOpportunityToMove(previousInput[0], previousInput[1]))
        {
            BaseTile nextTile = enemy.GetTileInDirection(previousInput[0], previousInput[1]);
        }

        Vector2Int invertInput = previousInput * -1;
        BaseTile previousTile = enemy.GetTileInDirection(invertInput[0], invertInput[1]);
       // BaseTile tile = HasTakePowerOpportunity();
        TileRating[previousTile] = -1;
      //  TileRating[tile] = 1;

       // TileRating.;

        var d = TileRating.OrderBy(x => x.Value);
    }
    void Shoot()
    {
        List<BaseTile> TilesToInvestigate = new List<BaseTile>();
        TilesToInvestigate = HarvestData();
        Fire = HasBombToSpawnOpportunity(TilesToInvestigate);

    }
    IEnumerator MakeDecision()
    {
       
        while (true)
        {
            Debug.Log("---");
            Shoot();
            Movement();
            //  ReadInput();
            yield return new WaitForSeconds(0.1f);
        }
        

    }
 

}
