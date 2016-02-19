using UnityEngine;
using System.Collections;

public class piyoHoleGenerator : MonoBehaviour {
	public GameObject _piyoPrefabs;
	public float _genaratePerSec = 1f;
	public float _genarateCoolCount = 3f;
	public float _genarateCoollTime = 60f;

	public float _genarateDefaultLevel;

	// Use this for initialization
	void Start () {
		if (_genarateDefaultLevel <= 0) {
			_genarateDefaultLevel = this.GetComponent<allEnemyBase> ()._defaultLevel;
		}

		StartCoroutine ( mainLoop() );
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator mainLoop(){
		int coolTime = 0;

		while(true){
			yield return new WaitForSeconds(_genaratePerSec);

			GameObject tmpGO = (GameObject)Instantiate( _piyoPrefabs);
			tmpGO.transform.position = this.transform.position;

			tmpGO.GetComponent<allEnemyBase>()._defaultLevel = _genarateDefaultLevel;

			enemyStandardMovingScript eSMS = tmpGO.GetComponent<enemyStandardMovingScript>();

			Vector2 tmpV2 = Random.insideUnitCircle;
			//Debug.Log (tmpV2);
			Vector3 tmpV3 = new Vector3(this.transform.position.x + (tmpV2.x * 2f), this.transform.position.y + tmpV2.y ,0f);

			eSMS.setMoveTypeTargetPosi(0 ,tmpV3);

			coolTime++;

			if (coolTime >= _genarateCoolCount){
				coolTime = 0;
				yield return new WaitForSeconds(_genarateCoollTime);
			}

		}
	}


}
