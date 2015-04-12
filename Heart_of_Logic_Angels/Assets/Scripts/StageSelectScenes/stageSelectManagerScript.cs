using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class stageSelectManagerScript : MonoBehaviour {
	
	private GameObject mainMenuCanvas;
	private GameObject parentFrame;
	private Image backGroundImage;
	private stageSelectDataStockerScript dataStoker;
	private storyProgress thisStoryProgress;
	private battleStageSelectVal selecedStageVal;
	private Coroutine bgCortn;

	//ステージセレクトシーンのメインキャンバスの GameObject
	public GameObject getMainCanvas(){
		if (mainMenuCanvas == null){
			mainMenuCanvas = GameObject.Find("MainMenuCanvas");
		}
		return mainMenuCanvas;
	}

	//ListInner
	//リストになる親枠
	public GameObject getParentFrame(){
		mainMenuCanvas = this.getMainCanvas ();

		if (parentFrame == null) {
			parentFrame = mainMenuCanvas.transform.Find("ListOuter").Find("ListInner").gameObject;
			//ListOuter
			//ListInner
		}
		return parentFrame;
	}


	//ステージセレクトシーンのバックグラウンドイメージ
	public Image getbackGroundImage(){
		GameObject tmpMainCanvas = getMainCanvas();

		if (backGroundImage == null) {
			backGroundImage = mainMenuCanvas.transform.Find("BackGroundImage").gameObject.GetComponent<Image>();
				}
		return backGroundImage;
	}

	public void setbackGroundImage(Sprite argsSprite){
		//消去
		Image gb = this.getbackGroundImage ();
		gb.sprite = null;

		if (bgCortn != null) {
			StopCoroutine( bgCortn );
			bgCortn = null;
		}

		bgCortn = StartCoroutine( bgImageFadeIn(argsSprite) );
	}

	IEnumerator bgImageFadeIn(Sprite argsSprite){
		backGroundImage.sprite = argsSprite;

		Color tmpC = new Color (1, 1, 1, 0);
		backGroundImage.color = tmpC;

		float fadeCnt = 30f;
		float j = 1f / fadeCnt;

		for (float i = 0; i < fadeCnt; i++) {
			yield return new WaitForSeconds(0.01f);

			Color tmpC2 = new Color (1, 1, 1, i * j);
			backGroundImage.color = tmpC2;
		}
	}
	
	//
	private Text txtStageTitle;
	private Text txtStageComment;

	public void setStageInfoText(string argsTitle, string argsComment){
		if (txtStageTitle == null) {
			txtStageTitle = GameObject.Find("selectedStageTitle").GetComponent<Text>();
		}
		if (txtStageComment == null) {
			txtStageComment = GameObject.Find("selectedStageTextInfo").GetComponent<Text>();
		}

		txtStageTitle.text = argsTitle;
		txtStageComment.text = argsComment;
	}

	//データストッカーへのアクセス
	public stageSelectDataStockerScript getDataStocker(){
		if (dataStoker == null) {
			dataStoker = this.transform.parent.gameObject.GetComponentInChildren<stageSelectDataStockerScript>();
		}
		return dataStoker;
	}




	public void setSelecedStageVal(battleStageSelectVal argsStageVal){
		selecedStageVal = argsStageVal;
	}
	public battleStageSelectVal getSelecedStageVal(){
		return selecedStageVal;
	}




	void Start(){
		//staticValues
		staticValuesScript tmpS = GameObject.Find("staticValues").GetComponent<staticValuesScript>();

		thisStoryProgress = tmpS.getStoryProgress ();

		//for debug
		thisStoryProgress.progress = storyProgressEnum.addHouzuki_nextStageSelect01;
		//thisStoryProgress.eventFlag

		this.startThisStageSelectSence ();
		}


	private void startThisStageSelectSence(){
		mainFrameScript mainFrameS = this.getParentFrame().GetComponent<mainFrameScript>();

		mainFrameS.createThisList (thisStoryProgress);
	}
}



//static このマネージャへのアクセス
public static class stageSelectManagerGetter{
	private static stageSelectManagerScript manager;

	public static stageSelectManagerScript getsceneSelectManager(){
		if (manager == null) {
			//stageSelectManager
			manager = GameObject.Find ("stageSelectManager").GetComponent<stageSelectManagerScript>();
		}
		return manager;
	}
}
