using UnityEngine;
using System.Collections;

public class PressColorChange : MonoBehaviour {

    UISprite sprite;

    Color rawColor;

    private void Start()
    {
        sprite = GetComponent<UISprite>();
        rawColor = sprite.color;
    }

    void OnSelect()
    {
        rawColor = sprite.color;
        Color tempColor = sprite.color;
        tempColor.r -= 0.1f;
        tempColor.g -= 0.1f;
        tempColor.b -= 0.1f;
        sprite.color = tempColor;        
    }

    void OnDeselect()
    {
        sprite.color = rawColor;
    }    
}
