using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class chara_basicAttack : MonoBehaviour {
	//アタックディレイはこの関数内で計算する
	private bool attackDeleyFlag = false;

	private Animator thisAnimetor;
	private allCharaBase parentCharaScrpt;

	private List<Transform> lastFrameAttackTarget;

	// Use this for initialization
	void Start () {
		thisAnimetor = this.transform.parent.GetComponentInChildren<Animator>();
		parentCharaScrpt = this.gameObject.GetComponentInParent<allCharaBase>();

		lastFrameAttackTarget = new List<Transform> ();

		StartCoroutine (mainLoop ());
	}

	IEnumerator mainLoop(){
		while (true) {
			yield return new WaitForSeconds(1f/61f);

			if (lastFrameAttackTarget.Count != 0 && attackDeleyFlag == false){

				yield return StartCoroutine(this.mostNearEnemyAttacking());

			}
		}
	}

	IEnumerator mostNearEnemyAttacking(){
		Vector3 tmpV;
		float minLength = 99999f;
		Transform nearTarget = null;

		//再接近ターゲットの確認
		foreach (Transform t in lastFrameAttackTarget){
			if (t != null){

				tmpV = t.transform.position - this.transform.position;

				if (tmpV.magnitude < minLength){
					minLength = tmpV.magnitude;
					nearTarget = t;
				}
			}
		}

		if (nearTarget == null) {
			//ターゲットが他キャラが同一フレームで倒した場合,ここを通る
			//1体の場合、起りやすい
			//即終了
			yield break;
		}

		int tmpDm = Mathf.FloorToInt(parentCharaScrpt.getCharaDamage()) + Mathf.FloorToInt(Random.value * 4);

		commonCharaAttack tmpAtk = new commonCharaAttack (nearTarget.gameObject, parentCharaScrpt.gameObject, tmpDm);
		//チェックと実行
		if (tmpAtk.exec() == false) {
			//Debug.Log ("yield break");
			yield break;
		}


		lastFrameAttackTarget.Clear();
		attackDeleyFlag = true;
		//移動硬直
		parentCharaScrpt.setMovingFreeze();

		//Attack Cycleの表示
		parentCharaScrpt.setAttackCycleShow();

		thisAnimetor.SetTrigger("gotoAttack");
		
		float tmpAttackDeley = parentCharaScrpt.thisChara.battleStatus.thisInfo.attackDelayTime;
		//アタックディレイ
		yield return StartCoroutine(attackDeleyClearer(tmpAttackDeley));

	}
	
	void OnTriggerStay2D(Collider2D c){
		if (attackDeleyFlag == false) {
			if (commonCharaAttack.checkTargetCollider(c)) {
				//Attack erea でない
				
				//Debug.Log("add attack List");
				//リストにストック
				lastFrameAttackTarget.Add(c.transform);
			}
		}
	}

	/*
	void OnTriggerEnter2D(Collider2D c){
		if (attackDeleyFlag == false) {
			if (commonCharaAttack.checkTargetCollider(c)) {
				//Attack erea でない
				
				//リストにストック
				Debug.Log("add List");
				lastFrameAttackTarget.Add(c.transform);
			}
		}
	}
*/

	IEnumerator attackDeleyClearer(float argsDeleyTime){
		yield return new WaitForSeconds(argsDeleyTime);
		attackDeleyFlag = false;
	}

}

