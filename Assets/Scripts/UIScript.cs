using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;


public class UIScript : MonoBehaviour {


    public static event Action OnQuickStart;
    public static event Action OnClickPresetLevel;
    public static event Action OnStartAfterPreparedLevel;


   public Text winText;
   public GameObject MainMenu;
   public GameObject LevelMaker;

    private void Awake()
    {
        Exit.onFoundPlayer += ShowWinText;
    }
    void ShowWinText()
    {
       winText.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        LevelMaker.SetActive(false);
        MainMenu.gameObject.SetActive(false);
        OnQuickStart();
    }
    public void StartGameOnPreparedLevel()
    {
        OnStartAfterPreparedLevel();
    }
    public void MakeLevel()
    {
            MainMenu.gameObject.SetActive(false);
    }
    public void SetPreset()
    {
        OnClickPresetLevel();
    }
}
