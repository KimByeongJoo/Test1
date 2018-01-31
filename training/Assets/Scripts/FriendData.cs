using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class FriendData {
    public string _name;
    public string  _id;
    public string  _picture;
    public int    _lastlogin;

    public bool _chkBox;
    public bool _deleteMask;

    public void SetData(string name, string id, int lastlogin, string picture)
    {
        _id = id;
        _name = name;
        _lastlogin = lastlogin;
        _picture = picture;
        _chkBox = false;
        _deleteMask = false;
    }
}