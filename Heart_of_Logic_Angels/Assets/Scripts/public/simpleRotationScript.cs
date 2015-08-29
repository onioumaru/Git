using UnityEngine;
using System.Collections;

public class simpleRotationScript : MonoBehaviour {
	public float _rotateTime_halfCycle = 10f;
	public float _rotateCourse = -1f;

	// Use this for initialization
	void Start () {

		StartCoroutine (mainLoop ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	IEnumerator mainLoop(){
		float rotate_perSec = 180f / _rotateTime_halfCycle;

		while (true) {
			this.transform.Rotate(0,0, rotate_perSec * Time.deltaTime * _rotateCourse);;
			
			yield return new WaitForSeconds(0.00001f);
		}

		yield return null;
	}

}
