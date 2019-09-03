using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class BaseObject : MonoBehaviour {

    public delegate void DestroyBaseObject(BaseObject baseObject);
    public event DestroyBaseObject OnDestroyBaseObject;

    public int positionX;
    public int positionY;
    public BaseTile tile;
   
    // Use this for initialization
	void Start ()
    {
        
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnDestroy()
    {
        if(OnDestroyBaseObject!=null)
        OnDestroyBaseObject(this);
    }
}
