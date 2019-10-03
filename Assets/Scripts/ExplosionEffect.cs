using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : BaseObject {

    float AppearanceTime = 1f;
    float Timer = 0;
    
    private void Update()
    {
        Timer += Time.deltaTime;
        if (AppearanceTime < Timer)
            Destroy(gameObject);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }
}
