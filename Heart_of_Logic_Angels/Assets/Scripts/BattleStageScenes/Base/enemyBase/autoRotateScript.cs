using UnityEngine;
using System.Collections;

public class autoRotateScript : MonoBehaviour {
	public float _sideAngleValue;
	public float _angleSpeedPerSec = 90f;
	public float _returnWait = 10f;

	private bool momentAscending = true;
	private float offsetAngleValue = 0f;
	private float returnWaitCnt = 0f;

	private bool freezFlag = false;
	private Quaternion defaultAngle;

	// Use this for initialization
	void Start () {
		defaultAngle = this.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (freezFlag == true) {return;	}

		if (Time.timeScale != 0) {

			if (returnWaitCnt >= 0) {
				returnWaitCnt -= 1f;
			}

			if (returnWaitCnt <= 0) {
				float tmpAddValue = Time.deltaTime * _angleSpeedPerSec;
				if (momentAscending) {
					this.transform.Rotate (Vector3.forward * tmpAddValue);
					offsetAngleValue += Vector3.forward.z * tmpAddValue;
				} else {
					this.transform.Rotate (Vector3.back * tmpAddValue);
					offsetAngleValue += Vector3.back.z * tmpAddValue;
				}

				//加算方向の判別
				if ((_sideAngleValue * -1f) > offsetAngleValue) {
					momentAscending = true;
					returnWaitCnt = _returnWait;
				}
				if (_sideAngleValue < offsetAngleValue) {
					momentAscending = false;
					returnWaitCnt = _returnWait;
				}
			}
		}
	}

	public void setFreezFlag(bool argsBool){
		freezFlag = argsBool;

		//フリーズから戻った場合、状態をリセット
		if (argsBool == false){
			this.transform.rotation = defaultAngle;
			offsetAngleValue = 0f;
		}
	}
}
