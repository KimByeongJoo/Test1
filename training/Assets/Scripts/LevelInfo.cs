using UnityEngine;
using System.Collections;

[System.Serializable]
public class LevelInfo {
    public int _id;
    public int _exp;

    public void Set(string id, string exp)
    {
        if(id.Length != 0)
            _id = int.Parse(id);
        if (exp.Length != 0)
            _exp = int.Parse(exp);
    }
}
