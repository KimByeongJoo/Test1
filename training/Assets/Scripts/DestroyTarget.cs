using UnityEngine;
using System.Collections;

public class DestroyTarget : MonoBehaviour {
    [SerializeField]
    GameObject target;

	public void _DestroyTarget()
    {
        Destroy(target);
    }
}
