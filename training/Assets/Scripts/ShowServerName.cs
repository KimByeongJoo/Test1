using UnityEngine;
using System.Collections;

public class ShowServerName : MonoBehaviour {
    [SerializeField] UILabel labelNumber;
    [SerializeField] UILabel labelName;

	public void ShowDebugText()
	{
		Debug.Log (labelNumber.text + " "+ labelName.text);
	}

    public void Set(string number, string name)
    {
        labelNumber.text = number;
        labelName.text = name;
    }
}
