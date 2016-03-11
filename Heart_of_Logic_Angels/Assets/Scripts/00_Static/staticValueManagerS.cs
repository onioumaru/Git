using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

using System.Collections.Generic;
using MiniJSON;

public class staticValueManagerS : MonoBehaviour {
	public bool _debugDataReset = false;
	//test中に付きpublic		todo
	public Dictionary<string, object> saveDataJSON01;
	public Dictionary<string, object> saveDataJSON02;
	public Dictionary<string, object> saveDataJSON03;

	//選択されたセーブデータ
	//外部からは基本この情報を見る
	//そのため、デバグのうちは代用の値を入れる必要がある
	private Dictionary<string, object> selectedUserSaveDat;
	private int selectedSaveDataNumber;
	
	//シーンチェンジに関するステータス
	private sceneChangeValue nowSceneValue;

	//PlayerPrefs key
	private string jsonSaveKey01 = "mainSaveDataJSON01";
	private string jsonSaveKey02 = "mainSaveDataJSON02";
	private string jsonSaveKey03 = "mainSaveDataJSON03";
	
	// Dictionary Key
	private string saveCharaValue_Key = "saveCharaValues_";
	private string storyProgress_Key = "StoryProgress";
	private string storyStage_Key ="StoryStage";

	//sortie info
	private bool[] sortieCharaNo;



	//
	void Awake(){
		Debug.Log("Awake");
		Debug.Log(SceneManager.GetActiveScene().name);


		Object.DontDestroyOnLoad (this);

		saveDataJSON01 = new Dictionary<string, object>();
		saveDataJSON02 = new Dictionary<string, object>();
		saveDataJSON03 = new Dictionary<string, object>();

		if (_debugDataReset) {
			PlayerPrefs.DeleteAll();
		}
		
		this.initSaveData (0);
		this.initSaveData (1);
		this.initSaveData (2);
		
		//初期値
		//各シーンからこの値を参照し、適正な値が入っている場合は正規の手順を踏む
		//もしそれ以外だった場合、デバッグモードで起動する
		//参照は getNowSceneを使用する
		nowSceneValue = new sceneChangeValue();

		//Titleだった場合はデータ選択時にこの処理をしているので不要
		if (Application.loadedLevelName != "title") {
			//for debug
			selectedUserSaveDat = saveDataJSON01;
			selectedSaveDataNumber = 0;
			nowSceneValue.convertSelectedUserSaveData();
		}
	}

	
	//**//


	public void changeScene(sceneChangeStatusEnum argsS){
		//他の場所から呼ばれる

		sceneChangeValue tmpSCV = new sceneChangeValue ();
		tmpSCV.convertSelectedUserSaveData ();

		//時間は戻す
		Time.timeScale = 1f;

		switch(argsS){
		case sceneChangeStatusEnum.dataLoading:
			// argsVal は必要ない
			//セーブデータから確認する

			//chapter進捗を確認しどこに移動するか決める

			object tmpPgrs = selectedUserSaveDat[storyProgress_Key];

			switch(	int.Parse(tmpPgrs.ToString())){
			case 0:
				//now Game
				//Application.LoadLevel("talkScene");
				SceneManager.LoadScene ("talkScene");

				break;
			default:
				//基本ここ
				//ステージセレクトへ移動
				//Application.LoadLevel("stageSelect");
				SceneManager.LoadScene ("stageSelect");
				break;
			}

			break;
			
		case sceneChangeStatusEnum.gotoTitle:
			// argsVal は必要ない
			//Application.LoadLevel("title");
			SceneManager.LoadScene ("title");
			break;
			
		case sceneChangeStatusEnum.gotoTalkScene:
			//下記の関数がスタートする
			//talkScene - talkingMain - mainText - talkingMainScript - Start()
			//Application.LoadLevel("talkScene");
			SceneManager.LoadScene ("talkScene");
			
			break;
		case sceneChangeStatusEnum.gotoStageSelect:
			//Debug.Log ("stageSelect");
			//Application.LoadLevel("stageSelect");
			SceneManager.LoadScene ("stageSelect");
			
			break;
			
		case sceneChangeStatusEnum.gotoSortieSelect:
			//Debug.Log ("gotoSortieSelect");
			//Application.LoadLevel("selectSortieCharactersScene");
			SceneManager.LoadScene ("selectSortieCharactersScene");
			
			break;

		case sceneChangeStatusEnum.gotoBattle:
			//Debug.Log ("goto Battle : " + tmpSCV.sceneFileName);
			//Application.LoadLevel(tmpSCV.sceneFileName);
			SceneManager.LoadScene (tmpSCV.sceneFileName);
			break;
		}
	}


