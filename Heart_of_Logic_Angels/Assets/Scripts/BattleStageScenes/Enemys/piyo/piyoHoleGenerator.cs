using UnityEngine;
using System.Collections;

public class piyoHoleGenerator : MonoBehaviour {
	public GameObject _piyoPrefabs;
	public float _ganaratePerSec = 1f;

	private float genarateDefaultLevel;

	// Use this for initialization
	void Start () {
		genarateDefaultLevel = this.GetComponent<allEnemyBase> ()._defaultLevel;
		StartCoroutine ( mainLoop() );
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator mainLoop(){
		while(true){
			yield return new WaitForSeconds(_ganaratePerSec);

			GameObject tmpGO = (GameObject)Instantiate( _piyoPrefabs);
			tmpGO.transform.position = this.transform.position;

			tmpGO.GetComponent<allEnemyBase>()._defaultLevel = genarateDefaultLevel;

			enemyStandardMovingScript eSMS = tmpGO.GetComponent<enemyStandardMovingScript>();

			Vector2 tmpV2 = Random.insideUnitCircle;
			Vector3 tmpV3 = new Vector3(this.transform.position.x + tmpV2.x,this.transform.position.y + tmpV2.y ,0f);

			eSMS.setMoveTypeTargetPosi(0 ,tmpV3);
		}
	}


}
