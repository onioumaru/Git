using UnityEngine;
using System.Collections;

public class genaratorDaemonScript : MonoBehaviour {
	public GameObject _piyoPrefabs;
	public GameObject _summonPrefabs;

	public float _genarateCnt = 1f;
	public float _genarateCoollTime = 60f;
	public float _genarateCircleRange = 1f;

	public float _genarateDefaultLevel;

	public Animator _animatior;

	// Use this for initialization
	void Start () {
		if (_genarateDefaultLevel <= 0) {
			_genarateDefaultLevel = this.GetComponent<allEnemyBase> ()._defaultLevel;
		}

		StartCoroutine ( mainLoop() );
	}


	IEnumerator mainLoop(){
		while(true){
			_animatior.SetTrigger ("gotoSkill");

			yield return new WaitForSeconds(_genarateCoollTime);
		}
	}

	public void genarateMonster(){

		for (int loopI = 0; loopI < _genarateCnt; loopI++) {
			GameObject tmpGO = (GameObject)Instantiate( _piyoPrefabs);
			tmpGO.transform.position = this.transform.position;

			Vector3 tmpV2 = Random.insideUnitCircle.normalized * _genarateCircleRange;
			tmpGO.transform.position += tmpV2;

			GameObject tmpSummonEff = Instantiate (_summonPrefabs) as GameObject;
			tmpSummonEff.transform.position = tmpGO.transform.position;

			tmpGO.GetComponent<allEnemyBase>()._defaultLevel = _genarateDefaultLevel;
		}
	}


}
