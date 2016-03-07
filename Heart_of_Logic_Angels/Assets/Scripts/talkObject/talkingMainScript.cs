using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
//Regex
using System.Text.RegularExpressions;

public class talkingMainScript : MonoBehaviour {
	
	public GameObject _attachedLeftChara;
	public GameObject _attachedRightChara;
	public GameObject _attachedCenterChara;
	public Image _bgColor;

	public GameObject _partsContainerGO;
	public GameObject _textMainFrame;

	private string[] allTxt;
	private int nowPage;
	private enm_textControllStatus thisStatus;
	private Boolean tapedFlag;
	
	public Image _BGImage;
	public Image _fazerIconSR;

	private Text thisUIText;
	private float txtColorAlpha;
	private float fadeSpeed = 0.1f;
	
	private staticValueManagerS sVMS;
	private soundManager_Base sMB;
	
	public GameObject _groundParent;
	
	public bool _forDebug = false;
	public string _forDebugFileName;
	
	//初期値
	DateTime endWaitTime = DateTime.Now;


	// Use this for initialization
	void Start () {
		Debug.Log ("In this scene, fastest Start is talkingMainScript");

		if (Application.loadedLevelName == "talkScene" ){
			_bgColor.color = Color.black;
		}

		thisUIText = this.transform.GetComponent<Text> ();
		_BGImage.color = new Color(0f, 0f, 0f, 0.3f);

		sVMS = staticValueManagerGetter.getManager ();
		sMB = soundManagerGetter.getManager ();

		//初期値　止める
		Time.timeScale = 0;

		//シーンファイルを取得
		//ない場合はログを吐いて初期値を入れる
		String sceneFileName = sVMS.getTalkingSceneName ();

		if (_forDebug) {
			this.startResouceRead (_forDebugFileName);
		} else {
			this.startResouceRead (sceneFileName);
		}

		_textMainFrame.GetComponent<Image> ().enabled = true;

		//メイン処理
		StartCoroutine ( loopText () );
	}

	public void startResouceRead(string argsFileName){
		string fPath = "ScenarioTxt/";
		string fFullPath = fPath + argsFileName;

		TextAsset txtA = Resources.Load(fFullPath) as TextAsset;

		string tmpAll = txtA.text;
		//@pg コードで分割する
		this.allTxt = tmpAll.Split(new string[]{"@pg\n"}, System.StringSplitOptions.None);

		this.pageStart ();
		tapedFlag = false;

	}

	public void onTap_goNextPage(){
		tapedFlag = true;
		sMB.playOneShotSound (enm_oneShotSound.massegeNext);
	}

	IEnumerator loopText(){

		while (true) {

			switch(thisStatus){
			case enm_textControllStatus.fadein:
				if (tapedFlag){
					txtColorAlpha = 1f;
					this.setTextAlpha ();
					
					thisStatus = enm_textControllStatus.inputWait;
					//fazerボタンの使用可能に
					_fazerIconSR.enabled = true;

					tapedFlag=false;
				}else{
					this.setTextAlpha ();

					if (txtColorAlpha >= 1f){
						thisStatus = enm_textControllStatus.inputWait;
						//fazerボタンの使用可能に
						_fazerIconSR.enabled = true;
					}
				}
				break;
			case enm_textControllStatus.inputWait:
				if (tapedFlag){
					_fazerIconSR.enabled = false;

					tapedFlag=false;
					this.pageNext();

				}else{
					//クリックされるまで何もしない
				}

				break;
			case enm_textControllStatus.sleep:
				//ウィンドウの非表示
				this.setChildImageEnable(false);

				if (tapedFlag){
					tapedFlag=false;
				}else{
					if (DateTime.Now > endWaitTime){
						//時間待機
						setChildImageEnable(true);

						this.pageNext();
					}
				}

				break;
			}

			yield return null; //new WaitForSeconds(0.05f);
		}
	}

