﻿using UnityEngine;
using System.Collections;

public class Utility {    
            
    static public string GetTimeUnit(uint time)
    {
        if (time >= 43200)
        {
            return string.Format("{0}일", time / 43200);
        }
        else if (time >= 3600)
        {
            return string.Format("{0}시간", time / 3600);
        }
        else if (time >= 60)
        {
            return string.Format("{0}분", time / 60);
        }

        return "방금";
    }
    static public UILabel SetLabelColor(UILabel label, Color color)
    {
        if(label != null)
            label.color = color;
        return label;       
    }

    /// <summary>    
    /// </summary>
    /// <param name="sprite">target</param>
    /// <param name="spriteName">change sprite name</param>
    /// <param name="baseHeight"></param>
    static public void ChangeSpriteAspectSnap( UISprite sprite, string spriteName, Vector2 raw_size)
    {
        sprite.spriteName = spriteName;
        UISpriteData atlasSpriteData = sprite.GetAtlasSprite();

        if (atlasSpriteData == null)
        {
            Debug.LogWarning("no atlas sprite data.");
            return;
        }

        float ratio = (float)atlasSpriteData.width / atlasSpriteData.height;

        sprite.SetDimensions(Mathf.RoundToInt(ratio * raw_size.y), Mathf.RoundToInt(raw_size.y));

        if (raw_size.x < sprite.width)
        {
            sprite.SetDimensions(Mathf.RoundToInt(raw_size.x), Mathf.RoundToInt(raw_size.x / ratio));
        }         
    }

    static public string GetSpriteNameByEnum(HeroPanel.Hero_Class hero_class, bool isWhite = false)
    {
        if(isWhite)
            return string.Format("class_icon_{0}_white", hero_class.ToString());
        else
            return string.Format("class_icon_{0}", hero_class.ToString());        

    }
    static public string GetSpriteNameByEnum(HeroPanel.Hero_Element hero_element)
    {
        return string.Format("element_icon_{0}", hero_element.ToString());        
    }

    static public void SetSpriteSortingLayerRecursive(GameObject root, string layerName)
    {
        if (root != null)
        {
            SpriteRenderer sprite = root.GetComponent<SpriteRenderer>();
            if(sprite)
            {
                sprite.sortingLayerName = layerName;
            }

            Transform trans = root.transform;

            if (trans.childCount > 0)
            {
                for(int i=0; i< trans.childCount; i++)
                {
                    SetSpriteSortingLayerRecursive(trans.GetChild(i).gameObject, layerName);
                }
            }
        }
    }
}
