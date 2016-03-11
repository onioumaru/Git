using UnityEngine;
using System.Collections;

public class simpleMovingScript : MonoBehaviour {
	public float _defaultMovingSpeed = 0.1f;
	public Vector3 _defaultVector = Vector3.zero;

	void Start (){
		if (_defaultVector != Vector3.zero) {
			this.setSimpleMovingVector (_defaultVector);
		}
	}

	/// <summary>
	/// 移動方向をセットする。Lengthは１で渡すこと(nomaralze)
	/// default moving speed はセットすること
	/// _outRangeLength は使わない
	/// </summary>
	/// <param name="argsV3">Arguments v3.</param>
	public void setSimpleMovingVector(Vector3 argsV3){

		StartCoroutine ( simpleMoveVector(argsV3) );
	}

	IEnumerator simpleMoveVector(Vector3 argsV3){
		while (true) {
			this.transform.position += argsV3 * _defaultMovingSpeed * Time.deltaTime * 35f;

			yield return new WaitForSeconds (0.0001f);
		}
	}


	public void setSimpleMovingUp(){

		StartCoroutine ( simpleMoveUp() );
	}

	IEnumerator simpleMoveUp(){
		while (true) {
			this.transform.position += this.transform.up * _defaultMovingSpeed * Time.deltaTime * 35f;

			yield return new WaitForSeconds (0.0001f);
		}
	}

}