	private void setChildImageEnable(bool argsBool){
		_textMainFrame.GetComponent<Image> ().enabled = argsBool;

		Image[] childImages = _textMainFrame.GetComponentsInChildren<Image> ();

		foreach (Image tmpI in childImages) {
			tmpI.enabled = argsBool;
				}
		}

	
	private void pageStart(){
		//最初だけ呼ばれる
		//以降は、pageNextが呼ばれる
		thisStatus = enm_textControllStatus.fadein;	//初期値
		thisUIText.text = "";	//初期化
		
		this.setTextAlpha (true);
		
		this.nowPage = 0;
		//テキスト用コマンドの実行
		this.execTextCommond ();

		thisUIText.text = allTxt[nowPage];


		//クリック待ちアイコンは非表示にする
		_fazerIconSR.enabled = false;
	}

	private void pageNext(){
		thisStatus = enm_textControllStatus.fadein;
		thisUIText.text = "";

		this.setTextAlpha (true);
		this.clearSpot ();	//ページ移行時には必ずクリアする

		this.nowPage += 1;

		if (this.nowPage >= allTxt.Length ) {
			//次が無い場合
			Debug.Log("todo : text end");
			this.finishTalking();
			return;
		}

		//テキスト用コマンドの実行
		this.execTextCommond ();

		thisUIText.text = allTxt[nowPage];
	}

	private void finishTalking(){
		//親ごと破壊
		Time.timeScale = 1f;
		//this.transform.parent.gameObject.SetActive (false);

		Destroy (_groundParent) ;

	}

