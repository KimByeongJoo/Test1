using UnityEngine;
using System.Collections;

public class GlobalNavigationPanel : MonoBehaviour
{

    [SerializeField]
    UISprite arrow;

    [SerializeField]
    UISprite bar;

    [SerializeField]
    UIPanel panel;

    bool isOpen = true;
    
    public void SetGlobalNavigationDepthToTop(bool ok = true)
    {
        if(ok)
            panel.depth =  Main.Instance.current_panel_depth + 1;
        else
            panel.depth = Main.Instance.current_panel_depth - 1;
    }

    public void OnOffGlobalBar()
    {
        if (isOpen)
        {
            Vector3 pos = bar.transform.localPosition ;
            pos.x += bar.width;
            bar.transform.localPosition = pos;

            //bar.rightAnchor.absolute = -30;
            arrow.flip = UIBasicSprite.Flip.Horizontally;
            isOpen = false;
        }
        else
        {
            Vector3 pos = bar.transform.localPosition;
            pos.x -= bar.width;
            bar.transform.localPosition = pos;

            arrow.flip = UIBasicSprite.Flip.Nothing;
            isOpen = true;
        }
    }

    public void MakeFriendPanel()
    {
        if (FriendPanel.Instance == null)
        {
            GameObject go = Main.Instance.MakeObjectToTarget("UI/Friend_Panel", Main.Instance.uiTarget);
            Main.Instance.AddPanel(go.GetComponent<MyPanel>());

            //add
            OnOffGlobalBar();
            
        }
    }

    public void MakeHeroPanel()
    {
        if (HeroPanel.Instance == null)
        {
            Main.Instance.current_panel_depth += 500;

            Main.Instance.MakeObjectToTargetAndSetPanelDepth("UI/Hero_Panel", Main.Instance.uiTarget, Vector3.one,
                Main.Instance.current_panel_depth);

            
            //Main.Instance.AddPanel(go.GetComponent<MyPanel>());
            //add
            OnOffGlobalBar();

            SetGlobalNavigationDepthToTop(false);

            //if (AchivementPanel.Instance != null)
            //{
            //    AchivementPanel.Instance.SelfDestroy();
            //}
        }
    }

    public void MakeOptionPanel()
    {
        if (OptionPanel.Instance == null)
        {
            GameObject go = Main.Instance.MakeObjectToTarget("UI/Option_Panel");
            Main.Instance.AddPanel(go.GetComponent<MyPanel>());
            OnOffGlobalBar();

            //if (AchivementPanel.Instance != null)
            //{
            //    AchivementPanel.Instance.SelfDestroy();
            //}
        }
    }
    public void MakeAchivementPanel()
    {
        if (AchivementPanel.Instance == null)
        {
            GameObject go = Main.Instance.MakeObjectToTarget("UI/Achivement_Panel");
            Main.Instance.AddPanel(go.GetComponent<MyPanel>());
            OnOffGlobalBar();
            //if (OptionPanel.Instance != null)
            //{
            //    OptionPanel.Instance.SelfDestroy();
            //}
        }
    }

}