	public Dictionary<string, object> getUserSaveDat(){
		return selectedUserSaveDat;
		}

	
	public long getEventFlag(string argsFlagName){
		object tmpObj = 0;
		
		selectedUserSaveDat.TryGetValue (argsFlagName, out tmpObj);

		long retLng = (long)tmpObj;

		return retLng;
	}
	
	
	public void setEventFlag(string argsFlagName, long flagVal){
		try { 
			selectedUserSaveDat.Remove(argsFlagName);
		} catch (UnityException e) {
			Debug.Log (e);
		}
		
		selectedUserSaveDat.Add(argsFlagName, flagVal);
	}

	/// <summary>
	/// 指定した進捗階層を１加算する
	/// 第２引数で True を指定した際には、対象の進捗度を０にする。基本打に2引数は省略して使用する
	/// </summary>
	public void addStoryProgresses(enum_StoryProgressType argsStr){
		//
		this.addStoryProgresses (argsStr, false);
		}

	public void addStoryProgresses(enum_StoryProgressType argsStr, bool resetF){
		//下位の進行度は、自動的にリセットされる

		switch(argsStr){
		case enum_StoryProgressType.Route:
			// StoryRoute
			long tmpRoute = (long)selectedUserSaveDat["StoryRoute"];

			if (resetF){
				selectedUserSaveDat["StoryRoute"] = 0;
			}else{
				selectedUserSaveDat["StoryRoute"] = tmpRoute + 1;
			}
			break;
		case enum_StoryProgressType.Progress:
			//storyProgress_Key
			int  tmpProgress = int.Parse(selectedUserSaveDat[storyProgress_Key].ToString() );
			if (resetF){
				selectedUserSaveDat[storyProgress_Key] = 0;
			}else{
				selectedUserSaveDat[storyProgress_Key] = tmpProgress + 1;
				this.addStoryProgresses(enum_StoryProgressType.Stage,true);
				this.addStoryProgresses(enum_StoryProgressType.Step,true);
			}
			break;
		case enum_StoryProgressType.Stage:
			//storyStage_Key
			int tmpStage = int.Parse(selectedUserSaveDat[storyStage_Key].ToString() );

			if (resetF){
				selectedUserSaveDat[storyStage_Key] = 0;
			}else{
				selectedUserSaveDat[storyStage_Key] = tmpStage + 1; 
				this.addStoryProgresses(enum_StoryProgressType.Step,true);
			}
			break;
		case enum_StoryProgressType.Step:
			// StoryStep
			int tmpStep = int.Parse( selectedUserSaveDat["StoryStep"].ToString() );

			if (resetF){
				selectedUserSaveDat["StoryStep"] = 0;
			}else{
				selectedUserSaveDat["StoryStep"] = tmpStep + 1;
			}
			break;
		}

		nowSceneValue.convertSelectedUserSaveData ();

		Debug.Log ("進捗増加 : " + nowSceneValue.sceneFileName);
	}
	/// <summary>
	/// 直接文字列指定
	/// 00-00-00-00
	/// </summary>
	public void setStoryProgress(string argsStr){

		string[] tmpS = argsStr.Split('-');
		Debug.Log (tmpS);
		this.setStoryProgress( int.Parse(tmpS[0]) ,int.Parse(tmpS[1]) ,int.Parse(tmpS[2]) ,int.Parse(tmpS[3]));

	}
	/// <summary>
	/// 個別指定
	/// 数字を別個指定
	/// </summary>
	/// <param name="argsRoute">Arguments route.</param>
	/// <param name="argsProgress">Arguments progress.</param>
	/// <param name="argsStage">Arguments stage.</param>
	/// <param name="argsStep">Arguments step.</param>
	public void setStoryProgress(int argsRoute,int argsProgress ,int argsStage ,int argsStep){
		
		selectedUserSaveDat["StoryRoute"] = argsRoute;
		selectedUserSaveDat[storyProgress_Key] = argsProgress;
		selectedUserSaveDat[storyStage_Key] = argsStage;
		selectedUserSaveDat["StoryStep"] = argsStep;

		nowSceneValue.convertSelectedUserSaveData ();
	}

