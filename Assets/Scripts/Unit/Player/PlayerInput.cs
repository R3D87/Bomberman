using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInput : MonoBehaviour, ICharacterInput {
    
    public int Horizontal { get; private set; }
    public int  Vertical{ get; private set; }
    public bool Fire { get; private set; }

    public void ReadInput()
    {
        Horizontal = (int)Input.GetAxisRaw("Horizontal");
        Vertical = (int)Input.GetAxisRaw("Vertical");
        Fire = Input.GetButtonDown("Fire1");
    }

    void Update()
    {
        ReadInput();
    }
}
