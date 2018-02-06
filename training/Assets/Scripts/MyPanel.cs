using UnityEngine;
using System.Collections;

public class MyPanel : MonoBehaviour {

    UIPanel panel;

    protected void Awake()
    {
        panel = GetComponent<UIPanel>();        
    }

    public void SetPanelDepth2Top()
    {
        //panel.depth = Main.Instance.current_panel_depth;
    }

    public void SetPanelDepth(int depth)
    {
        panel.depth = depth;
    }

    public int GetPanelDepth()
    {
        return panel.depth;
    }

    public void SelfDestroy()
    {
        //Main.Instance.DeletePanel(this);
    }
    
}

