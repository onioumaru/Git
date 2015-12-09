using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManagerScript : MonoBehaviour {
	public bool _forDebug = false;

	//GUI上で設定
	public Sprite[] charaIconEmptyGrp;
	public SpriteRenderer[] charaIconEmptyPosision;

	public Sprite[] _charaMainSprite;
	public RuntimeAnimatorController[] _charaMainAnimetion;
	
	public Sprite[] _charaFaceIconSprite;

	public gameStartingVariable loadedCharaList;
	
	public GameObject _cmrTracker;
	private GameObject instantedCmrTracker;
	private int charaIconPage=1;

	public GameObject _talkPartPerefab;
	private staticValueManagerS sVMS;

	
	public GameObject _battleTextCanvasPrefab;
	private battleTextCanvasParentScript battleTextInstance;

	private HashSet<int> colliderIDs;

	// Use this for initialization
	void Start () {
		instantedCmrTracker = (GameObject)Instantiate (_cmrTracker);
		instantedCmrTracker.name = "CameraTracker";
		charaIconEmptyPosision = instantedCmrTracker.GetComponent<cameraTrackerScript> ().getemptyWaku ();

		sVMS = staticValueManagerGetter.getManager ();

		this.battleStageStarter();

	}
	/// <summary>
	/// 第2引数を省略すると、自動的にポーズ処理を行う
	/// 第二引数 False で、ポーズ処理は飛ばす。連続でイベント処理を粉う場合は False で呼ぶと便利
	/// </summary>
	/// <param name="argsStr">Arguments string.</param>

	public void talkingPartLoader(string argsStr){
		talkingPartLoader(argsStr, true);
	}
	
	public void talkingPartLoader(string argsStr,bool freezChenged){
		//コライダーはすべて停止

		if (freezChenged) {
			Time.timeScale = 0f;
			this.setAllCollider2DEnabale (false);
		}
		
		//会話表示
		sVMS.getNowSceneChangeValue().sceneFileName = argsStr;
		GameObject tmpGO = (GameObject)Instantiate (_talkPartPerefab);
		
		StartCoroutine (talkingPartWaiter(tmpGO, freezChenged) );
	}



	IEnumerator talkingPartWaiter(GameObject argsGO, bool freezChenged){
		// トークシーンが破壊されるまでループして待つ
		while (argsGO != null) {
			yield return null;
		}

		if (freezChenged) {
			this.setAllCollider2DEnabale (true);
			Time.timeScale = 1f;
		}
	}


	
	public void setAllCollider2DEnabale(bool argsBool){
		//全てのコライダーを使用不可にして、イベントが起きないようにする
		Collider2D[] allCollider = GameObject.FindObjectsOfType<Collider2D>();

		if (argsBool == false) {
			//Falseをセットする場合
			//元々Falseだったものは取っておくので初期化する
			colliderIDs = new HashSet<int>();
				}

		//Debug.Log (allCollider.Length);
		foreach (Collider2D cldr in allCollider) {
			if (argsBool){
				if (colliderIDs.Contains(cldr.GetInstanceID()) == false ){
					//元々FalseだったものはTrueに戻さない
					cldr.enabled = argsBool;
				}
			} else {
				if (cldr.enabled == false){
					//使用停止中の場合はリストにキープ
					colliderIDs.Add(cldr.GetInstanceID());
				}
				cldr.enabled = argsBool;
			}
		}
	}


	public void destoryChara(int argsCharaNoIndex){
		//削除フラグ ON
		gameStartingVariable_Single tmpChara = loadedCharaList.charalist [argsCharaNoIndex];
		tmpChara.charaDestoryF = true;

		Destroy (tmpChara.charaBase);
		Destroy (tmpChara.charaIconSet);
		Destroy (tmpChara.charaFlag);
	}
	

	void battleStageStarter( ){

		loadedCharaList = new gameStartingVariable ();

		//charaStartBattleInfo cSBI = new charaStartBattleInfo (sVMS.getNowSceneChangeValue ());
		//sVMS.getNowSceneChangeValue ().sceneFileName = "0-3-0-0";

		charaStartBattleInfo cSBI = new charaStartBattleInfo (sVMS.getNowSceneChangeValue ());

		bool[] sortieCharas;
		if (_forDebug) {
			//for debug
			sortieCharas = new bool[9];
			sortieCharas[0] = true;
			sortieCharas[1] = true;
			sortieCharas[2] = false;
			sortieCharas[3] = false;
			sortieCharas[4] = false;
			sortieCharas[5] = false;
			sortieCharas[6] = false;
			sortieCharas[7] = true;
			sortieCharas[8] = true;
		} else {
			sortieCharas = sVMS.getSortieCharaNo ();
		}


		for (int loopI = 0; loopI < 9; loopI++){
			enumCharaNum tmpC = (enumCharaNum)loopI;

			//すべてのキャラを作成
			if (sortieCharas[loopI]){
				loadedCharaList.setData (tmpC, 1, 0);
			}
		}

		//Prefabsからロード
		//loadedCharaList.setData (enumCharaNum.syusuran_02, 1, 0);
		//chataList.setData (enumCharaNum.suzusiro_03, 1, 0);
		//chataList.setData (enumCharaNum.gyokuran_04, 1, 0);

		for (int i=0 ; i < loadedCharaList.charalist.Count;i++){
			gameStartingVariable_Single tmpChara = loadedCharaList.charalist[i];

			//charaMain
			tmpChara.charaBase = Instantiate(tmpChara.Prefab_charaGrh) as GameObject;
			tmpChara.charaBase.transform.name = tmpChara.CharaNumber.ToString();

			//キャラクターの初期位置
			tmpChara.charaBase.transform.position = cSBI.getCharaPosition(i);

			//charaAnime
			Animator tmpAnime = tmpChara.charaBase.transform.FindChild("charaAnime").GetComponent<Animator>();
			tmpAnime.runtimeAnimatorController = this.getCharaAnimator(tmpChara.CharaNumber);

			//charaSprite
			SpriteRenderer tmpSprite = tmpChara.charaBase.transform.FindChild("charaAnime").GetComponent<SpriteRenderer>();
			tmpSprite.sprite = this.getCharaSprite(tmpChara.CharaNumber);

			tmpChara.charaScript = tmpChara.charaBase.GetComponentInParent<allCharaBase>();
			
			tmpChara.charaScript.thisChara = new charaUserStatus (tmpChara.CharaNumber, 1f);
			tmpChara.charaFlag = Instantiate(tmpChara.Prefab_charaFlag , cSBI.getFlagPosition(i) ,Quaternion.identity) as GameObject;
			
			tmpChara.charaScript.thisCharaIndex = i;
			tmpChara.charaScript.thisCharaFlag = tmpChara.charaFlag;

			tmpChara.charaScript.calcdExp = characterLevelManagerGetter.getManager().calcLv(tmpChara.totalExp);

			tmpChara.charaScript.stopFlag = false;
		}

		//キャラアイコンの1ページ目の作成
		this.createCharaIconSet(1);
	}


	private Sprite getCharaSprite(enumCharaNum charaNo){
		Sprite retSR = null;

		/*
		switch (charaNo) {
		case enumCharaNum.syusuran_02:
			retSR = _charaMainSprite[1];
			break;
		case enumCharaNum.enju_01:
		default:
			retSR = _charaMainSprite[0];
			break;
				}
		*/

		int tmpInt = (int)charaNo;
		retSR = _charaMainSprite[tmpInt];

		return retSR;
	}

	private RuntimeAnimatorController getCharaAnimator(enumCharaNum charaNo){
		RuntimeAnimatorController retRAC = null;

		/*
		switch (charaNo) {
		case enumCharaNum.syusuran_02:
			retRAC = _charaMainAnimetion[1];
			break;
		case enumCharaNum.enju_01:
		default:
			int tmpInt = (int)charaNo;

			Debug.Log (tmpInt);

			retRAC = _charaMainAnimetion[tmpInt];
			break;
		}
*/
		
		int tmpInt = (int)charaNo;
		//Debug.Log (tmpInt);
		
		retRAC = _charaMainAnimetion[tmpInt];

		return retRAC;
	}

	
	private Sprite getCharacterIconSprite(enumCharaNum argsCharaNo){
		Sprite retSR = null;

		/*
		switch (argsCharaNo) {
		case enumCharaNum.syusuran_02:
			retSR = _charaFaceIconSprite[1];
			break;
		case enumCharaNum.enju_01:
		default:
			retSR = _charaFaceIconSprite[0];
			break;
		}
		*/
		
		int tmpInt = (int)argsCharaNo;
		
		retSR = _charaFaceIconSprite[tmpInt];

		return retSR;
	}

	public void createCharaIconSet(int argsPage){
		//初期化処理
		for (int i=0; i < loadedCharaList.charalist.Count; i++) {
			if (loadedCharaList.charalist[i].charaIconSet != null){
				//既に存在している場合は消去
				loadedCharaList.charalist[i].charaIconScript.destoryMe();
			}
		}

		// create
		int indexOffset = 0;

		switch(argsPage){
		case 1:
			indexOffset = 0;
			break;
		case 2:
			indexOffset = 3;
			break;
		case 3:
			indexOffset = 6;
			break;
		default:
			break;
		}
		
		
		for (int i = 0; i < 3; i++) {
			int tgtIndex = i + indexOffset;
			//emptyWaku の数字のスクロールは先に行う
			charaIconEmptyPosision[i].sprite = charaIconEmptyGrp[tgtIndex];
		}
		
		for (int i = 0; i < 3; i++){
			int tgtIndex = i + indexOffset;

			if (tgtIndex > loadedCharaList.charalist.Count -1) {break;}

			gameStartingVariable_Single tmpChara = loadedCharaList.charalist[tgtIndex];

			if (tmpChara.charaDestoryF == false){
				//既に破壊されていないか確認

				//IconSetは、必要な時に呼び出す
				float tmpX = -0.7f + (i * 0.7f);
				Vector3 tmpV2 = new Vector3(tmpX, 0, 0);
				
				tmpChara.charaIconSet = Instantiate(tmpChara.Prefab_charaIconSet) as GameObject;
				tmpChara.charaIconScript =  tmpChara.charaIconSet.GetComponentInParent<charaIconsetManager>();
				//親に設定
				tmpChara.charaIconSet.transform.parent = instantedCmrTracker.transform;
				//設定後位置修正
				tmpChara.charaIconSet.transform.localPosition = tmpV2;
				
				tmpChara.charaIconScript.thisCharaBase = tmpChara.charaBase;
				tmpChara.charaIconScript.thisCharaFlag = tmpChara.charaFlag;

				
				//5_charaIcon
				SpriteRenderer tmpSR = tmpChara.charaIconSet.transform.FindChild ("5_charaIcon").gameObject.GetComponent<SpriteRenderer>();
				tmpSR.sprite = this.getCharacterIconSprite (tmpChara.CharaNumber);


				//tmpChara.charaIconScript.GetComponent<charaIconset_modeIcon>().setModeIcon(tmpChara.charaScript.thisChara.battleStatus.charaMode);
				//modeIconScript = modeIcon.gameObject.
				//modeIconScript.setModeIcon (thisCharaBaseScrpt.thisChara.battleStatus.charaMode);
			}
		}
	}

	public void grantExp_forAllChara(HashSet<int> argsList, float argsGrantExp){
		//対象キャラが多いほど補正をかける
		float totalExp = argsGrantExp * (1f + (0.2f * (argsList.Count - 1f)));
		
		foreach (int intI in argsList) {
			//しんでいないか確認

			if (loadedCharaList.charalist[intI].charaBase != null){
				float tmpExp = totalExp / argsList.Count;
				loadedCharaList.charalist[intI].charaScript.getExp(tmpExp);
			}
		}
	}


	//
	//chara Icon Menu のスクロール
	//
	public void scrollCharaIcons(int argsVector){
		// charaIconPage is not Index (1-3) private
		charaIconPage += argsVector;

		if (charaIconPage < 1) {
			//下限
			charaIconPage = 1;
		}else if (charaIconPage > 3){
			//上限
			charaIconPage = 3;
		}else{
			//scroll 処理
			this.createCharaIconSet(charaIconPage);
		}
	}

	public GameObject getMostNearCharacter(Vector3 argsWorldPosision){
		float tmpNearVal = 999999;
		GameObject retGO = null;

		foreach (gameStartingVariable_Single tmp in this.loadedCharaList.charalist) {
			if (tmp.charaDestoryF == false){
				Vector3 tmpV = tmp.charaBase.transform.position - argsWorldPosision;
				if (tmpNearVal > tmpV.magnitude){
					tmpNearVal = tmpV.magnitude;
					retGO = tmp.charaBase;
				}
			}
		}

		return retGO;
	}

	
	public battleTextCanvasParentScript getBattleTextCanvasS(){

		if (battleTextInstance == null) {
			GameObject tmpGO = (GameObject)Instantiate(_battleTextCanvasPrefab);
			battleTextInstance = tmpGO.GetComponent<battleTextCanvasParentScript>();
				}

		return battleTextInstance;
	}

}

