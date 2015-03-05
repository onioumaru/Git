using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManagerScript : MonoBehaviour {
	//GUI上で設定
	public Sprite[] charaIconEmptyGrp;
	public SpriteRenderer[] charaIconEmptyPosision;

	//
	private bool initF = false;
	private List<float> expList;
	public gameStartingVariable chataList;

	public GameObject cmrTracker;
	private int charaIconPage=1;

	// Use this for initialization
	void Start () {
		//cmrTracker = GameObject.Find ("CameraTracker");
		argGameStageInfo argsInfo = new argGameStageInfo ();

		//Object obj = Resources.Load<GameObject>("Prefabs/charaBase/charaBase");

		this.battleStageStarter(argsInfo);
		//Debug.Log (obj.name);
	}

	
	public void destoryChara(int argsCharaNoIndex){
		//削除フラグ ON
		gameStartingVariable_Single tmpChara = chataList.charalist [argsCharaNoIndex];
		tmpChara.charaDestoryF = true;

		Destroy (tmpChara.charaBase);
		Destroy (tmpChara.charaIconSet);
		Destroy (tmpChara.charaFlag);
	}
	

	void battleStageStarter(argGameStageInfo argsInfo){

		chataList = new gameStartingVariable ();

		//Prefabsからロード
		chataList.setData (enumCharaNum.enju_01, 1, 0);
		chataList.setData (enumCharaNum.syusuran_02, 1, 0);
		//chataList.setData (enumCharaNum.suzusiro_03, 1, 0);
		//chataList.setData (enumCharaNum.gyokuran_04, 1, 0);

		for (int i=0 ; i < chataList.charalist.Count;i++){
			Vector3 tmpV = new Vector3(-2.5f, (i * 0.2f), 0);

			gameStartingVariable_Single tmpChara = chataList.charalist[i];

			//charaMain
			tmpChara.charaBase = Instantiate(tmpChara.Prefab_charaGrh) as GameObject;
			tmpChara.charaScript = tmpChara.charaBase.GetComponentInParent<allCharaBase>();
			
			tmpChara.charaScript.thisChara = new charaUserStatus (tmpChara.CharaNumber, 1f);
			tmpChara.charaFlag = Instantiate(tmpChara.Prefab_charaFlag ,tmpV ,Quaternion.identity) as GameObject;
			
			tmpChara.charaScript.thisCharaIndex = i;
			tmpChara.charaScript.thisCharaFlag = tmpChara.charaFlag;
			tmpChara.charaScript.calcdExp = this.calcLv(tmpChara.totalExp);

			tmpChara.charaScript.stopFlag = false;
		}

		this.createCharaIconSet(1);
	}

	public void createCharaIconSet(int argsPage){
		//初期化処理
		for (int i=0; i < chataList.charalist.Count; i++) {
			if (chataList.charalist[i].charaIconSet != null){
				//既に存在している場合は消去
				chataList.charalist[i].charaIconScript.destoryMe();
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

			if (tgtIndex > chataList.charalist.Count -1) {break;}

			gameStartingVariable_Single tmpChara = chataList.charalist[tgtIndex];

			if (tmpChara.charaDestoryF == false){
				//既に破壊されていないか確認

				//IconSetは、必要な時に呼び出す
				float tmpX = -0.7f + (i * 0.7f);
				Vector3 tmpV2 = new Vector3(tmpX, 0, 0);
				
				tmpChara.charaIconSet = Instantiate(tmpChara.Prefab_charaIconSet) as GameObject;
				tmpChara.charaIconScript =  tmpChara.charaIconSet.GetComponentInParent<charaIconsetManager>();
				//親に設定
				tmpChara.charaIconSet.transform.parent = cmrTracker.transform;
				//設定後位置修正
				tmpChara.charaIconSet.transform.localPosition = tmpV2;
				
				tmpChara.charaIconScript.thisCharaBase = tmpChara.charaBase;
				tmpChara.charaIconScript.thisCharaFlag = tmpChara.charaFlag;

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

			if (chataList.charalist[intI].charaBase != null){
				float tmpExp = totalExp / argsList.Count;
				chataList.charalist[intI].charaScript.getExp(tmpExp);
			}
		}
	}

	public retTypeExp calcLv(float argsExp){
		retTypeExp retExp = new retTypeExp ();

		if (initF == false) {
			this.initExpMaster();
		}

		for (int i = 0; i < 200; i++) {
			if (expList[i] > argsExp){
				//対象の経験値より小さい場合,このレベル

				retExp.Lv = i;
				retExp.nextExp = expList[i] - argsExp;

				if (i == 0){
					retExp.beforeExp = 0;
				}else{
					retExp.beforeExp = expList[i-1];
				}

				retExp.nextLvExp = expList[i];
				retExp.totalExp = argsExp;

				break;
			}
		}
		return retExp;
	}


	// init

	private void initExpMaster(){
		expList = new List<float>();
		float tmpF = 0f;

		for (int i=0; i < 200; i++) {
			tmpF = (i * i * i)+(37 * i * i)-38;
			expList.Add(tmpF);
		}
		initF = true;
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
}



// args Type

public class retTypeExp{
	public int Lv;
	public float totalExp;
	public float nextExp;
	public float beforeExp;
	public float nextLvExp;
}

public class argGameStageInfo{
	public int StageNo = 0;
	public int ClearCnt = 0;
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


		//Debug.Log (argsCharaNo.ToString());
		int tmpLen = argsCharaNo.ToString ().Length;
//		Debug.Log (argsCharaNo.ToString().Substring(tmpLen - 3,2));
		string tmpFileName = "flag_" + argsCharaNo.ToString().Substring(tmpLen - 2,2);

		for (int i=0; i < tmpObj.Length; i++) {
			if (tmpObj[i].name.Substring(0,7) == tmpFileName ){
				retGO = tmpObj[i];
				break;
			}
		}

		return retGO;
	}
}


