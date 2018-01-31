using UnityEngine;
using System.Collections;

public class ChangeRotation : MonoBehaviour {

    [SerializeField]
    GameObject target;

    [SerializeField]
    Vector3 rotation;

    private void Start()
    {
        UIButton button = GetComponent<UIButton>();
        
        EventDelegate del = new EventDelegate(this, "PlusRotationToTarget");        

        del.parameters[0].value = rotation;

        EventDelegate.Add(button.onClick, del);
    }

    public void PlusRotationToTarget(Vector3 vec)
    {
        Vector3 tempVec = target.transform.rotation.eulerAngles;
        tempVec += vec;
        target.transform.rotation = Quaternion.Euler(tempVec);
    }
}
