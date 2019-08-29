using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class BaseObject : MonoBehaviour {

	
    public List<BaseObject> baseObjectsList;
    public List<BaseUnit> baseUnitsList;
    public int positionX;
    public int positionY;

   
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnDestroy()
    {
        
    }
}
