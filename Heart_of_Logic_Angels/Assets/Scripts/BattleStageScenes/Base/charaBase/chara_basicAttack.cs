using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//マイフレーム wakeupするのに必要
[RequireComponent(typeof(Rigidbody2D))]
public class chara_basicAttack : MonoBehaviour {
	//アタックディレイはこの関数内で計算する
	private bool attackDeleyFlag = false;

	private Animator thisAnimetor;
	private allCharaBase parentCharaScrpt;
	private CircleCollider2D thisCldr;

	private List<Transform> lastFrameAttackTarget;
	public GameObject _attackLine;

	// Use this for initialization
	void Start () {
		thisAnimetor = this.transform.parent.GetComponentInChildren<Animator>();
		parentCharaScrpt = this.gameObject.GetComponentInParent<allCharaBase>();
		thisCldr = this.GetComponent<CircleCollider2D> ();

		lastFrameAttackTarget = new List<Transform> ();

		StartCoroutine (mainLoop ());
	}

	void Update(){
		this.GetComponent<Rigidbody2D> ().WakeUp();
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

		attackDeleyFlag = true;
		//移動硬直
		parentCharaScrpt.setMovingFreeze();

		thisAnimetor.SetTrigger("gotoAttack");

		//暫定で10フレームのアニメーションwaitを置く
		for (int loopI = 0; loopI < 3; loopI++){
			yield return null;
		}


		if (nearTarget == null) {
			//ターゲットが他キャラが同一フレームで倒した場合,攻撃モーション中に消滅した場合、ここを通る
			//即終了
			yield break;
		}

		int tmpDm = Mathf.FloorToInt(parentCharaScrpt.getCharaDamage()) + Mathf.FloorToInt(Random.value * 4);

		commonCharaAttack tmpAtk = new commonCharaAttack (nearTarget.gameObject, parentCharaScrpt.gameObject, tmpDm);
		//チェックと実行
		if (tmpAtk.exec() == false) {
			//Debug.Log ("yield break");
			lastFrameAttackTarget.Clear();
			yield break;
		}

		//AttackLineの表示
		GameObject tmpAttackLine = Instantiate(_attackLine) as GameObject;
		Vector3[] tmpV3 = new Vector3[]{parentCharaScrpt.transform.position, nearTarget.transform.position};
		tmpAttackLine.GetComponent<LineRenderer> ().SetPositions (tmpV3);

		lastFrameAttackTarget.Clear();


		//Attack Cycleの表示
		parentCharaScrpt.setAttackCycleShow();
		
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

