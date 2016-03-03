using UnityEngine;
using System.Collections;

public class enemyThunderTowerCollider : MonoBehaviour {

	private GameObject tgtChara;
	private float dealDamade;


	public void setDamage(float argsDamage){
		dealDamade = argsDamage;
	}

	void OnTriggerEnter2D(Collider2D c){
		//Enterなので1体に対して複数回は発生しない
		if (c.gameObject.name.Substring (0, 9) == "charaBase") {

			allCharaBase charaB = c.gameObject.GetComponent<allCharaBase> ();
			charaB.setDamage(dealDamade);
		}
	}

	/// <summary>
	/// 基本的にアニメータからイベントトリガーされる
	/// </summary>
	public void setThisObjectDisable(){
		this.gameObject.SetActive (false);
	}

}
