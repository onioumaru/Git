using UnityEngine;
using System.Collections;

public class commonCharaAttack
{
	private GameObject targetEnemy;
	private GameObject attackChara;
	private int dealDamage;

	public commonCharaAttack(GameObject argsTarget,GameObject argsChara,int argsDamage){
		targetEnemy = argsTarget;
		attackChara = argsChara;
		dealDamage = argsDamage;
	}
	
	public bool exec(){
		allCharaBase attackCharaBase = attackChara.GetComponent<allCharaBase>();

		//基本チェック
		if (targetEnemy == null) {
			Debug.Log ("isNull!!");
			return false;
		}
		
		this.attackColliderOptional (targetEnemy, attackChara);

		//Script別チェック
		allEnemyBase chkBase = targetEnemy.gameObject.GetComponent<allEnemyBase> ();

		if (chkBase != null) {
			if (chkBase.destoryF == true){
				//Debug.Log ("isDestoryFlag!!");
				return false;
			}

			chkBase.setDamage (dealDamage, attackCharaBase.thisCharaIndex);
			//Check OK
			return true;
		}

		bulletBase_CanDestory chkBlt = targetEnemy.gameObject.GetComponent<bulletBase_CanDestory> ();
		if (chkBlt != null) {
			if (chkBlt.destoryF == true){
				//Debug.Log ("isDestoryFlag!!");
				return false;
			}
			//Check OK

			chkBlt.interceptedThisBullet();
			return true;
		}

		//Debug.Log ("not Selected");

		return false;
	}


	private void attackColliderOptional(GameObject targetEnemy, GameObject attackChara){
		//TorchMan
		if (targetEnemy.name.Length >= 12) {
			if (targetEnemy.name.Substring(0,12) == "torchManBase"){
				targetEnemy.gameObject.GetComponent<torchman_Base>().hitThisEnemy(attackChara.transform);
			}
		}
	}
	//
	//Static
	//
	public static bool checkTargetCollider(Collider2D c){

		//10文字以上の名前
		if (c.gameObject.name.Length < 10) {
			return true;
				}

		//AttackErea で始まる場合のみ、False
		if (c.gameObject.name.Substring (0, 10) == "AttackErea") {
			return false;
				}

		return true;
	}
}

