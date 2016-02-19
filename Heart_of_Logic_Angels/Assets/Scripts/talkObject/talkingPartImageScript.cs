using UnityEngine;
using System.Collections;

public class talkingPartImageScript : MonoBehaviour {
	public Vector3 _defaultLocalScale;
	public Vector3 _defaultLocalPosition;


	// Use this for initialization
	void Start () {

		if (_defaultLocalScale != Vector3.zero) {
			this.transform.localScale = _defaultLocalScale;
		}

		if (_defaultLocalPosition != Vector3.zero) {
			this.transform.localPosition = _defaultLocalPosition;
		}
	}
}
