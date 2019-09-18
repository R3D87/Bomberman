using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour {


    public static event Action OnQuickStart;
    public static event Action OnClickPresetLevel;
    public static event Action OnStartAfterPreparedLevel;
    public static event Action OnChangePaint;
    public static event Func<bool> OnConditionCheck;

    public static UIScript InstanceUI;


    public Text winText;
    public GameObject MainMenu;
    public GameObject LevelMaker;
    public Button StartGameCustom;

    void MakeSingleton()
    {
        if (InstanceUI != null)
        {
            Destroy(gameObject);
        }
        else
        {
            InstanceUI = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Awake()
    {
        MakeSingleton();
    }
    
    private void Start()
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

                Debug.Log("Condition true");
            }
            else
            {

                Debug.Log("Condition false");
                StartGameCustom.interactable = false;
            }
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResetCurrentScene()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
        StartGameCustom.interactable = false;
        MakeLevel();

    }
    
}
   

