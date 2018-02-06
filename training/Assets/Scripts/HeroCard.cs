using UnityEngine;
using System.Collections;

public class HeroCard : MonoBehaviour {

    [SerializeField]
    UI2DSprite _sprite;
    [SerializeField]
    UISprite icon_Element;
    [SerializeField]
    UISprite icon_Class;

    public void Set(Sprite sprite, string icon_Element_Name, string icon_Class_Name)
    {
        _sprite.mainTexture = sprite.texture;
        icon_Element.spriteName = icon_Element_Name;
        icon_Class.spriteName = icon_Class_Name;
    }

}
