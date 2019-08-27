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
           
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
             gameBuilder.SendGameBoardBluprint();
            gameBoard.GetBluplirint(gameBuilder.SendGameBoardBluprint());
        }

	}
}
