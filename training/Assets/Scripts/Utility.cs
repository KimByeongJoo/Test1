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
}
