using UnityEngine;
using System.Collections;

public class GlobalNavigationPanel : MonoBehaviour {

    [SerializeField]
    UISprite arrow;

    [SerializeField]
    UISprite bar;

    bool isOpen = false;

    public void OnOffGlobalBar()
    {
        if(isOpen)
        {
            bar.rightAnchor.absolute = -30;
            arrow.flip = UIBasicSprite.Flip.Nothing;
            isOpen = false;
        }
        else
        {
            bar.rightAnchor.absolute = 128;
            arrow.flip = UIBasicSprite.Flip.Horizontally;
            isOpen = true;
        }
    }	

    public void MakeOptionPanel()
    {
        if (OptionPanel.Instance == null)
        {            
            GameObject go = Main.Instance.MakeObjectToTarget("Option_Panel");
            OnOffGlobalBar();
        }
    }
    public void MakeAchivementPanel()
    {
        if (AchivementPanel.Instance == null)
        {
            GameObject go = Main.Instance.MakeObjectToTarget("Achivement_Panel");
            OnOffGlobalBar();
        }
    }

}
