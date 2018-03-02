using UnityEngine;
using System.Collections;

public class ItemBoxPopup : MonoBehaviour {

    [SerializeField]
    UIWidget widget;
    public UIWidget GetWidget() { return widget.GetComponent<UIWidget>(); }

    [SerializeField]
    GameObject target;
    [SerializeField]
    UISprite sprite_bg;
    public UISprite GetSpriteWidget() { return sprite_bg; }

    [SerializeField]
    UILabel label_name;
    [SerializeField]
    UILabel label_count;
    [SerializeField]
    UILabel label_count_name;
    [SerializeField]
    UILabel label_description;

    [SerializeField]
    UILabel label_hero_name;
    [SerializeField]
    UILabel label_hero_kingdom;

    Reward_ItemBox _itemBox;    

    public void Set(Reward_ItemBox itemBox, string itemKind)
    {
        if (itemKind == "GameItemType")
        {
            ItemTypeData data = MyCsvLoad.Instance.GetGameItemTypeByID(itemBox.save_itemId);

            label_name.text = data._name;
            label_count.text = itemBox.save_count;
            label_description.text = data._description;

            label_name.gameObject.SetActive(true);
            label_hero_name.gameObject.SetActive(false);
            label_hero_kingdom.gameObject.SetActive(false);
            label_count.gameObject.SetActive(true);
            label_count_name.gameObject.SetActive(true);

            if (_itemBox == null)
            {
                GameObject go = Main.Instance.MakeObjectToTarget(ObjectPool.Instance.GetPrefab("UI/Reward_ItemBox"), target);
                _itemBox = go.GetComponent<Reward_ItemBox>();
                
                Color tempColor = MyCsvLoad.Instance.GetGameItemGradeByGradeID(data._grade)._color;
                _itemBox.Set(itemBox.save_itemKind, itemBox.save_count, itemBox.save_sprite, itemBox.save_itemId, itemBox.save_star, tempColor);
            }
            else
            {
                Color tempColor = MyCsvLoad.Instance.GetGameItemGradeByGradeID(data._grade)._color;
                _itemBox.Set(itemBox.save_itemKind, itemBox.save_count, itemBox.save_sprite, itemBox.save_itemId, itemBox.save_star, tempColor);
            }
            _itemBox.SetActiveCount(false);
        }
        else if (itemKind == "HeroLetter")
        {
            HeroTypeData data = MyCsvLoad.Instance.GetHeroTypeByID(itemBox.save_itemId);

            label_hero_name.gameObject.SetActive(true);
            label_hero_kingdom.gameObject.SetActive(true);
            label_name.gameObject.SetActive(false);
            label_count.gameObject.SetActive(false);
            label_count_name.gameObject.SetActive(false);

            label_hero_name.text = data._name;
            label_hero_kingdom.text = data._kingdom;

            HeroPanel.Hero_Kingdom _kingdom;
            _kingdom = (HeroPanel.Hero_Kingdom)System.Enum.Parse(typeof(HeroPanel.Hero_Kingdom),data._kingdom);
            label_hero_kingdom.text = Utility.KingdomEnumToKoreanString(_kingdom);

            if (_itemBox == null)
            {
                GameObject go = Main.Instance.MakeObjectToTarget(ObjectPool.Instance.GetPrefab("UI/Reward_ItemBox"), target);
                _itemBox = go.GetComponent<Reward_ItemBox>();
                _itemBox.Set(itemBox.save_itemKind, itemBox.save_count, itemBox.save_sprite, itemBox.save_itemId, itemBox.save_star, Color.white);
            }
            else
            {
                _itemBox.Set(itemBox.save_itemKind, itemBox.save_count, itemBox.save_sprite, itemBox.save_itemId, itemBox.save_star, Color.white);
            }
            _itemBox.SetActiveCount(false);
        }

        float value = sprite_bg.worldCorners[0].y - label_description.worldCorners[0].y;

        while (value < 0)
        {
            NGUIMath.ResizeWidget(sprite_bg, UIWidget.Pivot.Bottom, 0, 10, 0, 0, 1000, 1000);
            value = sprite_bg.worldCorners[0].y - label_description.worldCorners[0].y;
        }        

        while (value > 0)
        {
            NGUIMath.ResizeWidget(sprite_bg, UIWidget.Pivot.Bottom, 0, -10, 0, 0, 1000, 1000);
            value = sprite_bg.worldCorners[0].y - label_description.worldCorners[0].y;
        }
        NGUIMath.ResizeWidget(sprite_bg, UIWidget.Pivot.Bottom, 0, -10, 0, 0, 1000, 1000);        
    }

    public void SetPosition(Vector3 pos)
    {
        widget.transform.position = pos;

        //sprite_bg.
    }

}

