using UnityEngine;
using System.Collections;

public class MyPanel : MonoBehaviour
{
    protected UIPanel panel;

    [SerializeField]
    public int plusDepth = 50;

    protected void Awake()
    {
        panel = GetComponent<UIPanel>();
    }

    public void SetPanelDepth(int depth)
    {
        panel.depth = depth;
    }

    public int GetPanelDepth()
    {
        return panel.depth;
    }

    public void SelfDestroyDepth()
    {
        Main.Instance.DeletePanelAndDepth(this);
    }

    public void SelfDestroy()
    {
        Main.Instance.DeletePanel(gameObject);
    }

}

