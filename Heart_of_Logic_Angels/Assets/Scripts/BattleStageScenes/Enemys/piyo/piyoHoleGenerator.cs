using UnityEngine;
using System.Collections;

public class piyoHoleGenerator : MonoBehaviour {
	public GameObject _piyoPrefabs;
	public float _genaratePerSec = 1f;
	public float _genarateCoolCount = 3f;
	public float _genarateCoollTime = 60f;

	public float _genarateDefaultLevel;

	public Vector3 _meetingWorldPosition = Vector3.zero;
	public float _meetingCircleMagnX = 2f;
	public float _meetingCircleMagnY = 1f;
	/// <summary>
	/// The genalate limit.
	/// 生成制限をする場合に使用
	/// 0の場合は、使用しない
	/// 整数で指定
	/// </summary>
	public int _genalateLimit = 0;
	private int genarateCnt = 0;

	// Use this for initialization
	void Start () {
		if (_genarateDefaultLevel <= 0) {
			_genarateDefaultLevel = this.GetComponent<allEnemyBase> ()._defaultLevel;
		}

		StartCoroutine ( mainLoop() );
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

			Vector3 tmpV3 = new Vector3(this.transform.position.x + (tmpV2.x * _meetingCircleMagnX), this.transform.position.y + (tmpV2.y * _meetingCircleMagnY) ,0f);
			//Debug.Log (tmpV2);

			if (_meetingWorldPosition != Vector3.zero) {
				tmpV3 = new Vector3(_meetingWorldPosition.x + (tmpV2.x * _meetingCircleMagnX), _meetingWorldPosition.y + (tmpV2.y * _meetingCircleMagnY) ,0f);
			}

			eSMS.setMoveTypeTargetPosi(0 ,tmpV3);

			coolTime++;

			if (coolTime >= _genarateCoolCount){
				coolTime = 0;
				yield return new WaitForSeconds(_genarateCoollTime);
				genarateCnt++;
			}


			if (_genalateLimit != 0 && _genalateLimit <= genarateCnt) {
				Destroy (this.gameObject);
				break;
			}
		}
	}


}
