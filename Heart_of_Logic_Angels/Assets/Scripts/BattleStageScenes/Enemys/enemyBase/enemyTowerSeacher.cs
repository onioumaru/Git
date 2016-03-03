using UnityEngine;
using System.Collections;

public class enemyTowerSeacher : MonoBehaviour {
	//public GameObject _attackColliderParent;
	public GameObject _thunderColliderObject;

	public autoRotateScript _rotateParent;
	public allEnemyBase _allEnemyParent;

	public float _searchWailtSec = 10f;

	private float dealDamade;
	private bool searchEnabledFlag = true;

	private Coroutine tmpC;
	private SpriteRenderer attackCircle;

	public void setDamage(float argsDamage){
		dealDamade = argsDamage;
	}

	void Start(){
		attackCircle = this.GetComponent<SpriteRenderer> ();
	}


	void OnTriggerEnter2D(Collider2D c){
		//void OnTriggerStay2D(Collider2D c){

		if (searchEnabledFlag) {
			if (c.gameObject.name.Substring (0, 9) == "charaBase") {
				_thunderColliderObject.SetActive (true);
				_thunderColliderObject.GetComponent<enemyThunderTowerCollider> ().setDamage( _allEnemyParent.getAttackingPower() );

				searchEnabledFlag = false;
				_rotateParent.setFreezFlag(true);
				attackCircle.enabled = false;

				StartCoroutine( waitTime() );

				_rotateParent.transform.rotation = Quaternion.identity;
				_rotateParent.transform.rotation = LookAt2D.lookAt (c.transform.position, _allEnemyParent.transform.position);

				StartCoroutine (colliderActiveWait ());
			}
		}
	}

	IEnumerator waitTime(){
		yield return new WaitForSeconds (_searchWailtSec);

		searchEnabledFlag = true;
		_rotateParent.setFreezFlag(false);
		attackCircle.enabled = true;
	}

	IEnumerator colliderActiveWait(){
		//子側が勝手にFalseになるので、アクティブにしてやるだけでいい
		yield return new WaitForSeconds (1f);
		_thunderColliderObject.SetActive (true);

		yield return new WaitForSeconds (1f);
		_thunderColliderObject.SetActive (true);
	}
}
