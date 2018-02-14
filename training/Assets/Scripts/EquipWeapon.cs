using UnityEngine;
using System.Collections;

public class EquipWeapon : MonoBehaviour {

    [SerializeField]
    GameObject target_Weapon;

    [SerializeField]
    string weapon_path = "Unit/atlas_tkman_ha_hu_don/weapon-mighty_5";

	// Use this for initialization
	void Start () {        
        GameObject go = Main.Instance.MakeObjectToTarget(weapon_path, target_Weapon);
    }
	
}
