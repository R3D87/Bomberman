using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{

    bool[] isDragActive;
    bool[] downInPreviousFrame;

    protected virtual void Start()
    {
        isDragActive = new bool[] { false, false, false };
        downInPreviousFrame = new bool[] { false, false, false }; 
    }

    protected virtual void Update()
    {
        for (int i = 0; i < isDragActive.Length; i++)
        {
            if (Input.GetMouseButton(i))
            {
                if (downInPreviousFrame[i])
                {
                    if (isDragActive[i])
                    {
                        OnDragging(i);
                    }
                    else
                    {
                        isDragActive[i] = true;
                        OnDraggingStart(i);
                    }
                }
                downInPreviousFrame[i] = true;
            }
            else
            {
                if (isDragActive[i])
                {
                    isDragActive[i] = false;
                    OnDraggingEnd(i);
                }
                downInPreviousFrame[i] = false;
            }
        }
    }

    public virtual void OnDraggingStart(int mouseButton) { }

    public virtual void OnDragging(int mouseButton) { }

    public virtual void OnDraggingEnd(int mouseButton) { }

}