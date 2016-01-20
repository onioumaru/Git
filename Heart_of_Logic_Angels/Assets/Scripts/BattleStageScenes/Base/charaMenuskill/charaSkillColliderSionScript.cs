using UnityEngine;
using System.Collections;

public class charaSkillColliderSionScript : MonoBehaviour {
	private allCharaBase thisChara;
	private int intervalCnt;
	private Animator thisZombiAnime;
	private charaSkill_FreezeManager parentFM;

	private float animeFreezSec;

	void Start(){
		thisZombiAnime = this.GetComponent<Animator> ();
		parentFM = this.transform.parent.GetComponent<charaSkill_FreezeManager> ();
	}

	void OnTriggerEnter2D(Collider2D c){

		if (commonCharaAttack.checkTargetCollider(c) == false) {
			return;
		}

		switch (thisChara.thisChara.charaNo) {
		case enumCharaNum.sion_08:
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

			if (animeFreezSec > 0) {
				animeFreezSec -= Time.deltaTime;

				if (animeFreezSec < 0) {
					parentFM.setMovingFlag (true);
					thisZombiAnime.SetTrigger ("setWalk");
				}
			}
		}
	}

	void OnTriggerStay2D(Collider2D c){
		if (commonCharaAttack.checkTargetCollider(c) == false) {
			return;
		}

		int tmpDm;

		switch (thisChara.thisChara.charaNo) {
		case enumCharaNum.sion_08:
			if ((intervalCnt % 120) != 0) {
				return;
			}

			tmpDm = Mathf.FloorToInt (thisChara.getCharaDamage ()) + Mathf.FloorToInt (Random.value * 2);

			commonCharaAttack tmpAtk06 = new commonCharaAttack (c.gameObject, thisChara.gameObject, tmpDm);
			//チェックと実行
			tmpAtk06.exec ();

			parentFM.setMovingFlag (false);
			thisZombiAnime.SetTrigger ("setAttack");

			animeFreezSec = 3f;

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

		//シオンの場合は自動遷移後
		tmpAnime.SetTrigger("gotoSkillDo");

		thisChara.transform.Find("charaAnime").GetComponent<charaAnimationSubScript>().destorySubAnime();
	}

	public void autoChangeAnimation_Idle(){
		thisZombiAnime.SetTrigger ("setIdle");
	}
}
