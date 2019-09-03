using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class Player : BaseUnit
{
    PlayerInput input;
    public static event Action<BaseUnit, bool> OnAnimationProgration;
    private void Awake()
    {
        
    }

    private void Start()
    {
      //  gameObject.AddComponent<PlayerInput>();
        input = gameObject.GetComponent<PlayerInput>();
    }

    
    bool HasInputChanged()
    {
        return input.Horizontal != 0 || input.Vertical != 0;
    }
    bool AnimationProcced = false;
    private void Update()
    {
        if (  HasInputChanged())
        {
            
           Move( input.Horizontal, input.Vertical);
            Debug.Log("X: " + input.Horizontal + " Y: " + input.Vertical);
        }
    }
}
