using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInput : MonoBehaviour {

    
    public int Horizontal { get; private set; }

    public int  Vertical{ get; private set; }
    public bool DropBomb { get; private set; }

    public event Action onFire = delegate { };

    // Update is called once per frame

    void Update()
    {

        Horizontal = (int)Input.GetAxisRaw("Horizontal");
        Vertical = (int)Input.GetAxisRaw("Vertical");
    

        DropBomb = Input.GetButtonDown("Fire1");
        if (DropBomb)
            onFire();

    }
}
