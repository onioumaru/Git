using UnityEngine;
using System.Collections;

public class destoryME : MonoBehaviour {
	public float _waitTime = 0f;
	float countSec = 0.001f;

	// Use this for initialization
	void Start () {
		if (_waitTime == 0f) {
			return;
		}

		StartCoroutine (waitLoop ());
	}

	void FixedUpdate(){
		countSec += Time.fixedDeltaTime;
	}

	IEnumerator waitLoop(){
		while(countSec < _waitTime){
			yield return null;

		}
		Destroy (this.gameObject);
	}

	public void destoryNow(){
		Destroy (this.gameObject);
		
	}

}
