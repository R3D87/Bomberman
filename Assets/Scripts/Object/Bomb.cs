using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {


    int distanceExplotion = 20;
    LayerMask layerMask;
    float Timer = 0;
    void Start () {

        layerMask = LayerMask.GetMask("Destroyable");
       
      

    }

    private void Update()
    {
        Timer+=Time.deltaTime;
        if (Timer >= 3)
        {
            Explotion();
        }
    }
    void Explotion()
    {
        RaycastHit HitInfo;
        Debug.DrawRay(transform.position, 1.5f*Vector3.left, Color.red,int.MaxValue);

            Debug.DrawRay(transform.position, Vector3.right, Color.red, int.MaxValue);
        Debug.DrawRay(transform.position, Vector3.up, Color.red, int.MaxValue);
        Debug.DrawRay(transform.position, Vector3.down, Color.red, int.MaxValue);
        if (Physics.Raycast(transform.position, Vector3.left, out HitInfo, distanceExplotion, layerMask))
        {
            Debug.Log(HitInfo.collider.gameObject.name);
            Destroy(HitInfo.collider.gameObject);
   
        }
        Destroy(gameObject, 0.1f);
    }



    
}
