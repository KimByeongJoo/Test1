using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour {

    Dictionary<string, GameObject> prefabs;

    private static ObjectPool _instance;

    public static ObjectPool Instance
    {
        get
        {
            return _instance;
        }
    }

    private void OnDestroy()
    {
        prefabs.Clear();
        _instance = null;
    }
    
    public void Awake()
    {
        _instance = this;
        prefabs = new Dictionary<string, GameObject>();
    }

    public GameObject GetPrefab(string path)
    {
        GameObject go;
        prefabs.TryGetValue(path, out go);

        if (go != null)
        {
            return go;
        }
        else
        {
            go = Resources.Load(path) as GameObject;
        }
        return go;        
    }

    public bool RemovePrefab(string key)
    {
        if (prefabs != null)
        {
            return prefabs.Remove(key);
        }
        return false;
    }

    public void PrefabsClear()
    {
        prefabs.Clear();
    }
}
