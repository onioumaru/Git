using UnityEngine;
using System.Collections;

public class piyoThrowingTaru : MonoBehaviour {
	public GameObject _taru_Prefabs;
	public Vector3 _throwRockVector;
	public float _ganarateSec = 2f;

	private allEnemyBase enemyBase;

	void Start () {
		enemyBase = this.GetComponent<allEnemyBase> ();
		StartCoroutine ( mainLoop() );
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator mainLoop(){
		while(true){
			GameObject tmpGO = (GameObject)Instantiate(_taru_Prefabs);

			tmpGO.SetActive (true);
			tmpGO.transform.parent = null;
			tmpGO.transform.position = this.transform.position;
			//投射セット
			tmpGO.GetComponent<bulletBase_CannotDestory>()._movingSpeedforSec = _throwRockVector;
			tmpGO.GetComponent<bulletBase_CannotDestory>().deleteTime = 30f;

			float tmpDm = enemyBase.getAttackingPower() * 0.8f;
			tmpGO.GetComponent<bulletBase_CannotDestory> ().setDealDamage (tmpDm);

			yield return new WaitForSeconds(_ganarateSec);
		}
	}
}