	private void execTextCommond(){
		//allTxt[nowPage]
		bool findF = false;
		string matchStr = "";
		string tmpFindStr = "";
		
		//絵の消去
		tmpFindStr = "<removeall>";
		findF = Regex.IsMatch (allTxt [nowPage], tmpFindStr);
		if (findF) {
			//match の取得
			matchStr = Regex.Match(allTxt [nowPage], tmpFindStr).Value;
			
			this.setADVImageRemoveAll();		//Commond
			
			allTxt [nowPage] = Regex.Replace (allTxt [nowPage], tmpFindStr, "");
		}
		
		//絵の消去
		tmpFindStr = "<remove:[lrc]>";
		findF = Regex.IsMatch (allTxt [nowPage], tmpFindStr);
		if (findF) {
			//match の取得
			matchStr = Regex.Match(allTxt [nowPage], tmpFindStr).Value;
			
			this.setADVImageRemove(matchStr);		//Commond
			
			allTxt [nowPage] = Regex.Replace (allTxt [nowPage], tmpFindStr, "");
		}

		/*
		 * イベント操作系は、最初に処理しておくこと
		*/
		
		//イベントフラグ
		tmpFindStr = "<setEventFlag:[.]+>";
		findF = Regex.IsMatch (allTxt [nowPage], tmpFindStr);
		if (findF) {
			//match の取得
			matchStr = Regex.Match(allTxt [nowPage], tmpFindStr).Value;
			
			//Commond
			Debug.Log(matchStr);
			// todo
			
			allTxt [nowPage] = Regex.Replace (allTxt [nowPage], tmpFindStr, "");
		}

		//進捗増加
		tmpFindStr = "<forwardEvent:[abcd]>";
		findF = Regex.IsMatch (allTxt [nowPage], tmpFindStr);
		if (findF) {
			//match の取得
			matchStr = Regex.Match(allTxt [nowPage], tmpFindStr).Value;

			//Commond 頭文字は被る為、abcdとした
			// a : route
			// b : progress
			// c : stage
			// d : step
			// a以外の進行度が増加した時、必ず下位の進行度は0にする


			Debug.Log("matchStr : " + matchStr);
			// todo
			this.forwardStroyProgresses(matchStr);

			allTxt [nowPage] = Regex.Replace (allTxt [nowPage], tmpFindStr, "");
		}

		//進捗増加
		tmpFindStr = "<jampToEvent:[*]>";
		findF = Regex.IsMatch (allTxt [nowPage], tmpFindStr);
		if (findF) {
			//match の取得
			matchStr = Regex.Match(allTxt [nowPage], tmpFindStr).Value;

			Debug.Log("matchStr : " + matchStr);
			// todo
			this.setStroyProgresses(matchStr);

			allTxt [nowPage] = Regex.Replace (allTxt [nowPage], tmpFindStr, "");
		}
			


		//bgimage
		tmpFindStr = "<bgimage:[0-9]{2}>";
		findF = Regex.IsMatch (allTxt [nowPage], tmpFindStr);
		if (findF) {
			//match の取得
			matchStr = Regex.Match(allTxt [nowPage], tmpFindStr).Value;
			
			Debug.Log(matchStr);
			this.setBGImage(matchStr);			//Commond
			
			allTxt [nowPage] = Regex.Replace (allTxt [nowPage], tmpFindStr, "");
		}
		
		//image
		tmpFindStr = "<image:[0-9]{4}:[lrc]>";
		findF = Regex.IsMatch (allTxt [nowPage], tmpFindStr);
		if (findF) {
			//match の取得
			matchStr = Regex.Match(allTxt [nowPage], tmpFindStr).Value;
			
			Debug.Log(matchStr);
			this.setADVImage(matchStr);			//Commond
			
			allTxt [nowPage] = Regex.Replace (allTxt [nowPage], tmpFindStr, "");
		}
		
		//bgm
		tmpFindStr = "<bgm:[0-9]{2}>";
		findF = Regex.IsMatch (allTxt [nowPage], tmpFindStr);
		if (findF) {
			//match の取得
			matchStr = Regex.Match(allTxt [nowPage], tmpFindStr).Value;
			
			Debug.Log("matchStr : " + matchStr);
			this.setBGM(matchStr);			//Commond
			
			allTxt [nowPage] = Regex.Replace (allTxt [nowPage], tmpFindStr, "");
		}

		//se
		tmpFindStr = "<se:[0-9]{2}>";
		findF = Regex.IsMatch (allTxt [nowPage], tmpFindStr);
		if (findF) {
			//match の取得
			matchStr = Regex.Match(allTxt [nowPage], tmpFindStr).Value;

			Debug.Log("matchStr : " + matchStr);
			this.setSE(matchStr);			//Commond

			allTxt [nowPage] = Regex.Replace (allTxt [nowPage], tmpFindStr, "");
		}

		//se2
		tmpFindStr = "<se2:.*>";
		findF = Regex.IsMatch (allTxt [nowPage], tmpFindStr);
		if (findF) {
			//match の取得
			matchStr = Regex.Match(allTxt [nowPage], tmpFindStr).Value;

			Debug.Log("matchStr : " + matchStr);
			this.setSE2(matchStr);			//Commond

			allTxt [nowPage] = Regex.Replace (allTxt [nowPage], tmpFindStr, "");
		}

		//フィールドeffect
		tmpFindStr = "<eff:[0-9]{2}>";
		findF = Regex.IsMatch (allTxt [nowPage], tmpFindStr);
		if (findF) {
			//match の取得
			matchStr = Regex.Match(allTxt [nowPage], tmpFindStr).Value;

			//Commond
			Debug.Log(matchStr);
			this.setVisualEffect(matchStr);

			allTxt [nowPage] = Regex.Replace (allTxt [nowPage], tmpFindStr, "");
		}

		//表情effect
		tmpFindStr = "<emoeff:[0-9]{2}:[lrc]>";
		findF = Regex.IsMatch (allTxt [nowPage], tmpFindStr);
		if (findF) {
			//match の取得
			matchStr = Regex.Match(allTxt [nowPage], tmpFindStr).Value;

			//Commond
			Debug.Log(matchStr);
			this.setEmotionEffect(matchStr);

			allTxt [nowPage] = Regex.Replace (allTxt [nowPage], tmpFindStr, "");
		}


		
		//立ち絵の明るさを設定する
		//指定絵以外は暗くする
		tmpFindStr = "<spot:[lrc]{1}>";
		findF = Regex.IsMatch (allTxt [nowPage], tmpFindStr);
		if (findF) {
			//match の取得
			matchStr = Regex.Match(allTxt [nowPage], tmpFindStr).Value;
			
			//Commond
			Debug.Log("Sinario Command : " + matchStr);
			// todo
			this.setSpot(matchStr);

			allTxt [nowPage] = Regex.Replace (allTxt [nowPage], tmpFindStr, "");
		}

		
		//sleep time
		tmpFindStr = "<sleep:[0-9]{1,5}>";
		findF = Regex.IsMatch (allTxt [nowPage], tmpFindStr);
		if (findF) {
			//match の取得
			matchStr = Regex.Match(allTxt [nowPage], tmpFindStr).Value;
			
			this.setSleep(matchStr);
			
			allTxt [nowPage] = Regex.Replace (allTxt [nowPage], tmpFindStr, "");
		}

		
		//Scene移動
		tmpFindStr = "<nextScene:[tbs]{1}:.+>";
		findF = Regex.IsMatch (allTxt [nowPage], tmpFindStr);
		if (findF) {
			//match の取得
			matchStr = Regex.Match(allTxt [nowPage], tmpFindStr).Value;
			
			//Commond
			Debug.Log(matchStr);

			//t:トーク
			//b:バトル
			//s:ステージセレクト
			this.setNextScene(matchStr);

			// todo
			
			allTxt [nowPage] = Regex.Replace (allTxt [nowPage], tmpFindStr, "");
		}

	}


	
	//
	//
	private void forwardStroyProgresses(string argsStr){
		// <forwardEvent:[abcd]>
		string tagMain = argsStr.Substring (1, (argsStr.Length-2));		//カッコの消去
		string[] spritStr = tagMain.Split(new string[]{":"}, System.StringSplitOptions.None);	//sprit
		
		string argsPosi = spritStr[1];

		//a以外の進行度が増加した時、必ず下位の進行度は0にする

		switch(argsPosi){
		case "a":
			sVMS.addStoryProgresses(enum_StoryProgressType.Route);
			break;
		case "b":
			sVMS.addStoryProgresses(enum_StoryProgressType.Progress);
			break;
		case "c":
			sVMS.addStoryProgresses(enum_StoryProgressType.Stage);
			break;
		case "d":
			sVMS.addStoryProgresses(enum_StoryProgressType.Step);
			break;
		}
	}


