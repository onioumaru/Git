using UnityEngine;
using System.Collections;

public class charaSkillColliderScript : MonoBehaviour {
	private allCharaBase thisChara;

	void OnTriggerEnter2D(Collider2D c){
		if (c.gameObject.name.Substring (0, 10) == "AttackErea") {
			return;
		}

		switch (thisChara.thisChara.charaNo) {
		case enumCharaNum.syusuran_02:
		case enumCharaNum.enju_01:
		default:
			
			int tmpDm = Mathf.FloorToInt(thisChara.getCharaDamage()) + Mathf.FloorToInt(Random.value * 4);
			Debug.Log (thisChara.getCharaDamage());
			//float retExt = 
			c.gameObject.GetComponent<allEnemy> ().setDamage (tmpDm);

			break;
				}
	}

	void OnTriggerStay2D(Collider2D c){
		if (c.gameObject.name.Substring (0, 10) == "AttackErea") {
			return;
		}
	}

	void OnTrigger2D(Collider2D c){
		if (c.gameObject.name.Substring (0, 10) == "AttackErea") {
			return;
		}
	}

	public void setBaseCharaScript(allCharaBase argsChara){
		thisChara = argsChara;
		//Debug.Log (thisChara.thisChara.charaNo);
	}
}
