using UnityEngine;
using System.Collections;

public class stage_bg : MonoBehaviour {
	
	private Rigidbody2D rgB;
//	private GameObject ;

	// Use this for initialization
	void Start () {
		rgB = this.gameObject.GetComponentInChildren<Rigidbody2D>();

		rgB.angularVelocity = 0.4f;
	}
	
	// Update is called once per frame
	void Update () {
		//rgB.angularVelocity = 0.4f;

		//this.gameObject.transform.localRotation += Vector3(0f,0f,0.1f);
	}
}
