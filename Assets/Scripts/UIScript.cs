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
    public static event Action OnReLoadDuringGame;
    public static event Func<bool> OnConditionCheck;

    public static UIScript InstanceUI;

    public GameObject LosePopup;
    public GameObject WinPopup;
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
        Player.onPlayerDestroy += ShowLosePopup;
    }
    
    private void Start()
    {
        Exit.onFoundPlayer += ShowWinPopup;

    }
    void ShowWinPopup()
    {
        WinPopup.gameObject.SetActive(true);
    }
    public void ShowLosePopup()
    {
        LosePopup.gameObject.SetActive(true);
    }
    void HidePopups()
    {
        LosePopup.gameObject.SetActive(false);
        WinPopup.gameObject.SetActive(false);
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
        HidePopups();
    }

    public void ResetCurrentSceneDuringGame()
    {
        if(OnReLoadDuringGame!=null)
            OnReLoadDuringGame();
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
        StartGameCustom.interactable = false;
        MakeLevel();
        LevelMaker.SetActive(true);
        HidePopups();
    }
    public void TryAgain()
    {
        ResetCurrentSceneDuringGame();

        Invoke("SetPreset", 0.02f);
        Invoke("StartGameOnPreparedLevel", 0.03f);
        Invoke("HidePopups", 0.01f);


    }


}
   

