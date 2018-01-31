using UnityEngine;
using System.Collections;

public class ActiveTarget : MonoBehaviour {

    [SerializeField]
    GameObject target;

    public void SetActiveTargetOnOff()
    {        
        if (target.activeSelf)
            target.SetActive(false);
        else
            target.SetActive(true);
    }

    public void SetActiveTarget(bool chk)
    {
        target.SetActive(chk);     
    }
}
