using UnityEngine;
using System.Collections;

public class stageSelectParentDisable : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void diseableThisObject(){
		this.transform.parent.gameObject.SetActive (false);
	}
}
