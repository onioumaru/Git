using UnityEngine;
using System.Collections;

public class healEffect : MonoBehaviour {
	private Rigidbody2D thisRigi;

	private bool reversFlag = false;

	// Use this for initialization
	void Start () {
		thisRigi = this.gameObject.GetComponent<Rigidbody2D> ();

		float tmpFlt = (Random.value * 0.03f) + 0.03f;
		this.gameObject.transform.localScale = new Vector3 (tmpFlt, tmpFlt,0);

		float tmpFlt2 = (Random.value - 0.5f);
		if (tmpFlt2 > 0) {
			thisRigi.angularVelocity = (tmpFlt2 * 720) + 360;
		} else {
			thisRigi.angularVelocity = (tmpFlt2 * 720) - 360;
		}

		StartCoroutine (reverseFlagSec ());
		StartCoroutine (desrotyMe ());
	}
	
	IEnumerator reverseFlagSec(){
		yield return new WaitForSeconds(0.1f);
		reversFlag = true;
	}

	IEnumerator desrotyMe(){
		yield return new WaitForSeconds(0.2f);
		Destroy (this.gameObject);
	}

	
	// Update is called once per frame
	void Update () {

		float tmpFlt = 0;
		if (reversFlag){
			tmpFlt = -0.01f;
		}else{
			tmpFlt = 0.01f;
		}
		Vector3 tmpV = new Vector3(tmpFlt, tmpFlt, 0);

		this.gameObject.transform.localScale += tmpV;

	}
}