	//
	//
	//シーンチェンジに関する色々
	//
	//
	
	public void setSortieCharaNo(bool[] argsBool){
		sortieCharaNo = argsBool;
	}

	public bool[] getSortieCharaNo(){
		return sortieCharaNo;
	}




	public string getTalkingSceneName(){
		//init されていない場合は空白が返される
		return nowSceneValue.sceneFileName;
		}


	public int getStoryRoute(){
		//storyStage_Key
		object tmpVal = selectedUserSaveDat["StoryRoute"];

		return int.Parse( tmpVal.ToString());
	}

	public int getStoryStage(){
		//storyStage_Key
		object tmpVal = selectedUserSaveDat[storyStage_Key];

		return int.Parse( tmpVal.ToString());
	}

	public int getStoryProgress(){
		//現在進行してる進捗データを返す
		//int tmpStoryProgress = 0;

		//selectedSaveDat
		/*
		object tmpObj;

		selectedUserSaveDat.TryGetValue (storyProgress_Key, out tmpObj);

		long val = (long)tmpObj;

		/*
		if( val == 0) {
			selectedUserSaveDat.Add(storyProgress_Key, val);
		}
		 */

		object val = selectedUserSaveDat[storyProgress_Key];

		return int.Parse( val.ToString());
	}



	public int getStoryProgress_pinpoint(int argsIndx){
		//直接セーブデータ枠1-3の進捗を見るときに使う
		//基本的に使わない
		//ほぼセーブデータ一覧用
		long tmpStoryProgress = 0;
		Dictionary<string, object> tmpSelectedDic;

		switch (argsIndx) {
		case 0:
			tmpSelectedDic = saveDataJSON01;
			break;
		case 1:
			tmpSelectedDic = saveDataJSON02;
			break;
		default:
			tmpSelectedDic = saveDataJSON03;
			break;
		}

		return int.Parse(tmpSelectedDic[storyProgress_Key].ToString() );
	}

	/*
	 * 
	 * 
	 * 
	 */

	public bool getCharacterEnable(int argsIndex){
		//そのキャラが解放されているか

		string tmpKeyStr = saveCharaValue_Key + argsIndex.ToString ("0") + "_enable";
		return (bool)selectedUserSaveDat[tmpKeyStr];
	}

	public saveCharaValueClass getSaveChara_MaxLevel(int argsIndx){
		//直接セーブデータ枠1-3の最大レベルキャラを返す
		//基本的に使わない
		//ほぼセーブデータ一覧用
		saveCharaValueClass retCharaData;
		Dictionary<string, object> tmpSelectedDic;
		
		switch (argsIndx) {
		case 0:
			tmpSelectedDic = saveDataJSON01;
			break;
		case 1:
			tmpSelectedDic = saveDataJSON02;
			break;
		default:
			tmpSelectedDic = saveDataJSON03;
			break;
		}

		//Debug.Log (Json.Serialize(tmpSelectedDic));
		retCharaData = this.getSaveCharaValue(tmpSelectedDic,0);

		long tmpMaxLevel = retCharaData.level;

		for (int loopI = 0; loopI < 9; loopI++) {
			string tmpKey = saveCharaValue_Key + loopI.ToString ("0") + "_level";
			long tmpI = (long)tmpSelectedDic[tmpKey];

			
			string tmpEnableKey = saveCharaValue_Key + loopI.ToString ("0") + "_enable";
			bool tmpCharaBool = (bool)tmpSelectedDic[tmpEnableKey];

			if (tmpCharaBool == true){
				if (tmpMaxLevel < tmpI){
					//最大レベルの保持
					retCharaData = this.getSaveCharaValue(tmpSelectedDic,loopI);
					tmpMaxLevel = retCharaData.level;
				}
			}
		}

		return retCharaData;
	}


