using UnityEngine;
using System.Collections;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour{

    private static T _instance;

    public static T Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType(typeof(T)) as T;

                if (_instance == null)
                {
                    Debug.LogWarning("There's no active " + typeof(T) + "in this scene");
                }
            }
            return _instance;
        }
    }

    private void OnDestroy()
    {
        _instance = null;
    }
}
