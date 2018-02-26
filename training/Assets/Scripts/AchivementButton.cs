using UnityEngine;
using System.Collections;

public class AchivementButton : MonoBehaviour {

    [SerializeField]
    UILabel label_name;
    [SerializeField]
    UILabel label_description;
    [SerializeField]
    UILabel label_kingdom_point;

    [SerializeField]
    UISprite kingdom_reward_icon;

    [SerializeField]
    UISprite reward_icon;
    [SerializeField]
    UILabel label_reward_value;

    [System.NonSerialized]
    Vector2 raw_img_Size;

    private void Awake()
    {
        raw_img_Size = new Vector2( kingdom_reward_icon.height, kingdom_reward_icon.width );
    }

    public void Set(string name, string description, int kingdompoint, string kingdom_spriteName, uint reward_value)
    {
        label_name.text = name;
        label_description.text = description;
        label_kingdom_point.text = kingdompoint.ToString();

        Utility.ChangeSpriteAspectSnap( kingdom_reward_icon, kingdom_spriteName, raw_img_Size);

        kingdom_reward_icon.spriteName = kingdom_spriteName;
        label_reward_value.text = reward_value.ToString();
    }
}
