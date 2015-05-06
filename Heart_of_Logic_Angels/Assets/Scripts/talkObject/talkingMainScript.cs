using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
//Regex
using System.Text.RegularExpressions;

public class talkingMainScript : MonoBehaviour {
	private string[] allTxt;
	private int nowPage;
	private enm_textControllStatus thisStatus;
	private Boolean tapedFlag;

	private Text thisUIText;
	private SpriteRenderer fazerIconSR;
	private float txtColorAlpha;
	private float fadeSpeed = 0.02f;



	// Use this for initialization
	void Start () {
		thisUIText = this.transform.GetComponent<Text> ();

		//waitIconFazer
		fazerIconSR = this.transform.parent.Find ("waitIconFazer").GetComponent<SpriteRenderer>();

		Time.timeScale = 0;
		this.startResouceRead ("1-1-1");

		StartCoroutine ( loopText () );
	}

	public void startResouceRead(string argsFileName){
		string fPath = "ScenarioTxt/";
		string fFullPath = fPath + argsFileName;
		//Debug.Log (fFullPath);

		TextAsset txtA = Resources.Load(fFullPath) as TextAsset;

		string tmpAll = txtA.text;
		//@pg コードで分割する
		this.allTxt = tmpAll.Split(new string[]{"@pg\n"}, System.StringSplitOptions.None);

		this.pageStart ();
		tapedFlag = false;

		//Debug.Log (allTxt.Length);

	}

	public void onTap_goNextPage(){
		Debug.Log ("tap");

		tapedFlag = true;

	}

	IEnumerator loopText(){
		DateTime endWaitTime = DateTime.Now;

		while (true) {
			yield return null; //new WaitForSeconds(0.05f);

			switch(thisStatus){
			case enm_textControllStatus.fadein:
				if (tapedFlag){
					txtColorAlpha = 1f;
					this.setTextAlpha ();
					
					thisStatus = enm_textControllStatus.inputWait;
					//fazerボタンの使用可能に
					fazerIconSR.enabled = true;

					tapedFlag=false;
				}else{
					this.setTextAlpha ();

					if (txtColorAlpha >= 1f){
						thisStatus = enm_textControllStatus.inputWait;
						//fazerボタンの使用可能に
						fazerIconSR.enabled = true;
					}
				}
				break;
			case enm_textControllStatus.inputWait:
				if (tapedFlag){
					fazerIconSR.enabled = false;

					tapedFlag=false;
					this.pageNext();

				}else{
					//クリックされるまで何もしない
				}

				break;
			case enm_textControllStatus.sleep:
				if (tapedFlag){
					tapedFlag=false;
				}else{
					if (DateTime.Now > endWaitTime){
						//時間待機

						// todo next page
						this.pageNext();
					}
				}

				break;
			}

		}
	}
	
	private void pageStart(){
		//最初だけ呼ばれる
		thisStatus = enm_textControllStatus.fadein;	//初期値
		thisUIText.text = "";	//初期化
		
		this.setTextAlpha (true);
		
		//テキスト用コマンドの実行
		this.execTextCommond ();

		this.nowPage = 0;
		thisUIText.text = allTxt[nowPage];


		//クリック待ちアイコンは非表示にする
		fazerIconSR.enabled = false;
	}

	private void pageNext(){
		thisStatus = enm_textControllStatus.fadein;
		thisUIText.text = "";

		this.setTextAlpha (true);
		
		//テキスト用コマンドの実行
		this.execTextCommond ();

		this.nowPage += 1;

		if (this.nowPage >= allTxt.Length ) {
			//次が無い場合
			Debug.Log("todo : text end");
		} else {
			thisUIText.text = allTxt[nowPage];
		}
	}

	private void execTextCommond(){
		//allTxt[nowPage]
		bool findF = false;
		string matchStr = "";
		string tmpFindStr = "";

		//image
		tmpFindStr = "<image:[0-9]{4}:[lrc]>";
		findF = Regex.IsMatch (allTxt [nowPage], tmpFindStr);
		if (findF) {
			//match の取得
			matchStr = Regex.Match(allTxt [nowPage], tmpFindStr).Value;

			//Debug.Log(matchStr);
			this.setADVImage(matchStr);

			allTxt [nowPage] = Regex.Replace (allTxt [nowPage], tmpFindStr, "");
		}

		//表情effect
		tmpFindStr = "<eff:[0-9]{2}>";
		findF = Regex.IsMatch (allTxt [nowPage], tmpFindStr);
		if (findF) {
			//match の取得
			matchStr = Regex.Match(allTxt [nowPage], tmpFindStr).Value;
			
			allTxt [nowPage] = Regex.Replace (allTxt [nowPage], tmpFindStr, "");
		}

		
		//絵の消去
		tmpFindStr = "<remove>";
		findF = Regex.IsMatch (allTxt [nowPage], tmpFindStr);
		if (findF) {
			//match の取得
			matchStr = Regex.Match(allTxt [nowPage], tmpFindStr).Value;
			
			allTxt [nowPage] = Regex.Replace (allTxt [nowPage], tmpFindStr, "");
		}

		//sleep time
		tmpFindStr = "<sleep:[0-9]{2}>";
		findF = Regex.IsMatch (allTxt [nowPage], tmpFindStr);
		if (findF) {
			//match の取得
			matchStr = Regex.Match(allTxt [nowPage], tmpFindStr).Value;
			
			allTxt [nowPage] = Regex.Replace (allTxt [nowPage], tmpFindStr, "");
		}



	}

	private void setADVImage(string argsStr){
		string tagMain = argsStr.Substring (1, (argsStr.Length-2));
		string[] spritStr = tagMain.Split(new string[]{":"}, System.StringSplitOptions.None);

		string argsImageNo = spritStr[1];
		string argsPosi = spritStr[2];
		
		//Debug.Log (argsImageNo);
		//Debug.Log (argsPosi);

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