	private void setStroyProgresses(string argsStr){
		// <forwardEvent:[abcd]>
		string tagMain = argsStr.Substring (1, (argsStr.Length-2));		//カッコの消去
		string[] spritStr = tagMain.Split(new string[]{":"}, System.StringSplitOptions.None);	//sprit

		string argsPosi = spritStr[1];

		//直接文字列指定
		//進行度をジャンプする
		Debug.Log("jump Sroty tag :" + argsPosi);

		sVMS.setStoryProgress(argsPosi);
	}
	
	//
	//
	private void setSpot(string argsStr){
		string tagMain = argsStr.Substring (1, (argsStr.Length-2));		//各個の消去
		string[] spritStr = tagMain.Split(new string[]{":"}, System.StringSplitOptions.None);	//sprit

		string argsPosi = spritStr[1];

		standingCharaImageParent sSCIS_L = _attachedLeftChara.GetComponent<standingCharaImageParent>();
		standingCharaImageParent sSCIS_R = _attachedRightChara.GetComponent<standingCharaImageParent>();
		standingCharaImageParent sSCIS_C = _attachedCenterChara.GetComponent<standingCharaImageParent>();

		//自分以外のイラストは暗くする

		switch (argsPosi) {
		case "l":
			sSCIS_C.setThisChildsSpot(false);
			sSCIS_L.setThisChildsSpot(true);
			sSCIS_R.setThisChildsSpot(false);

			break;
		case "r":
			sSCIS_C.setThisChildsSpot(false);
			sSCIS_L.setThisChildsSpot(false);
			sSCIS_R.setThisChildsSpot(true);

			break;
		case "c":
			sSCIS_C.setThisChildsSpot(true);
			sSCIS_L.setThisChildsSpot(false);
			sSCIS_R.setThisChildsSpot(false);

			break;
		}
	}

	/// <summary>
	/// Clears the spot.
	/// </summary>
	private void clearSpot(){

		standingCharaImageParent sSCIS_L = _attachedLeftChara.GetComponent<standingCharaImageParent>();
		standingCharaImageParent sSCIS_R = _attachedRightChara.GetComponent<standingCharaImageParent>();
		standingCharaImageParent sSCIS_C = _attachedCenterChara.GetComponent<standingCharaImageParent>();

		//すべて暗くする
		sSCIS_C.setThisChildsSpot(false);
		sSCIS_L.setThisChildsSpot(false);
		sSCIS_R.setThisChildsSpot(false);

	}
	//
	//
	private void setBGM(string argsStr){
		string tagMain = argsStr.Substring (1, (argsStr.Length-2));		//各個の消去
		string[] spritStr = tagMain.Split(new string[]{":"}, System.StringSplitOptions.None);	//sprit
		
		int tmpInt = int.Parse(spritStr[1]);
		
		soundManagerGetter.getManager ().playBGM (tmpInt);
	}
	
