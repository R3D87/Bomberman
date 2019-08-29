using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseUnit {

    // Use this for initialization

    public int speed = 0;
    public GameObject    bomb;


    PlayerInput playerInput;

   
    void Start ()
    {
        gameObject.AddComponent<PlayerInput>();
        playerInput = GetComponent<PlayerInput>();
        playerInput.onFire += SpawnBomb;
	}

    void PlayerMovemnet()
    {

        transform.position += Vector3.right * speed * playerInput.Horizontal*Time.deltaTime;
        transform.position += Vector3.up * speed * playerInput.Vertical* Time.deltaTime;
    }

    void Update () {



        PlayerMovemnet();


    }
    void SpawnBomb()
    {
        Instantiate(bomb, transform.position, Quaternion.identity);
    }
}
