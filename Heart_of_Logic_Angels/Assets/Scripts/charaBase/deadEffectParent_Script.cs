using UnityEngine;
using System.Collections;

public class deadEffectParent_Script : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(destoryMe() );
	}

	IEnumerator destoryMe(){
		yield return new WaitForSeconds(4.0f);
		Destroy(this.gameObject);
	}
}