	private void setSE(string argsStr){
		string tagMain = argsStr.Substring (1, (argsStr.Length-2));		//各個の消去
		string[] spritStr = tagMain.Split(new string[]{":"}, System.StringSplitOptions.None);	//sprit
		
		int tmpInt = int.Parse(spritStr[1]);


		enm_oneShotSound tmpS = enm_oneShotSound.charaMenu;

		switch(tmpInt){
		case 0:
			tmpS = enm_oneShotSound.effect001;
			break;
		case 1:
			tmpS = enm_oneShotSound.attack03;
			break;
		case 2:
			tmpS = enm_oneShotSound.scream_of_a_monster_C1;
			break;
		case 3:
			tmpS = enm_oneShotSound.knocking_a_wooden_door;
			break;
		case 4:
			tmpS = enm_oneShotSound.removeCloud;
			break;
		case 5:
			tmpS = enm_oneShotSound.duck;
			break;
		case 6:
			tmpS = enm_oneShotSound.tinnitus;
			break;
		case 7:
			tmpS = enm_oneShotSound.horror;
			break;
		}

		soundManagerGetter.getManager().playOneShotSound (tmpS);
	}

	private void setSE2(string argsStr){
		string tagMain = argsStr.Substring (1, (argsStr.Length-2));		//各個の消去
		string[] spritStr = tagMain.Split(new string[]{":"}, System.StringSplitOptions.None);	//sprit

		string tmpName = spritStr[1];

		soundManagerGetter.getManager().playOneShotSound (tmpName);
	}

	//
	//
	private void setSleep(string argsStr){
		//"<nextScene:[tbs]{1}:[.]+>";
		string tagMain = argsStr.Substring (1, (argsStr.Length-2));		//各個の消去
		string[] spritStr = tagMain.Split(new string[]{":"}, System.StringSplitOptions.None);	//sprit
		
		double argsSleepTime = double.Parse(spritStr[1]);
		
		thisStatus = enm_textControllStatus.sleep;
		
		this.endWaitTime = DateTime.Now.AddMilliseconds(argsSleepTime);
	}




	//
	//
	private void setNextScene(string argsStr){
		//"<nextScene:[tbs]{1}:[.]+>";
		string tagMain = argsStr.Substring (1, (argsStr.Length-2));		//各個の消去
		string[] spritStr = tagMain.Split(new string[]{":"}, System.StringSplitOptions.None);	//sprit
		
		string argsNextSceneType = spritStr[1];
		string argsOption = spritStr[2];

		//Debug.Log (argsNextSceneType + argsOption);

		switch (argsNextSceneType) {
		case "t":
			//トークScene
			//トークシーンからトークシーンに移動するため、第2引数は特に不要
			//自動的にステップ１追加

			sVMS.addStoryProgresses(enum_StoryProgressType.Step);

			sVMS.changeScene(sceneChangeStatusEnum.gotoTalkScene);
			
			break;

		case "b":
			//バトル
			//会話パートからは、シーン名直接指定
			//sceneChangeValue tmpBtlVal = sVMS.getNowSceneChangeValue();
			//tmpBtlVal.sceneFileName = argsOption;
			
			sVMS.changeScene(sceneChangeStatusEnum.gotoBattle);

			break;
		case "s":
			//ステージセレクト
			sVMS.changeScene(sceneChangeStatusEnum.gotoStageSelect);

			break;
		}
	}


