using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class wolfSkillAttackScript : MonoBehaviour {
	//通常攻撃はこちらのスクリプトで制御する
	public Collider2D _basicAttackCollider;

	private bool firstContact = false;
	private bool deleyFlag = false ;
	public float _thisSkillWaitSec ;

	private HashSet<Transform> lastFrameAttackTarget;
	private allEnemyBase aEB;

	public GameObject _skillAttackPrefabs;
	public GameObject _animationBase;

	// Use this for initialization
	void Start () {
		lastFrameAttackTarget  = new HashSet<Transform>();
		aEB = this.transform.parent.gameObject.GetComponent<allEnemyBase>();

	}

	/*
	public void aa(){
		while(true){
			//実質毎フレーム
			yield return new WaitForSeconds (1f / 61f);
			
			if (lastFrameAttackTarget.Count != 0 && deleyFlag == false){
				yield return StartCoroutine(this.mostNearEnemyAttacking());
			}
		}
	}*/
	
	public void skillAttack(){
		if (lastFrameAttackTarget.Count != 0 && deleyFlag == false){
			StartCoroutine(this.mostNearEnemyAttacking());
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

		//Debug.Log ("skill!!");

		wolfSkillAnimationPosition tmpPosiS = _animationBase.GetComponent<wolfSkillAnimationPosition> ();
		tmpPosiS.setTargetPosition(nearTarget.transform.position);
		tmpPosiS.startXPosition();


		Animator thisAnimetor = _animationBase.GetComponentInChildren<Animator>();
		thisAnimetor.SetTrigger ("setSkill");

		_basicAttackCollider.enabled = false;	//通常攻撃の停止

		this.GetComponent<chara_attackEreaVisible>().setVisibleThisCicle();

		StartCoroutine (this.skillAttackWait(nearTarget.transform.position, aEB.getAttackingPower() * 2.1f) );
		//Debug.Log ("1");
		StartCoroutine (this.basicAttackWait());
		//Debug.Log ("2");
		StartCoroutine (this.attackDeleyClearer());
		//Debug.Log ("3");
	}

	
	IEnumerator skillAttackWait(Vector3 argsV, float argsDamage){
		yield return new WaitForSeconds(0.5f);

		GameObject tmpGO = Instantiate(_skillAttackPrefabs, argsV, Quaternion.identity) as GameObject;

		Vector3 tempV3 = new Vector3 (this.transform.parent.position.x, this.transform.parent.position.y +1f, 0f);
		Vector3 diff = (tempV3 - tmpGO.transform.position).normalized;
		//Debug.Log(diff);

		tmpGO.transform.rotation =  Quaternion.FromToRotation(Vector3.up, diff);

		tmpGO.GetComponent<enemySkillColliderBase>().setDealDamage(argsDamage);
	}


	IEnumerator basicAttackWait(){
		yield return new WaitForSeconds(3f);
		
		_basicAttackCollider.enabled = true;	//通常攻撃の停止
		//Debug.Log ("2-2");
	}

	IEnumerator attackDeleyClearer(){
		yield return new WaitForSeconds(_thisSkillWaitSec);
		//Debug.Log ("3-2");
		
		deleyFlag = false;
	}

	void OnTriggerStay2D(Collider2D c){
		if (c.gameObject.name.Substring(0, 9) == "charaBase") {
			//衝突相手がキャラクターユニットの場合のみ、以下の動作を実行する

			//Debug.Log ("check");

			if (firstContact == false ) {
				//最初の接触が済んでいない場合、通常攻撃から入る
				deleyFlag = true;
				firstContact = true;
				_basicAttackCollider.enabled = true;	//使用可能に

				StartCoroutine( attackDeleyClearer() );

				return;
			}

			if (deleyFlag == false) {
					//リストにストック
					lastFrameAttackTarget.Add(c.transform);
			}
		}
	}
}
