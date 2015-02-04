using UnityEngine;
using System.Collections;

public class greenFlag : MonoBehaviour {

	void Start() {
		print("Start!");
	}

	void Update(){
		// for debug
		if (Input.GetMouseButtonDown (0)) {
			Vector2 tapPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			transform.position = tapPoint;
			FindObjectOfType<allChara>().stopFlag=false;
		}
	}
}
