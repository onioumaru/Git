using UnityEngine;
using System.Collections;

public class piyoThrowingTaru : MonoBehaviour {
	public GameObject _taru_Prefabs;
	public Vector3 _throwRockVector;
	public float _ganarateSec = 2f;

	void Start () {
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

			yield return new WaitForSeconds(_ganarateSec);
		}
	}
}
