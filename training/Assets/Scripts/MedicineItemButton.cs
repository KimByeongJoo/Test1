using UnityEngine;
using System.Collections;

public class MedicineItemButton : MonoBehaviour {

    [SerializeField]
    UISprite sprite_button;

    [SerializeField]
    UITexture texture_icon;
    [SerializeField]
    UILabel label_count;
    [SerializeField]
    UILabel label_name;
    [SerializeField]
    UILabel label_desciption;

    Vector2 rawSize = Vector2.one;

    private void Start()
    {
        rawSize.x = texture_icon.width;
        rawSize.y = texture_icon.height;
    }

    public void Set(string sprite_Name, int count, string name, string description)
    {
        Utility.ChangeSpriteAspectSnap(texture_icon, sprite_Name, rawSize);
                
        label_count.text = count.ToString();
        label_name.text = name;
        label_desciption.text = description;
    }
}
