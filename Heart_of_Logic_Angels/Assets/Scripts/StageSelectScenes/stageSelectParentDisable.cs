using UnityEngine;
using System.Collections;

public class stageSelectParentDisable : MonoBehaviour {
	public GameObject _yesNoDialog;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void diseableThisObject(){
		_yesNoDialog.SetActive (false);
		this.transform.parent.gameObject.SetActive (false);
	}
}
