using UnityEngine;
using System.Collections;

public class RenderQueue : MonoBehaviour {

    [SerializeField]
    UISprite sprite;

    private void Start()
    {
        Debug.Log(sprite.material.renderQueue);
    }

    public void SetRenderQueue()
    {

    }
}
