using UnityEngine;
using System.Collections;

public class okButtonScript : MonoBehaviour {
	private faceIconController fIC;
	private tapedObjectMotion tObj;

	public GameObject _yesNoDialog;

	private staticValueManagerS sVMS;

	// Use this for initialization
	void Start () {
		fIC = this.transform.parent.GetComponentInChildren<faceIconController> ();
		tObj = this.GetComponent<tapedObjectMotion> ();
		sVMS = staticValueManagerGetter.getManager ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void clickOKButton(){

		tObj.actionTapEffect ();

		if (!this.sorityCharaNothing ()) {
			//todo 中止時のメッセージ表示
			return;
			}

		//この時点で staticValueManager に入れてしまう
		bool[] tmpB = fIC.getSorityStatus ();
		sVMS.setSortieCharaNo (tmpB);

		_yesNoDialog.SetActive (true);

	}

	private bool sorityCharaNothing(){
		//bool retB = true;
		bool[] tmpB = fIC.getSorityStatus ();
		
		//出撃チェック
		for (int loopI = 0; loopI < 9; loopI++) {
			if (tmpB[loopI]){
				//一人でも出撃していればOK
				return true;
			}
		}
		//ここまで来るのは、全てFalseのばあいのみ
		return false;
	}

	
	public void cancelButton(){
		
		tObj.actionTapEffect ();

		sVMS.changeScene (sceneChangeStatusEnum.gotoStageSelect);
		
	}
}
