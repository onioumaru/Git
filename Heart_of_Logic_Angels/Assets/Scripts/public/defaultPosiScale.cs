using UnityEngine;
using System.Collections;

public class defaultPosiScale : MonoBehaviour {
	public Vector3 _defaultPosition;
	public Vector3 _defaultScale;


	// Use this for initialization
	void Start () {
		this.transform.transform.localPosition =_defaultPosition ;
		this.transform.transform.localScale =_defaultScale ;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
}
