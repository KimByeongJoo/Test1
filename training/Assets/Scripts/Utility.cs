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
        sprite.Update();            
    }

    static public void ChangeSpriteAspectSnap(UI2DSprite ui_sprite2d, Sprite sprite, Vector2 raw_size)
    {
        if (sprite == null)
            return; 

        ui_sprite2d.sprite2D = sprite;

        float ratio = sprite.rect.width / sprite.rect.height;

        ui_sprite2d.SetDimensions(Mathf.RoundToInt(ratio * raw_size.y), Mathf.RoundToInt(raw_size.y));

        if (raw_size.x < ui_sprite2d.width)
        {
            ui_sprite2d.SetDimensions(Mathf.RoundToInt(raw_size.x), Mathf.RoundToInt(raw_size.x / ratio));
        }
        ui_sprite2d.Update();
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
        texture.Update();
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
    
    static public string KingdomEnumToKoreanString(HeroPanel.Hero_Kingdom kingdom)
    {
        switch (kingdom)
        {
            case HeroPanel.Hero_Kingdom.all:
                return "국가";
            case HeroPanel.Hero_Kingdom.ancient:
                return "춘추전국";
            case HeroPanel.Hero_Kingdom.chock:
                return "촉나라";
            case HeroPanel.Hero_Kingdom.chohan:
                return "초한쟁패";
            case HeroPanel.Hero_Kingdom.etc:
                return "세외";
            case HeroPanel.Hero_Kingdom.han:
                return "한나라";
            case HeroPanel.Hero_Kingdom.oh:
                return "오나라";
            case HeroPanel.Hero_Kingdom.samurai:
                return "사무라이";
            case HeroPanel.Hero_Kingdom.wii:
                return "위나라";
            default:
                return "";
        }
    }

    static public void CalcPopupPosition(UIPanel panel_popup, ItemBoxPopup popup, Reward_ItemBox itemBox)
    {
        UIWidget boxWidget = itemBox.GetBoxWidget();
        Vector3 box_WorldPos = boxWidget.worldCenter;

        UIWidget popupBgSprite = popup.GetSpriteWidget();       

        float box_half_size_y = (boxWidget.worldCorners[1].y - boxWidget.worldCorners[0].y) / 2;
        float popup_half_size_y = (popupBgSprite.worldCorners[1].y - popupBgSprite.worldCorners[0].y) / 2;
        
        // 아랫쪽에 생성
        Debug.Log("itemBox_world_Pos : " + itemBox.transform.position + "half_size_y : " + box_half_size_y);
        Debug.Log("popup half size : " + popup_half_size_y);

        Vector3 worldPos = itemBox.transform.position;
        if (box_WorldPos.y > UICamera.mainCamera.transform.position.y)
        {
            worldPos.y -= box_half_size_y;
            worldPos.y -= popup_half_size_y;
        }

        //popup.transform.position = worldPos;        
        if (box_WorldPos.y <= UICamera.mainCamera.transform.position.y)
        {
            worldPos.y += box_half_size_y;
            worldPos.y += popup_half_size_y;
        }
        popup.SetPosition(worldPos);

        //CapturedPositionByPanel(panel_popup, popup);
    }

    static public void CapturedPositionByPanel(UIPanel targetPanel, ItemBoxPopup popup)
    {
        UIWidget popupWidget = popup.GetWidget();

        // right 
        float value = popupWidget.worldCorners[3].x - targetPanel.worldCorners[3].x;
        if (value > 0)
        {
            Vector3 tempPos = popupWidget.transform.position;
            tempPos.x -= value;
            popupWidget.transform.position = tempPos;
        }

        // left
        value = popupWidget.worldCorners[0].x - targetPanel.worldCorners[0].x;
        if (value < 0)
        {
            Vector3 tempPos = popupWidget.transform.position;
            tempPos.x -= value;
            popupWidget.transform.position = tempPos;
        }

        // top
        value = popupWidget.worldCorners[1].y - targetPanel.worldCorners[1].y;
        if (value > 0)
        {
            Vector3 tempPos = popupWidget.transform.position;
            tempPos.y -= value;
            popupWidget.transform.position = tempPos;
        }

        // bottom
        value = popupWidget.worldCorners[0].y - targetPanel.worldCorners[0].y;
        if (value < 0)
        {
            Vector3 tempPos = popupWidget.transform.position;
            tempPos.y -= value;
            popupWidget.transform.position = tempPos;
        }        
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
