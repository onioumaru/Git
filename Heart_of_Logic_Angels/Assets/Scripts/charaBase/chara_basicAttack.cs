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
			yield return new WaitForSeconds(1f/60f);

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

		int tmpDm = Mathf.FloorToInt(parentCharaScrpt.getCharaDamage()) + Mathf.FloorToInt(Random.value * 4);


		if (nearTarget == null) {
			Debug.Log ("isNull!!");
			yield break;
			//稀に破壊されてNullになるため
				}
		if (nearTarget.gameObject.GetComponent<allEnemyBase> ().destoryF == true) {
			Debug.Log ("isDestoryFlag!!");
			yield break;
				}

		nearTarget.gameObject.GetComponent<allEnemyBase> ().setDamage (tmpDm, parentCharaScrpt.thisCharaIndex);

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
			if (c.gameObject.name.Substring(0,10) != "AttackErea") {
				//Attack erea でない
				
				//リストにストック
				lastFrameAttackTarget.Add(c.transform);
			}
		}
	}

	/*
	void OnTriggerEnter2D(Collider2D c){
		if (attackDeleyFlag == false) {
			if (c.gameObject.name.Substring(0,10) != "AttackErea") {
				//Attack erea でない
				
				//リストにストック
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

