using UnityEngine;
using System.Collections;

public class sorityYesNoDiaglog : MonoBehaviour {
	public GameObject _OkButtonObj;
	public GameObject _CancelButtonObj;

	private bool boubleTapFlag = false;

	// Use this for initialization
	void Start () {
	
	}

	
	public void clickOKButton(){
		if (boubleTapFlag == true) {return;}

		_OkButtonObj.GetComponent<tapedObjectMotion> ().actionTapEffect ();

		StartCoroutine (selectedSaveData ());
	}
	
	public void clickCancelButton(){
		if (boubleTapFlag == true) {return;}
		
		_CancelButtonObj.GetComponent<tapedObjectMotion> ().actionTapEffect ();

		StartCoroutine (hiddenThisTimeWait ());
	}

	IEnumerator hiddenThisTimeWait(){
		boubleTapFlag = true;
		yield return new WaitForSeconds (0.1f);

		this.gameObject.SetActive (false);
		boubleTapFlag = false;
	}
	
	IEnumerator selectedSaveData(){
		boubleTapFlag = true;
		yield return new WaitForSeconds (0.1f);

		staticValueManagerS sVMS = staticValueManagerGetter.getManager ();
		sVMS.changeScene(sceneChangeStatusEnum.gotoBattle);

		boubleTapFlag = false;
	}
}
