using UnityEngine;
using System.Collections;

public class titleScript : MonoBehaviour {
	public GameObject _saveDataSelecter;

	// Use this for initialization
	void Start () {

		//staticValueManagerS sSMG = staticValueManagerGetter.getManager ();

	}

	public void touchStart(){
		soundManagerGetter.getManager ().playOneShotSound (enm_oneShotSound.charaMenu);
		_saveDataSelecter.SetActive(true);
	}
}
