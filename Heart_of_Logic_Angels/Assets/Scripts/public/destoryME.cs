using UnityEngine;
using System.Collections;

public class destoryME : MonoBehaviour {
	public float _waitTime = 0f;

	// Use this for initialization
	void Start () {
		StartCoroutine (waitLoop ());
	}

	IEnumerator waitLoop(){
		float countSec = 0.001f;

		while(countSec < _waitTime){
			yield return null;
			countSec += Time.fixedDeltaTime;
		}

		Destroy (this.gameObject);
	}
}
