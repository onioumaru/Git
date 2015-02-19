using UnityEngine;
using System.Collections;

public class chara_basicAttack : MonoBehaviour {
	//アタックディレイはこの関数内で計算する
	private bool attackDeleyFlag = false;

	private Animator thisAnimetor;
	private allChara parentCharaScrpt;

	// Use this for initialization
	void Start () {
		thisAnimetor = this.gameObject.GetComponentInParent<Animator>();
		parentCharaScrpt = this.gameObject.GetComponentInParent<allChara>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerStay2D(Collider2D c){
		if (attackDeleyFlag == false) {
			if (c.gameObject.name != "AttackErea") {
				//Attack erea でない

				thisAnimetor.SetTrigger("gotoAttack");

				int tmpDm = Mathf.FloorToInt(parentCharaScrpt.getCharaDamage()) + Mathf.FloorToInt(Random.value * 4);
				float retExt = c.gameObject.GetComponent<allEnemy> ().setDamage (tmpDm);

				//敵が倒せている場合
				if (retExt > 0f) {
					//Lv Up Check
					parentCharaScrpt.getExp(retExt);
				}
				//Attack Cycleの表示
				parentCharaScrpt.setAttackCycleShow();

				float tmpAttackDeley = parentCharaScrpt.thisChara.battleStatus.thisInfo.attackDelayTime;

				attackDeleyFlag = true;
				//移動硬直
				parentCharaScrpt.setMovingFreeze();
				//アタックディレイ
				StartCoroutine (this.attackDeleyClearer(tmpAttackDeley));
			}
		}
	}

	IEnumerator attackDeleyClearer(float argsDeleyTime){
		yield return new WaitForSeconds(argsDeleyTime);
		attackDeleyFlag = false;
	}

}
