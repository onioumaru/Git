using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class enemy_basicAttack : MonoBehaviour {
	
	private List<Transform> lastFrameAttackTarget;


	public float attackDeley = 0.5f;
	private bool deleyFlag = false;

	// Use this for initialization
	void Start () {
		lastFrameAttackTarget = new List<Transform> ();

		StartCoroutine (mainLoop ());
	}
	
	IEnumerator mainLoop(){
		while(true){
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
		
		int tmpDm = 1 + Mathf.FloorToInt(Random.value * 4);
		nearTarget.gameObject.GetComponent<allCharaBase> ().setDamage (tmpDm);
		
		yield return StartCoroutine (this.attackDeleyClearer());
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
		yield return new WaitForSeconds(attackDeley);
		deleyFlag = false;
	}

}
