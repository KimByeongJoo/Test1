using UnityEngine;
using System.Collections;

public class GlobalNavigationPanel : MonoBehaviour
{

    [SerializeField]
    UISprite arrow;

    [SerializeField]
    UISprite bar;

    bool isOpen = true;    

    public void OnOffGlobalBar()
    {
        if (isOpen)
        {
            Vector3 pos = bar.transform.localPosition ;
            pos.x += bar.width;
            bar.transform.localPosition = pos;

            //bar.rightAnchor.absolute = -30;
            arrow.flip = UIBasicSprite.Flip.Nothing;
            isOpen = false;
        }
        else
        {
            Vector3 pos = bar.transform.localPosition;
            pos.x -= bar.width;
            bar.transform.localPosition = pos;

            arrow.flip = UIBasicSprite.Flip.Horizontally;
            isOpen = true;
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
