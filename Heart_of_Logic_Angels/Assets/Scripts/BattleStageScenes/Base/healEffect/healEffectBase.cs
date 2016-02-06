using UnityEngine;
using System.Collections;

public class healEffectBase : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D co){
		if (co.name.Length > 9) {
			if (co.name.Substring(0,9) == "charaBase") {
				allCharaBase tmpAC = co.gameObject.GetComponent<allCharaBase>();
				tmpAC.setHealing(2);
			}
		}
	}
}