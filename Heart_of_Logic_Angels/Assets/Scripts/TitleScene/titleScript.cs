using UnityEngine;
using System.Collections;

public class titleScript : MonoBehaviour {
	public GameObject _saveDataSelecter;

	// Use this for initialization
	void Start () {

		//staticValueManagerS sSMG = staticValueManagerGetter.getManager ();

	}

	public void touchStart(){
		_saveDataSelecter.SetActive(true);
	}
}
