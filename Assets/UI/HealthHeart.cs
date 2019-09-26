using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class HealthHeart : MonoBehaviour {

    // Use this for initialization
    [Header("Color of heart means Amount Player Lives")]
    public Color Heart = new Color(1,0,0);
    [Header("The color of the heart means Amount lost lives by player")]
    public Color LostHeart= new Color(0,0,0);

    SpriteRenderer[] _colorSprites;

    private void Awake()
    {
        _colorSprites = GetComponentsInChildren<SpriteRenderer>();
        ChangeColor(Heart);

    }
    void Start () {

    }

    public void ChangeColor(Color color)
    {
        if (_colorSprites.Length == 0)
            return;
        
            foreach (var sprite in _colorSprites)
            {
                sprite.color = color;
            }
       
    }

}
