using UnityEngine;
using System.Collections;

public class damageErea : MonoBehaviour {
	private int dealDamage = 999999;

	public void setDealDamage(float argsVal){
		dealDamage = Mathf.FloorToInt (argsVal);
	}
	
	void OnTriggerEnter2D(Collider2D c){
		//Enterのみ
		if (c.gameObject.name.Substring (0, 9) == "charaBase") {
			c.gameObject.GetComponent<allCharaBase> ().setDamage (dealDamage);
		}
	}
}
