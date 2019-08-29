using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour {


    public GameObject gameBoardObject;
    public GameObject gameBuilderObject;
    Translator translator;
    GameBoard gameBoard;

    GameBuilder gameBuilder;
    GameObject[,] test;

    public delegate void StartBuilding();
    public static StartBuilding OnstartBuilding;
    // Use this for initialization
    void Start()
    {

        gameBoard = gameBoardObject.GetComponent<GameBoard>();
        gameBuilder = gameBuilderObject.GetComponent<GameBuilder>();

    }
    bool Validate(GameObject gameObject)
    {
        return (gameObject != null) ? true : false;
    }
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            gameObject.AddComponent<Translator>();
            translator = GetComponent<Translator>();
            gameBuilder.SendGameBoardBluprint();
            translator.GetBlueprint(gameBuilder.SendGameBoardBluprint());
            translator.OnFinishBuilding += DestroyBuilder;
            translator.OnFinishBuilding += DestroyAllPaintHelper;
        }


      

 
	}

    void DestroyAllPaintHelper()
    {
 
        Transform[] AllChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in AllChildren)
        {
            if (child.gameObject == gameObject)
                continue;
            Debug.Log(child.gameObject.name);
           Destroy(child.gameObject);
        }
    }

    void DestroyBuilder()
    {
        Destroy(translator);
        Destroy(gameBuilder.gameObject);
    }

}
