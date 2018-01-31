using UnityEngine;
using System.Collections;

public class OptionAccountTab_Content : MonoBehaviour {


    public void ClickCouponExchangeButton()
    {
        Main.Instance.MakeObjectToTarget("TextInputPopup", gameObject, Vector3.left * 100);
    }
}
