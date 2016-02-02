using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class stageSelectManagerScript : MonoBehaviour {
	
	private GameObject mainMenuCanvas;
	private GameObject parentFrame;
	private Image backGroundImage;
	private battleStageSelectVal selecedStageVal;
	private Coroutine bgCortn;

	private staticValueManagerS sVMS;

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
		//GameObject tmpMainCanvas = getMainCanvas();

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

		Color tmpC = new Color (0.5f, 0.5f, 0.5f, 0);
		backGroundImage.color = tmpC;

		float fadeCnt = 30f;
		float j = 1f / fadeCnt;

		for (float i = 0; i < fadeCnt; i++) {
			yield return new WaitForSeconds(0.01f);

			Color tmpC2 = new Color (0.5f, 0.5f, 0.5f, i * j);
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

	public void setSelecedStageVal(battleStageSelectVal argsStageVal){
		//stageSelectNode などから呼ばれる
		selecedStageVal = argsStageVal;
	}
	public battleStageSelectVal getSelecedStageVal(){
		return selecedStageVal;
	}




	void Start(){
		Debug.Log ("In this scene, fastest Start is " + this.name);
		soundManagerGetter.getManager ().playBGM (10);

		sVMS = staticValueManagerGetter.getManager ();

		int thisStoryRoute = sVMS.getStoryRoute ();
		int thisStoryProgress = sVMS.getStoryProgress();
		int thisStoryStage = sVMS.getStoryStage ();

		//for debug
		//thisStoryProgress = 4;

		Debug.Log ("chapter " + thisStoryProgress + " Stage" + thisStoryStage);
		
		mainFrameScript mainFrameS = this.getParentFrame().GetComponent<mainFrameScript>();
		mainFrameS.createThisList (thisStoryRoute, thisStoryProgress, thisStoryStage);
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
