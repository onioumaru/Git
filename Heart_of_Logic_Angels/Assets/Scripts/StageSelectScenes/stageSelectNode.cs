using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class stageSelectNode : MonoBehaviour {
	public GameObject _TapEffectPrefab;

	private battleStageSelectVal thisStage = null;
	private Text thisNoteText;
	private tapedObjectMotion tapS;
	private stageSelectManagerScript sSMS;


	//public delegate void OnNodeClickEvent();
	//public event EventHandler OnClick;

	public void OnClick_EventTrigger(){

		if (thisStage == null) {
			Debug.Log ("このオブジェクトを使用するのに、初期化されていません。:battleStageSelectVal");
				}
		//マネージャの取得
		stageSelectManagerScript tmpMnger = stageSelectManagerGetter.getsceneSelectManager ();

		//BGの設定先のイメージ
		tmpMnger.setbackGroundImage(thisStage.bgImage);


		Vector2 mouseDown = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		GameObject tapEffect = Instantiate (_TapEffectPrefab) as GameObject;
		tapEffect.transform.position = mouseDown;

		sSMS.setStageInfoText (thisStage.stageTitle, thisStage.stageComment);

		//タップエフェクト
		tapS.actionTapEffect ();
		//選択結果を返す
		sSMS.setSelecedStageVal (thisStage);

	}

	void Start(){
		tapS = this.GetComponent<tapedObjectMotion>();
		sSMS = stageSelectManagerGetter.getsceneSelectManager ();
	}

	public void setStageValues(battleStageSelectVal argsVal){
		thisStage = argsVal;

		thisNoteText = this.transform.Find ("Text").GetComponent<Text> ();

		thisNoteText.text = thisStage.stageTitle;
	}
}
