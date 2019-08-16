using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseUnit {

    // Use this for initialization

    public int speed = 0;
    public GameObject    bomb;
    public GameObject sprite;

    void Start () {
		
	}



    void Update () {

        float movementH = Input.GetAxis("Horizontal");
        float movementV = Input.GetAxis("Vertical");

        transform.position += Vector3.right * speed * movementH * Time.deltaTime;
        transform.position += Vector3.up * speed * movementV * Time.deltaTime;


        if (Input.GetKeyDown(KeyCode.A))
        {
            
            Instantiate(sprite, new Vector3(0, 0, 0),Quaternion.identity);
            Debug.Log("Sprite");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnBomb();
        }
    }
    void SpawnBomb()
    {
        Instantiate(bomb, transform.position, Quaternion.identity);
    }
}