//


public class gameStartingVariable_Single{
	public enumCharaNum CharaNumber;
	public float totalExp;
	public int equipNumber;

	//初期にセットする必要がある関数
	public Object Prefab_charaGrh;
	public Object Prefab_charaFlag;
	public Object Prefab_charaIconSet;

	public bool charaDestoryF = false;
	public GameObject charaBase;
	public GameObject charaFlag;
	public GameObject charaIconSet;

	//prefab ロード時にセット
	public allCharaBase charaScript;
	public charaIconsetManager charaIconScript;
	//public allChara charaIconScript;

}

public class gameStartingVariable{
	public List<gameStartingVariable_Single> charalist = new List<gameStartingVariable_Single>();

	public void setData(enumCharaNum argsCharaNo, float totalExp, int argsEquip){
		gameStartingVariable_Single tmpVal = new gameStartingVariable_Single ();

		tmpVal.CharaNumber = argsCharaNo;
		tmpVal.totalExp = totalExp;
		tmpVal.equipNumber = argsEquip;
		
		tmpVal.Prefab_charaGrh = Resources.Load<GameObject>("Prefabs/charaBase/charaBase");
		tmpVal.Prefab_charaIconSet = Resources.Load<GameObject>("Prefabs/charaIconSet/charaIconSet");
		//Flagはシンプルなので、ここで確定させる
		tmpVal.Prefab_charaFlag = this.getCharacterFlag_InResource (argsCharaNo);

		charalist.Add (tmpVal);
	}

	private Object getCharacterFlag_InResource(enumCharaNum argsCharaNo){
		//Flagはシンプルなので、ここで確定させる
		//プレハブから直接設定
		Object[] tmpObj = Resources.LoadAll<GameObject>("Prefabs/Flags");
		Object retGO = null;

		int tmpLen = argsCharaNo.ToString ().Length;
		string tmpFileName = "flag_" + argsCharaNo.ToString().Substring(tmpLen - 2,2);
		//Debug.Log (tmpFileName);

		for (int i=0; i < tmpObj.Length; i++) {
			if (tmpObj[i].name.Substring(0,7) == tmpFileName ){
				retGO = tmpObj[i];
				break;
			}
		}

		return retGO;
	}

}


public static class GameManagerGetter{
	private static GameManagerScript keepGMS = null;
	
	public static GameManagerScript getGameManager(){
		if (keepGMS == null) {
			GameObject retGO = GameObject.Find("GameManager");
			keepGMS = retGO.GetComponent<GameManagerScript>();
		}
		return keepGMS;
	}
}