	//setVisualEffect
	private void setVisualEffect(string argsStr){
		string tagMain = argsStr.Substring (1, (argsStr.Length-2));		//各個の消去
		string[] spritStr = tagMain.Split(new string[]{":"}, System.StringSplitOptions.None);	//sprit

		string tmpNo = spritStr[1];

		string fFullPath = "";
		GameObject tmpResouce = null;
		GameObject tmpGO = null;

		switch (tmpNo) {
		case "00":
			fFullPath = "Prefabs/talkingParts/visualEff/visualEff_FadeInOut";
			tmpResouce = Resources.Load (fFullPath) as GameObject;
			tmpGO = (GameObject)Instantiate (tmpResouce);

			tmpGO.transform.parent = _groundParent.transform;
			tmpGO.transform.position = Vector3.zero;
			tmpGO.transform.localScale = Vector3.one;
			tmpGO.transform.SetAsLastSibling ();

			break;

		case "01":
			fFullPath = "Prefabs/talkingParts/visualEff/visualEff_Flash";
			tmpResouce = Resources.Load (fFullPath) as GameObject;
			tmpGO = (GameObject)Instantiate (tmpResouce);

			tmpGO.transform.parent = _groundParent.transform;
			tmpGO.transform.position = Vector3.zero;
			tmpGO.transform.localScale = Vector3.one;
			tmpGO.transform.SetAsLastSibling ();

			break;
		}
	}

	//
	//表情アイコンエフェクト
	//
	private void setEmotionEffect(string argsStr){
		string tagMain = argsStr.Substring (1, (argsStr.Length-2));		//各個の消去
		string[] spritStr = tagMain.Split(new string[]{":"}, System.StringSplitOptions.None);	//sprit
		
//		int argsIconNo = int.Parse(spritStr[1]);
		string argsEffNo = spritStr[1];
		string argsPosi = spritStr[2];

		GameObject tmpBaseTrn = _attachedLeftChara.gameObject;

		switch (argsPosi) {
		case "l":
			tmpBaseTrn = _attachedLeftChara.gameObject;

			break;
		case "r":
			tmpBaseTrn = _attachedRightChara.gameObject;

			break;
		case "c":
			tmpBaseTrn = _attachedCenterChara.gameObject;
			
			break;
		}

		GameObject tmpGo = Instantiate(_partsContainerGO) as GameObject;
		talkingPartsContainersScript tPCS = tmpGo.GetComponent<talkingPartsContainersScript> ();

		returnTalkingParts retPrefabs = tPCS.getCharaEffect (argsEffNo);

		if (retPrefabs.backPrefas != null) {
			foreach (GameObject tmpGO in retPrefabs.backPrefas) {
				GameObject copyEff = Instantiate (tmpGO) as GameObject;

				copyEff.transform.SetParent(tmpBaseTrn.transform);
				copyEff.transform.localPosition = Vector3.zero;
				copyEff.transform.SetAsFirstSibling ();
			}
		}
		
		if (retPrefabs.frontPrefas != null) {
			foreach (GameObject tmpGO in retPrefabs.frontPrefas) {
				GameObject copyEff = Instantiate (tmpGO) as GameObject;

				copyEff.transform.SetParent (tmpBaseTrn.transform);
				copyEff.transform.localPosition = Vector3.zero;
				copyEff.transform.SetAsLastSibling ();
			}
		}

		Destroy (tmpGo);
	}


	private void setADVImageRemoveAll(){
			this.setADVImageRemoveSingle ("c");
			this.setADVImageRemoveSingle ("l");
			this.setADVImageRemoveSingle ("r");
		}

	private void setADVImageRemove(string argsStr){
		string tagMain = argsStr.Substring (1, (argsStr.Length-2));		//各個の消去
		string[] spritStr = tagMain.Split(new string[]{":"}, System.StringSplitOptions.None);	//sprit
		
		string argsIconNo = spritStr[1];

		this.setADVImageRemoveSingle (argsIconNo);
	}

	private void setADVImageRemoveSingle(string argsStr){
		switch(argsStr){
		case "c":
			standingCharaImageParent sSCIS_C = _attachedCenterChara.transform.GetComponent<standingCharaImageParent>();
			sSCIS_C.destoryChild();
			break;
		case "l":
			standingCharaImageParent sSCIS_L = _attachedLeftChara.transform.GetComponent<standingCharaImageParent>();
			sSCIS_L.destoryChild();
			break;
		case "r":
			standingCharaImageParent sSCIS_R = _attachedRightChara.transform.GetComponent<standingCharaImageParent>();
			sSCIS_R.destoryChild();
			break;
		}
	}

