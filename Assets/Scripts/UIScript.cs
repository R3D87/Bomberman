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
    public static event Action OnChangePaint;
    public static event Func<bool> OnConditionCheck;

   

    public Text winText;
   public GameObject MainMenu;
   public GameObject LevelMaker;
    Button StartGameCustom; 
    private void Awake()
    {
        StartGameCustom = GetComponentInChildren<Button>();
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
        LevelMaker.SetActive(false);
        OnStartAfterPreparedLevel();
    }
    public void MakeLevel()
    {
            MainMenu.gameObject.SetActive(false);
    }
    public void SetPreset()
    {
        OnClickPresetLevel();
        FullFillConditions();
    }
    public void CheckChangeOnPaint()
    {
        FullFillConditions();
    }
    private void FullFillConditions()
    {
        
        if (OnConditionCheck != null)
        {
            if (OnConditionCheck())
            {
                StartGameCustom.interactable = true;
            }
            else
            {
                StartGameCustom.interactable = false;
            }
        }
    }
}