	public void setSelectedSaveDat(int argsIndex){
		//selectedSaveDataNumber にセーブデータを割り当てる
		//セーブデータの場所を決定して
		selectedSaveDataNumber = argsIndex;

		switch(argsIndex){
		case 0:
			selectedUserSaveDat = new Dictionary<string, object>(saveDataJSON01);
			break;
		case 1:
			selectedUserSaveDat =  new Dictionary<string, object>(saveDataJSON02);
			break;
		default:
			selectedUserSaveDat =  new Dictionary<string, object>(saveDataJSON03);
			break;
		}

		//現在のシーン情報を入れる
		//このクラス自身が selectedUserSaveDat から直接見る
		nowSceneValue.convertSelectedUserSaveData ();
	}

	private void initSaveData(int argsInt){
		//データ呼び出し
		//新規作成の場合は、キャラクターデータまで作成してしまう

		string playerPrefskeyString = "";

		switch(argsInt){
		case 0:
			playerPrefskeyString = jsonSaveKey01;
			break;
		case 1:
			playerPrefskeyString = jsonSaveKey02;
			break;
		default:
			playerPrefskeyString = jsonSaveKey03;
			break;
		}

		//Data Check
		string tmpStr = PlayerPrefs.GetString (playerPrefskeyString);

		//Debug.Log (tmpStr);

		if (tmpStr == "") {
			//作ってセーブ
			Dictionary<string, object> tmpDc = new Dictionary<string, object> ();
			tmpDc.Add ("thisSaveName", System.DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
			tmpDc.Add ("playCnt", 0);
			tmpDc.Add ("StoryRoute", 0);
			tmpDc.Add ("StoryProgress", 0);
			tmpDc.Add ("StoryStage", 0);
			tmpDc.Add ("StoryStep", 0);
			tmpDc.Add ("StoryMode", "nomal");

			//キャラの枠だけ作成
			for (int indexI = 0;indexI < 9; indexI++){
				saveCharaValueClass tmpVal = new saveCharaValueClass(indexI) ;
				
				this.setSaveCharaValue(tmpDc, tmpVal);
			}

			PlayerPrefs.SetString (playerPrefskeyString, Json.Serialize (tmpDc));
			//Debug.Log (Json.Serialize (tmpDc));
		}


		string tmpPP = PlayerPrefs.GetString (playerPrefskeyString);
		
		switch(argsInt){
		case 0:
			saveDataJSON01 = Json.Deserialize (tmpPP) as Dictionary<string, object>;
			break;
		case 1:
			saveDataJSON02 = Json.Deserialize (tmpPP) as Dictionary<string, object>;
			break;
		default:
			saveDataJSON03 = Json.Deserialize (tmpPP) as Dictionary<string, object>;
			break;
		}
	}

	public void createNewGameData(){
		//new Game
		//selectedUserSaveDat

		Dictionary<string, object> tmpDc = new Dictionary<string, object> ();
		tmpDc.Add ("thisSaveName", System.DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
		tmpDc.Add ("playCnt", 0);
		tmpDc.Add ("StoryRoute", 0);
		tmpDc.Add ("StoryProgress", 0);
		tmpDc.Add ("StoryStage", 0);
		tmpDc.Add ("StoryStep", 0);
		tmpDc.Add ("StoryMode", "nomal");

		//キャラの枠だけ作成
		for (int indexI = 0;indexI < 9; indexI++){
			saveCharaValueClass tmpVal = new saveCharaValueClass(indexI) ;

			this.setSaveCharaValue(tmpDc, tmpVal);
		}

		selectedUserSaveDat = tmpDc;
	}


/*
 * 		setJSON
 */

	public void saveJSONData(int argsInt){
		//PlayerPrefs.SetString (playerPrefskeyString, Json.Serialize (tmpDc));
		
		string playerPrefskeyString = "";
		
		//selectedUserSaveDat
		switch(argsInt){
		case 0:
			playerPrefskeyString = jsonSaveKey01;
			saveDataJSON01 = new Dictionary<string, object>(selectedUserSaveDat);
			break;
		case 1:
			playerPrefskeyString = jsonSaveKey02;
			saveDataJSON02 = new Dictionary<string, object>(selectedUserSaveDat);
			break;
		default:
			playerPrefskeyString = jsonSaveKey03;
			saveDataJSON03 = new Dictionary<string, object>(selectedUserSaveDat);
			break;
		}

		PlayerPrefs.SetString (playerPrefskeyString, Json.Serialize (selectedUserSaveDat));

	}


/*
 * 		getJSON
 */

	public string getJSONString(string argsKey){
		//string retStr = "";

		return (string)saveDataJSON01[argsKey];
	}
	
	public float getJSONfloat(string argsKey){
		//float retStr = 0f;
		
		return (float)saveDataJSON01[argsKey];
	}


	/// <summary>
	/// 省略形の場合、selectedUserSaveDataに保存する
	/// </summary>
	/// <param name="srgsCharaVal">Srgs chara value.</param>
	public void setSaveCharaValue(saveCharaValueClass argsCharaVal){
		setSaveCharaValue (selectedUserSaveDat, argsCharaVal);
	}
	/// <summary>
	/// 非省略形
	/// </summary>
	/// <param name="saveData">Save data.</param>
	/// <param name="srgsCharaVal">Srgs chara value.</param>
	public void setSaveCharaValue(Dictionary<string, object> saveData,saveCharaValueClass srgsCharaVal){
		//tmpSelectedDic.Add(saveCharaValue_Key + indexI.ToString("0"), tmpVal);

		string tmpKey = saveCharaValue_Key + srgsCharaVal.Number.ToString ("0");

		try{
			//該当キーは全て削除し入れなおし
			saveData.Remove(tmpKey + "_Number");
			saveData.Remove(tmpKey + "_enable");
			saveData.Remove(tmpKey + "_level");
			saveData.Remove(tmpKey + "_exp");
			saveData.Remove(tmpKey + "_equipment");
			saveData.Remove(tmpKey + "_battleLimiter");
			saveData.Remove(tmpKey + "_sanity");
		} catch (UnityException e){
				}

		saveData.Add (tmpKey + "_Number", srgsCharaVal.Number);
		saveData.Add (tmpKey + "_enable", srgsCharaVal.enable);
		saveData.Add (tmpKey + "_level", srgsCharaVal.level);
		saveData.Add (tmpKey + "_exp", srgsCharaVal.exp);
		saveData.Add (tmpKey + "_equipment", srgsCharaVal.equipment);
		saveData.Add (tmpKey + "_battleLimiter", srgsCharaVal.battleLimiter);
		saveData.Add (tmpKey + "_sanity", srgsCharaVal.sanity);

	}
	/// <summary>
	/// キャラの追加削除フラグ
	/// </summary>
	/// <param name="argsCharaNum">Arguments chara number.</param>
	/// <param name="argsFlag">If set to <c>true</c> arguments flag.</param>
	public void setSaveCharaEnableFlag(enumCharaNum argsCharaNum, bool argsFlag, enumCharactorJoinType argsExpType){
		int tmpL = (int)argsCharaNum;
		saveCharaValueClass beforeSCVC = this.getSaveCharaValue(tmpL);

		//フラグセット
		beforeSCVC.enable = argsFlag;

		switch (argsExpType) {
		case enumCharactorJoinType.dontTouchExp:
			//何もしない
			break;
		case enumCharactorJoinType.maxExp:
			
			float maxVal = 0;

			for (int loopI = 0; loopI < 9; loopI++) {
				saveCharaValueClass tmpC = this.getSaveCharaValue (loopI);

				Debug.Log (tmpC.exp);

				if (maxVal < tmpC.exp) {
					maxVal = tmpC.exp;
				}
			} 

			beforeSCVC.exp = maxVal;

			break;

		case enumCharactorJoinType.avarageExp:

			float Allexp = 0;

			for (int loopI = 0; loopI < 9; loopI++) {
				saveCharaValueClass tmpC = this.getSaveCharaValue (loopI);
				Allexp += tmpC.exp;
			} 

			beforeSCVC.exp = Allexp / 9f;

			break;

		case enumCharactorJoinType.sameEnju:
			int enjuNum = (int)enumCharaNum.enju_01;
			beforeSCVC.exp = this.getSaveCharaValue (enjuNum).exp * 0.9f;

			Debug.Log (beforeSCVC.exp);

			break;
		}

		//レベルを計算してセット
		beforeSCVC.level = characterLevelManagerGetter.getManager ().calcLv (beforeSCVC.exp).Lv;

		this.setSaveCharaValue (beforeSCVC);
	}


	public sceneChangeValue getNowSceneChangeValue(){
		return nowSceneValue;
		}
	
	public saveCharaValueClass getSaveCharaValue(int argsCharaNo){
		//overload
		return this.getSaveCharaValue (selectedUserSaveDat, argsCharaNo);
	}

	public saveCharaValueClass getSaveCharaValue(Dictionary<string, object> saveData, int argsCharaNo){
		saveCharaValueClass retDat = new saveCharaValueClass (argsCharaNo);
		string dicKey = "";
		//saveCharaValue_Key + argsCharaNo + "_Number";

		dicKey = saveCharaValue_Key + argsCharaNo + "_enable";
		//Debug.Log (dicKey);
		retDat.enable = (bool)saveData [dicKey];

		dicKey = saveCharaValue_Key + argsCharaNo + "_level";
		retDat.level = (long)saveData [dicKey];

		dicKey = saveCharaValue_Key + argsCharaNo + "_exp";
		retDat.exp = float.Parse( saveData [dicKey].ToString() );

		dicKey = saveCharaValue_Key + argsCharaNo + "_equipment";
		retDat.equipment = (long)saveData [dicKey];
		
		dicKey = saveCharaValue_Key + argsCharaNo + "_battleLimiter";
		retDat.battleLimiter = (long)saveData [dicKey];

		dicKey = saveCharaValue_Key + argsCharaNo + "_sanity";
		retDat.battleLimiter = (long)saveData [dicKey];

		return retDat;
	}

	public string getProgressString(int argsProgress){
		//セーブデータのみだしに使われる日本語名称
		//battleStageSelectVal はステージごとの名称で、別物
		string retStr = "not found";
		
		switch(argsProgress){
		case 0:
			retStr = "はじめから";
			break;
		case 1:
			retStr = "チュートリアル終了";
			break;
		case 2:
			retStr = "シュスラン合流後";
			break;
		}
		
		return retStr;
	}

	public bool getRenderingShadowFlag(){
		string tmpStr = PlayerPrefs.GetString ("Option_RenderShadow");
		if (tmpStr == "") {
			tmpStr = "False";
		}
		return System.Convert.ToBoolean (tmpStr);
	}

	public bool getRenderingStageEffFlag(){
		string tmpStr = PlayerPrefs.GetString ("Option_RenderStageEff");
		if (tmpStr == "") {
			tmpStr = "False";
		}
		return System.Convert.ToBoolean (tmpStr);
	}


}

/*
	static ManagerGetter
*/
public static class staticValueManagerGetter{
	private static staticValueManagerS thisMNG;

	public static staticValueManagerS getManager(){
		if (thisMNG == null) {

			//thisMNG = GameObject.Find("StaticSceneManager").GetComponent<staticValueManagerS>();
			thisMNG = GameObject.FindWithTag("StaticSceneManager").GetComponent<staticValueManagerS>();
		}

		return thisMNG;
	}
}


/*
	static charaSaveClass
*/
public class saveCharaValueClass{
	//そのままではminiJSONで保存できない為、
	//プロパティの増加時は、json保存用の関数も編集する事
	public long Number;
	public bool enable;
	public float exp;
	public long level;
	public long equipment;
	public long battleLimiter;	//リミット解除状態であるか
	public long sanity;			//正気度
	
	public saveCharaValueClass(int argsNum){
		Number = argsNum;
		enable = false;
		if (argsNum < 1) {enable = true;}
		exp = 0f;
		level = 1;
		equipment = 0;
		battleLimiter = 0;
		sanity = 0;
	}

	public string getCharaName(){
		string retS = "";
		switch(this.Number){
		case 0:
			retS = "エンジュ";
			break;
		case 1:
			retS = "シュスラン";
			break;
		case 2:
			retS = "スズシロ";
			break;
		case 3:
			retS = "アカネ";
			break;
		case 4:
			retS = "ホオズキ";
			break;
		case 5:
			retS = "モクレン";
			break;
		case 6:
			retS = "サクラ";
			break;
		case 7:
			retS = "シオン";
			break;
		case 8:
			retS = "ヒイラギ";
			break;
		}

		return retS;
	}

	public enumCharaNum getCharaEnum(){
		return (enumCharaNum)this.Number;
	}
}



public enum sceneChangeStatusEnum{
	gotoTitle,
	dataLoading,	//データロードの場合は、進行度により進むシーンが違う為、個別に判断する
	gotoSortieSelect,
	gotoStageSelect,
	gotoBattle,
	gotoTalkScene
}

public enum enum_StoryProgressType{
	Route,
	Progress,	//データロードの場合は、進行度により進むシーンが違う為、個別に判断する
	Stage,
	Step
}




public class sceneChangeValue{
	//トークシーンは開くシナリオファイルを直接指定
	public string sceneFileName = "";

	private staticValueManagerS sVMS;
	private Dictionary<string, object> userSaveData;


	public sceneChangeValue(){
		sVMS = staticValueManagerGetter.getManager ();

		//特になし。初期値
		this.sceneFileName = "0-0-0-0";
		}
	

	public void convertSelectedUserSaveData(){
		/*
	 * 		tmpDc.Add ("thisSaveName", playerPrefskeyString);
			tmpDc.Add ("playCnt", 1);
			tmpDc.Add ("StoryRoute", 1);
			tmpDc.Add ("StoryProgress", 1);
			tmpDc.Add ("StoryStage", 1);
			tmpDc.Add ("StoryStep", 1);
			tmpDc.Add ("StoryMode", "nomal");

			この値については、ストーリー管理txtを読む事
			データロード時に初期値を入れる
	 */
		userSaveData = sVMS.getUserSaveDat ();

		string tmpStr = userSaveData ["StoryRoute"].ToString();
		tmpStr += "-" + (string)userSaveData ["StoryProgress"].ToString();
		tmpStr += "-" + (string)userSaveData ["StoryStage"].ToString();
		tmpStr += "-" + (string)userSaveData ["StoryStep"].ToString();	//Stepはマルチステージでは0.ここで判断しない

		sceneFileName = tmpStr;

		//Debug.Log (tmpStr);
	}

	/*
	public void setStoryRoute(long argsNum){
		//Route 情報については、直接値をセットする
		userSaveData ["StoryRoute"] = argsNum;

		this.convertSelectedUserSaveData();
	}
	
	public void addStoryProgress(long argsAddCount){
		userSaveData["StoryProgress"] = (long)userSaveData["StoryProgress"] + argsAddCount;
		
		//Debug.Log (userSaveData["StoryProgress"].ToString() );
		this.convertSelectedUserSaveData();
	}
	
	public void addStoryStage(long argsAddCount){
		userSaveData["StoryStage"] =  (long)userSaveData["StoryStage"] + argsAddCount;
		
		//Debug.Log (userSaveData["StoryStage"].ToString() );

		this.convertSelectedUserSaveData();
	}
	
	public void addStoryStep(long argsAddCount){
		userSaveData["StoryStep"] =  (long)userSaveData["StoryStep"] + argsAddCount;

		//Debug.Log (userSaveData["StoryStep"].ToString() );
		this.convertSelectedUserSaveData();
	}
	*/
}




public enum enumCharactorJoinType{
	dontTouchExp,
	maxExp,
	avarageExp,
	sameEnju
}
