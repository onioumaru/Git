using UnityEngine;
using System.Collections;

public class uGUIImageRotation : MonoBehaviour {
	private float _rotateSecTime = 0.2f;

	private float stacTime = 0.001f;

	private Quaternion finishedRotate;

	// Use this for initialization
	void Start () {
		finishedRotate = this.transform.localRotation;
	}


	private float  _LimitedScaleM = 40f;
	// Update is called once per frame
	void Update () {
		if (stacTime <= _rotateSecTime) {
			this.transform.Rotate(0f ,0f , 75f);

			//float limitedScaleM = (_rotateSecTime * _rotateSecTime) / (stacTime * stacTime);
			float limitedScaleM = (_rotateSecTime ) / (stacTime );
			if (limitedScaleM > _LimitedScaleM) {
				limitedScaleM = _LimitedScaleM;
			}

			this.transform.localScale = Vector3.one * limitedScaleM * 0.2f ; // * (2f - (_rotateSecTime / stacTime)) * 2f;

			stacTime += Time.fixedDeltaTime;
			if (stacTime >= _rotateSecTime) {
				//初期回転位置に戻す
				this.transform.localRotation = finishedRotate;
				this.transform.localScale = Vector3.one;
			}
		}
	}
}
