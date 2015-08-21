using UnityEngine;
using System.Collections;

public class stageSelectOKButton : MonoBehaviour {
	private stageSelectManagerScript sSMS;
	private staticValueManagerS sVMS;
	private battleStageSelectVal tgtStage = null;
	
	void Start(){
		sSMS = stageSelectManagerGetter.getsceneSelectManager ();
		sVMS = staticValueManagerGetter.getManager (); 
	}

	public void OnMouseDown(){
		tgtStage = sSMS.getSelecedStageVal ();

		if (tgtStage == null) {
			//return;
			Debug.Log ("return!");
			return;
				}
		//tgtStage.


		sVMS.changeScene (sceneChangeStatusEnum.gotoSortieSelect);

	}
}
