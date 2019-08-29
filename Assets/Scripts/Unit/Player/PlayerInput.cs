using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInput : MonoBehaviour {


    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }
    public bool DropBomb { get; private set; }

    public event Action onFire = delegate { };

    // Update is called once per frame

    void Update()
    {

        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");
        DropBomb = Input.GetButtonDown("Fire1");
        if (DropBomb)
            onFire();

    }
}
