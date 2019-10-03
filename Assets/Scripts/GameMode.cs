using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour {

    GameObject gameBoardObject;
    public GameObject gameBuilderObject;
    Translator translator;
    GameBoard gameBoard;
    GameBuilder gameBuilder;

    private void Awake()
    {
        gameObject.AddComponent<GameBoard>();
        gameBoard = GetComponent<GameBoard>();
    }

    void Start()
    {
        UIScript.OnQuickStart += DebugBuildGame;
        UIScript.OnClickPresetLevel += RequestForPaintedBoard;
        UIScript.OnStartAfterPreparedLevel += OnlyTranslation;
        gameBuilder = gameBuilderObject.GetComponent<GameBuilder>();
    }

    bool Validate(GameObject gameObject)
    {
        return (gameObject != null) ? true : false;
    }

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

    void OnlyTranslation()
    {
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
        ObjectSource[] AllChildren = GetComponentsInChildren<ObjectSource>();
        foreach (ObjectSource child in AllChildren)
        {
            if (child.gameObject == gameObject)
                continue;
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

    private void OnDisable()
    {
        UIScript.OnQuickStart -= DebugBuildGame;
        UIScript.OnClickPresetLevel -= RequestForPaintedBoard;
        UIScript.OnStartAfterPreparedLevel -= OnlyTranslation;
    }
}
