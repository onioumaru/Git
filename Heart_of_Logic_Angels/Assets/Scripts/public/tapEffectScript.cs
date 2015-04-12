using UnityEngine;
using System.Collections;

public class tapEffectScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine ( waitTime() );
	}

	IEnumerator waitTime(){
		yield return new WaitForSeconds (1f);
		Destroy (this.gameObject);
	}
}
