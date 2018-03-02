using UnityEngine;
using System.Collections;

[System.Serializable]
public class GameItemGrade {

    public string       _id;
    public string       _grade;
    public string       _name;
    public uint         _star;
    public uint         _order;
    public Color        _color;
    public string       _tier;
    
    public void Set(string id, string grade, string name, string star, string order, string color, string tier)
    {
        _id = id;
        _grade = grade;
        _name = name;
        if (star.Length != 0)
            _star = uint.Parse(star);
        if (order.Length != 0)
            _order = uint.Parse(order);
        if (color.Length != 0)
        {
            string[] str_color = color.Split(' ');
            _color.r = float.Parse(str_color[0]);
            _color.g = float.Parse(str_color[1]);
            _color.b = float.Parse(str_color[2]);
            _color.a = 1.0f;
        }
        _tier = tier;
    }
}

