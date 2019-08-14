using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour {


    public GameBoard gameboard;
    public GameBuilder gameBuilder;
    // Use this for initialization
	void Start () {
		
	}

    bool Validate(GameObject gameObject)
    {
        return (gameObject != null) ? true : false;
    }
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

        }

	}
}