	private void setBGImage(string argsStr){
		string tagMain = argsStr.Substring (1, (argsStr.Length-2));
		string[] spritStr = tagMain.Split(new string[]{":"}, System.StringSplitOptions.None);
		
		string argsImageNo = spritStr[1];

		//初期値
		_BGImage.color = Color.black;

		String fFullPath = "";
		Sprite[] charaBase;

		switch (argsImageNo) {
		case "97":
			//TODO: 星空の背景画像にする
			//blinkObj、完全透明（skybox:星）
			_BGImage.sprite = largeBGImageLoader.getImage("99");
			_BGImage.color = new Color(1f, 1f, 1f, 0f);
			_bgColor.color = new Color(1f, 1f, 1f, 0f);

			break;
		case "98":
			//半透明 黒
			_BGImage.sprite = null;
			_BGImage.color = new Color(0f, 0f, 0f, 0.3f);

			break;
		case "99":
			//黒塗り
			_BGImage.sprite = null;
			_BGImage.color = Color.black;
			
			break;
		default:
			_BGImage.sprite = largeBGImageLoader.getImage(argsImageNo);
			
			break;
				}

		StartCoroutine ( backGrougImageShow() );

		}
	
	IEnumerator backGrougImageShow(){
		_BGImage.color = Color.black;

		for (int loopI = 0; loopI < 60; loopI++) {
			yield return null;

			Color tmpC = new Color(loopI/60f, loopI/60f, loopI/60f ,1 );
			
			_BGImage.color = tmpC;
		} 
	}


	private void setADVImage(string argsStr){
		string tagMain = argsStr.Substring (1, (argsStr.Length-2));
		string[] spritStr = tagMain.Split(new string[]{":"}, System.StringSplitOptions.None);

		string argsImageNo = spritStr[1];
		string argsPosi = spritStr[2];


		standingCharaImageParent sSCIS_L = _attachedLeftChara.GetComponent<standingCharaImageParent>();
		standingCharaImageParent sSCIS_R = _attachedRightChara.GetComponent<standingCharaImageParent>();
		standingCharaImageParent sSCIS_C = _attachedCenterChara.GetComponent<standingCharaImageParent>();


		switch (argsPosi) {
		case "l":
			sSCIS_C.setThisChildsSpot(false);
			sSCIS_L.initCharactorImage(argsImageNo);

			//sSCIS_L.setThisChildsSpot(false);
			sSCIS_R.setThisChildsSpot(false);

			//tmpShadow.transform.parent = _attachedLeftChara.transform.parent;
			break;
		case "r":
			sSCIS_C.setThisChildsSpot(false);
			sSCIS_R.initCharactorImage(argsImageNo);
			
			sSCIS_L.setThisChildsSpot(false);
			//sSCIS_R.setThisChildsSpot(false);

			break;
		case "c":
			sSCIS_C.initCharactorImage(argsImageNo);
			
			sSCIS_L.setThisChildsSpot(false);
			sSCIS_R.setThisChildsSpot(false);
			//sSCIS_C.setThisChildsSpot(false);

			break;
		default:
			Debug.Log ("キャラの表示位置の指定が間違っています！ l,r,cの何れかである事を確認してください。");
			break;
				}
		}

	/*
	private void pageEnd(){
		thisStatus = enm_textControllStatus.fadeStart;
		
		txtColorAlpha = 0f;
		Color tmpColr = new Color (1, 1, 1, txtColorAlpha);
		thisUIText.color = tmpColr;

		this.nowPage += 1f;
	}
	*/

	private void setTextAlpha(){
		this.setTextAlpha (false);
	}
	private void setTextAlpha(bool restertFlag){
		
		txtColorAlpha += fadeSpeed;
		if (restertFlag) {
			txtColorAlpha = 0f;
				}

		Color tmpColr = new Color (0, 0, 0, txtColorAlpha);
		thisUIText.color = tmpColr;
	}
}

public enum enm_textControllStatus{
	inputWait
	,fadein
	,sleep
}

