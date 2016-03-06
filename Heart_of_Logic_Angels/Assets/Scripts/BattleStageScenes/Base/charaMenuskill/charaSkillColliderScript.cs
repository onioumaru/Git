using UnityEngine;
using System.Collections;

public class charaSkillColliderScript : MonoBehaviour {
	private allCharaBase thisChara;
	private int intervalCnt;

	void OnTriggerEnter2D(Collider2D c){
		
		if (commonCharaAttack.checkTargetCollider(c) == false) {
			return;
		}

		switch (thisChara.thisChara.charaNo) {
		case enumCharaNum.akane_04:
		case enumCharaNum.mokuren_06:
			//stayで処理するのでEnterは何もしない
			break;
		case enumCharaNum.houzuki_05:
		case enumCharaNum.suzusiro_03:
			//防御系は charaSkillDefenceColliderScript
			break;
		case enumCharaNum.sakura_07:
			//サクラは charaSkillColliderSakuraScript を使うので注意
			break;
		case enumCharaNum.syusuran_02:
		case enumCharaNum.enju_01:
		case enumCharaNum.hiragi_09:
		default:
			int tmpDm = Mathf.FloorToInt(thisChara.getCharaDamage()) + Mathf.FloorToInt(Random.value * 4);
			
			commonCharaAttack tmpAtk = new commonCharaAttack (c.gameObject, thisChara.gameObject, tmpDm);
			//チェックと実行
			tmpAtk.exec();

			break;
				}
	}

	void Update(){
		if (Time.timeScale > 0) {
			intervalCnt += 1;
			if (intervalCnt == 120) {
				intervalCnt = 0;
			}
		}
	}

	void OnTriggerStay2D(Collider2D c){
		if (commonCharaAttack.checkTargetCollider(c) == false) {
			return;
		}

		int tmpDm;
				
		switch (thisChara.thisChara.charaNo) {
		case enumCharaNum.mokuren_06:
			if ( (intervalCnt % 10) != 0) {
				return;
			}

			tmpDm = Mathf.FloorToInt(thisChara.getCharaDamage()) + Mathf.FloorToInt(Random.value * 2);

			commonCharaAttack tmpAtk06 = new commonCharaAttack (c.gameObject, thisChara.gameObject, tmpDm);
			//チェックと実行
			tmpAtk06.exec();

			break;
		case enumCharaNum.akane_04:
			if ( (intervalCnt % 5) != 0) {
				return;
			}

			tmpDm = Mathf.FloorToInt(thisChara.getCharaDamage()) + Mathf.FloorToInt(Random.value * 4);

			commonCharaAttack tmpAtk04 = new commonCharaAttack (c.gameObject, thisChara.gameObject, tmpDm);
			//チェックと実行
			tmpAtk04.exec();

			break;
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
