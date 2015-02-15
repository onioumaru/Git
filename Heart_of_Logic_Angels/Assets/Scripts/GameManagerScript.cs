using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManagerScript : MonoBehaviour {

	//ArrayList playerCharaList = new ArrayList();

	private bool initF = false;
	private List<float> expList;


	// Use this for initialization
	void Start () {
		argGameStageInfo argsInfo = new argGameStageInfo ();

		//Object obj = Resources.Load<GameObject>("Prefabs/charaBase/charaBase");

		this.battleStageStarter(argsInfo);
		//Debug.Log (obj.name);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	

	void battleStageStarter(argGameStageInfo argsInfo){
		gameStartingVariable chataList = new gameStartingVariable ();

		//Prefabsからロード
		chataList.setData (10, 1, 0);
		chataList.setData (10, 1, 0);
		chataList.setData (10, 1, 0);
		chataList.setData (10, 1, 0);

		for (int i=0 ; i < chataList.charalist.Count;i++){
			Vector3 tmpV = new Vector3(-2.5f, i, 0);

			chataList.charalist[i].charaGrh = Instantiate(chataList.charalist[i].Prefab_charaGrh) as GameObject;


			chataList.charalist[i].charaFlag = Instantiate(chataList.charalist[i].Prefab_charaFlag ,tmpV ,Quaternion.identity) as GameObject;




			chataList.charalist[i].charaIconSet = Instantiate(chataList.charalist[i].Prefab_charaIconSet) as GameObject;
		}
	}






	public retTypeExp calcLv(float argsExp){
		retTypeExp retExp = new retTypeExp ();

		if (initF==false) {
			this.initExpMaster();
		}

		for (int i = 0; i < 200; i++) {
			if (expList[i] > argsExp){
				//対象の経験値より小さい場合,このレベル

				retExp.Lv = i;
				retExp.nextExp = expList[i] - argsExp;

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
}



// args Type

public class retTypeExp{
	public int Lv;
	public float nextExp;
}

public class argGameStageInfo{
	public int StageNo = 0;
	public int ClearCnt = 0;
}

//


public class gameStartingVariable_Single{
	public int CharaNumber;
	public float totalExp;
	public int equipNumber;

	//初期にセットする必要がある関数
	public Object Prefab_charaGrh;
	public Object Prefab_charaFlag;
	public Object Prefab_charaIconSet;

	public bool charaDestoryF = false;
	public GameObject charaGrh;
	public GameObject charaFlag;
	public GameObject charaIconSet;

	//prefab ロード時にセット
	public allChara charaScript;
	//public allChara charaIconScript;

}

public class gameStartingVariable{
	public List<gameStartingVariable_Single> charalist = new List<gameStartingVariable_Single>();

	public void setData(int argsCharaNo, float totalExp, int argsEquip){
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

	private Object getCharacterFlag_InResource(int argsCharaNo){
		//Flagはシンプルなので、ここで確定させる
		//プレハブから直接設定
		Object[] tmpObj = Resources.LoadAll<GameObject>("Prefabs/Flags");
		Object retGO = null;

		string tmpFileName = "flag_" + argsCharaNo.ToString ("00");

		for (int i=0; i < tmpObj.Length; i++) {
			if (tmpObj[i].name.Substring(0,7) == tmpFileName ){
				retGO = tmpObj[i];
				break;
			}
		}

		return retGO;
	}
}


