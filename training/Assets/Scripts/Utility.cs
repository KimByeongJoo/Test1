using UnityEngine;
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

    static public void ChangeSpriteAspectSnap(UITexture texture, string textureName, Vector2 raw_size)
    {
        Sprite sprite = Main.Instance.GetItemSpriteByName(textureName);

        texture.mainTexture = sprite.texture;        
        
        float ratio = sprite.rect.width / sprite.rect.height;

        texture.SetDimensions(Mathf.RoundToInt(ratio * raw_size.y), Mathf.RoundToInt(raw_size.y));        

        if (raw_size.x < texture.width)
        {
            texture.SetDimensions(Mathf.RoundToInt(raw_size.x), Mathf.RoundToInt(raw_size.x / ratio));
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

    static public void SetSpriteSortingLayerRecursive(GameObject parent, string layerName)
    {
        if (parent != null)
        {
            SpriteRenderer sprite = parent.GetComponent<SpriteRenderer>();
            if(sprite)
            {
                sprite.sortingLayerName = layerName;
            }

            Transform trans = parent.transform;

            if (trans.childCount > 0)
            {
                for(int i=0; i< trans.childCount; i++)
                {
                    SetSpriteSortingLayerRecursive(trans.GetChild(i).gameObject, layerName);
                }
            }
        }
    }

    static public void SetRenderQueue(Material material, int queue)
    {
        material.renderQueue = queue;
    }

    static public void SetRenderQueueRecursive(GameObject parent, int queue)
    {
        if (parent != null)
        {
            SpriteRenderer sprite = parent.GetComponent<SpriteRenderer>();

            if (sprite != null)
            {
                sprite.material.renderQueue = queue;
            }

            Transform trans = parent.transform;

            if (trans.childCount > 0)
            {
                for (int i = 0; i < trans.childCount; i++)
                {
                    SetRenderQueueRecursive(trans.GetChild(i).gameObject, queue);
                }
            }
        }
    }

    static public void SetSpriteSortingOrderRecursive(GameObject parent, int sortingOrder)
    {
        if (parent != null)
        {
            SpriteRenderer sprite = parent.GetComponent<SpriteRenderer>();

            if (sprite != null)
            {
                sprite.sortingOrder = sortingOrder;                
            }

            Transform trans = parent.transform;

            if (trans.childCount > 0)
            {
                for (int i = 0; i < trans.childCount; i++)
                {
                    SetSpriteSortingOrderRecursive(trans.GetChild(i).gameObject, sortingOrder);
                }
            }
        }
    }

    static public bool CheckScreenRatio4to3()
    {
        return Screen.height - 0.5f <= ((float)Screen.width / 4 * 3) && ((float)Screen.width / 4 * 3) <= Screen.height + 0.5f;
    }


    // test
    static public void LogRenderQueue(UISprite sprite)
    {
        if(sprite.drawCall)
            Debug.Log(sprite.drawCall.renderQueue);
    }

    static public void LogRenderQueue(UIPanel panel)
    {
        if (panel != null)        
        {
            for (int i = 0; i < panel.drawCalls.Count; i++)
            {
                Debug.Log(panel.drawCalls[i].renderQueue);
            }
        }
    }
}
