﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour {


    GameObject gameBoardObject;
    public GameObject gameBuilderObject;
    Translator translator;
    GameBoard gameBoard;

    GameBuilder gameBuilder;
 


    // Use this for initialization
    void Start()
    {
        gameObject.AddComponent<GameBoard>();
        gameBoard = GetComponent<GameBoard>();
       

        gameBuilder = gameBuilderObject.GetComponent<GameBuilder>();

    }
    bool Validate(GameObject gameObject)
    {
        return (gameObject != null) ? true : false;
    }
    // Update is called once per frame

    void Update ()
    {

        if(Input.GetKeyDown(KeyCode.F))
            DebugBuildGame();


    }
    void DebugBuildGame()
    {
        RequestForPaintedBoard();
        AttachTranslatorComponent();
        Invoke("StartedTranslation", 0.01f);
      
    }
    private void RequestForPaintedBoard()
    {
        gameBuilder.PaintingRandomBoard();
    }
    private void StartedTranslation()
    {
        TransferTranslationRequest();
        translator.StartTranslate();
    }

    private void TransferTranslationRequest()
    {
        translator.GetBlueprint(gameBuilder.SendGameBoardBluprint());
    }

    private void AttachTranslatorComponent()
    {
        gameObject.AddComponent<Translator>();
        translator = GetComponent<Translator>();
   
        translator.OnSendTranslation += TrasferTranslation;
     
        gameBoard.OnReceivedTranslationAction +=  DestroyBuilder;
        gameBoard.OnReceivedTranslationAction += DestroyAllPaintHelper;
    }

    void DestroyAllPaintHelper()
    {
 
        Transform[] AllChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in AllChildren)
        {
            if (child.gameObject == gameObject)
                continue;
           // Debug.Log(child.gameObject.name);
           Destroy(child.gameObject);
        }
    }

    void DestroyBuilder()
    {
        Destroy(translator);
        Destroy(gameBuilder.gameObject);
    }
    void TrasferTranslation()
    {
        gameBoard.ReceiveTranslation(translator.SendTranslation());
        
        
    }

}
