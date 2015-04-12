using UnityEngine;
using System.Collections;

public class stageSelectOKButton : MonoBehaviour {
	public GameObject _TapEffectPrefab;

	private tapedObjectMotion tapS;
	private stageSelectManagerScript sSMS;
	private battleStageSelectVal tgtStage = null;
	
	void Start(){
		tapS = this.GetComponent<tapedObjectMotion>();
		tapS.defaultScale = 2f;
		tapS.scalePingPong = 0.1f;

		sSMS = stageSelectManagerGetter.getsceneSelectManager ();
	}

	void OnMouseDown(){
		tgtStage = sSMS.getSelecedStageVal ();
		
		//タップエフェクト
		Vector2 mouseDown = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		GameObject tapEffect = Instantiate (_TapEffectPrefab) as GameObject;

		tapEffect.transform.position = mouseDown;
		tapS.actionTapEffect ();

		if (tgtStage == null) {
			return;
				}




	}
}
