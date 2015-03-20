using UnityEngine;
using System.Collections;

public class charaSkillColliderScript : MonoBehaviour {
	private allCharaBase thisChara;

	void OnTriggerEnter2D(Collider2D c){
		if (commonCharaAttack.checkTargetCollider(c) == false) {
			return;
		}

		switch (thisChara.thisChara.charaNo) {
		case enumCharaNum.syusuran_02:
		case enumCharaNum.enju_01:
		default:
			int tmpDm = Mathf.FloorToInt(thisChara.getCharaDamage()) + Mathf.FloorToInt(Random.value * 4);
			
			commonCharaAttack tmpAtk = new commonCharaAttack (c.gameObject, thisChara.gameObject, tmpDm);
			//チェックと実行
			tmpAtk.exec();

			break;
				}
	}

	void OnTriggerStay2D(Collider2D c){
		if (commonCharaAttack.checkTargetCollider(c) == false) {
			return;
		}
	}

	void OnTrigger2D(Collider2D c){
		if (commonCharaAttack.checkTargetCollider(c) == false) {
			return;
		}
	}

	public void setBaseCharaScript(allCharaBase argsChara){
		thisChara = argsChara;
		
		Animator tmpAnime = thisChara.gameObject.GetComponentInChildren<Animator>();
		tmpAnime.SetTrigger("gotoSkillDo");
		//Debug.Log (thisChara.thisChara.charaNo);
	}
}
