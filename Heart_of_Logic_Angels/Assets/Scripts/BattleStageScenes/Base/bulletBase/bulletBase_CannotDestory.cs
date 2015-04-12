using UnityEngine;
using System.Collections;

/*
 * このスクリプトがアタッチされている弾は
 * 攻撃判定だけの子ライダーを持つ
 * 親コライダーのみのこうせいでダメージを与える
 * この弾は必ず時間経過で破壊される物とする。
 * */

[RequireComponent(typeof(Rigidbody2D))]
public class bulletBase_CannotDestory : MonoBehaviour {
	private float dealDamage = 999999f;
	private float deleteTime = 10f;
	
	// Use this for initialization
	void Start () {
		StartCoroutine (removeTime());
	}
	
	public void setRemoveTime(float argsSec){
		deleteTime = argsSec;
	}

	IEnumerator removeTime(){
		//init Time
		yield return new WaitForSeconds (0.1f);
		//Main Wait
		yield return new WaitForSeconds (deleteTime);
		GameObject.Destroy (this.gameObject);
	}

	//
	//
	public void setDealDamage(float argsVal){
		dealDamage = Mathf.Floor(argsVal);
	}
	
	void OnTriggerEnter2D(Collider2D c){
		//Enterのみ
		if (c.gameObject.name.Substring (0, 9) == "charaBase") {
			c.gameObject.GetComponent<allCharaBase> ().setDamage (dealDamage);
		}
	}

	public void setRigidVelocity(Vector2 argsVect){
		this.GetComponent<Rigidbody2D>().velocity = argsVect;
	}
}
