using UnityEngine;
using System.Collections;

public class MedicineItemButton : MonoBehaviour {

    [SerializeField]
    UISprite sprite_button;

    [SerializeField]
    UI2DSprite sprite_icon;
    [SerializeField]
    UILabel label_count;
    [SerializeField]
    UILabel label_name;
    [SerializeField]
    UILabel label_desciption;

    Vector2 rawSize = Vector2.one;

    private void Awake()
    {
        rawSize.x = 66;
        rawSize.y = 64;
    }

    public void Set(string sprite_Name, int count, string name, string description)
    {
        Utility.ChangeSpriteAspectSnap(sprite_icon, Main.Instance.GetItemSpriteByName(sprite_Name), rawSize);
                
        label_count.text = count.ToString();
        label_name.text = name;
        label_desciption.text = description;
    }
    public void SetWidth(int x)
    {
        sprite_button.width = x;        
    }

    public void SetHeight(int y)
    {
        sprite_button.height = y;
    }
    
}
