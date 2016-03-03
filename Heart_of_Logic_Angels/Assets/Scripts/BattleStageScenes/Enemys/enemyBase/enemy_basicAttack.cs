using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(chara_attackEreaVisible))]
public class enemy_basicAttack : MonoBehaviour {
	public enemyAttackAnimation _animetionPrefabs;

	private List<Transform> lastFrameAttackTarget;

	private allEnemyBase aEB;

	private float thisEnemyAttackDeleySec;
	private bool deleyFlag = false;

	public GameObject _attackEffectPrefab;

	/// <summary>
	/// 直下の親以外のEnemyBaseを指定する場合は、ここで実体を指定する
	/// 基本構造の場合はNullでOK
	/// プレハブ不可
	/// </summary>
	public GameObject _parentBaseNotAutoDetectedTarget;


	// Use this for initialization
	void Start () {
		lastFrameAttackTarget = new List<Transform> ();

		if (_parentBaseNotAutoDetectedTarget == null) {
			aEB = this.transform.parent.gameObject.GetComponent<allEnemyBase> ();
		} else {
			aEB = _parentBaseNotAutoDetectedTarget.transform.GetComponent<allEnemyBase> ();
		}

		thisEnemyAttackDeleySec = aEB.getAttackingDelay ();

		StartCoroutine (mainLoop ());
	}

	IEnumerator mainLoop(){
		while(true){
			//実質毎フレーム
			yield return new WaitForSeconds (1f / 61f);

			if (lastFrameAttackTarget.Count != 0 && deleyFlag == false){
				
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
			Debug.Log ("isNull!!");
			yield break;
			//稀に破壊されてNullになるため
		}

		allCharaBase charaB = nearTarget.gameObject.GetComponent<allCharaBase> ();
		if (charaB.destroyF == true) {
			Debug.Log ("isDestoryFlag!!");
			yield break;
		}

		lastFrameAttackTarget.Clear();
		deleyFlag = true;

		if (_animetionPrefabs != null) {
			//アニメーションがセットしてある場合
			_animetionPrefabs.enabled = true;
			}

		int tmpDm = 1 + Mathf.FloorToInt(aEB.getAttackingPower() + (Random.value * 2));
		charaB.setDamage (tmpDm);

		if (_attackEffectPrefab != null) {
			Instantiate(_attackEffectPrefab, nearTarget.transform.position, Quaternion.identity);
		}

		this.GetComponent<chara_attackEreaVisible>().setVisibleThisCicle();

		//ここで再取得
		thisEnemyAttackDeleySec = aEB.getAttackingDelay ();

		StartCoroutine (this.attackDeleyClearer());
	}


	void OnTriggerStay2D(Collider2D c){
		if (deleyFlag == false) {
			if (c.gameObject.name.Substring(0, 9) == "charaBase") {
				//Attack erea でない

				//リストにストック
				lastFrameAttackTarget.Add(c.transform);
			}
		}
	}
	
	IEnumerator attackDeleyClearer(){
		yield return new WaitForSeconds(thisEnemyAttackDeleySec);
		deleyFlag = false;
	}

}
