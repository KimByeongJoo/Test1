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
        float ratio = (float)atlasSpriteData.width / atlasSpriteData.height;

        //sprite.aspectRatio = ratio;
        sprite.SetDimensions(Mathf.RoundToInt(ratio * raw_size.y), Mathf.RoundToInt(raw_size.y));

        if (raw_size.x < sprite.width)
        {
            sprite.SetDimensions(Mathf.RoundToInt(raw_size.x), Mathf.RoundToInt(raw_size.x / ratio));
        }        
        //sprite.SetDimensions(Mathf.RoundToInt(boxSize.x), Mathf.RoundToInt(boxSize.x / ratio));
        //if (sprite.height > boxSize.y)
        //{
        //    sprite.SetDimensions(Mathf.RoundToInt(ratio * boxSize.y), Mathf.RoundToInt(boxSize.y));
        //}        
    }
}
