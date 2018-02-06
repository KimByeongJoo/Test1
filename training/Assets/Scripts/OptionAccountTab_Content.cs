using UnityEngine;
using System.Collections;

public class OptionAccountTab_Content : MonoBehaviour {


    public void ClickCouponExchangeButton()
    {
        Main.Instance.MakeObjectToTargetAndSetPanelDepth("TextInputPopup",
            gameObject, Vector3.left * 100, Main.Instance.current_panel_depth + 10);
    }
}
