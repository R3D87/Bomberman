using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIScript : MonoBehaviour {

    public Text winText;
  
    private void Awake()
    {
        Exit.onFoundPlayer += ShowWinText;
    }
    void ShowWinText()
    {
        winText.gameObject.SetActive(true);
    }

}
