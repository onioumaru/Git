using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class faceIconController : MonoBehaviour {
	public GameObject[] _faceIconObject;
	public Text _charaInfoText;
	private GameObject _charaAnime;

	//面倒なので public にするが、GUIから変更しない事
	public int selectedIconIndex;

	private staticValueManagerS sVMS;

	// Use this for initialization
	void Start () {
		Debug.Log ("In this scene, fastest Start is " + this.name);

		sVMS = staticValueManagerGetter.getManager ();

		for (int loopI = 0; loopI < 9; loopI++) {
			bool tmpB = sVMS.getCharacterEnable(loopI);
			//Debug.Log(loopI + tmpB.ToString());

			if (!tmpB){
				_faceIconObject[loopI].SetActive(false);
			}
		}

		this.setRockFlag ();

		this.showSelectedCharaInfo (0);

		soundManagerGetter.getManager().playBGM (10);
	}


	public void showSelectedCharaInfo(int argsInt){
		//表示情報の変更
		//選択情報の移動も同時に行う
		selectedIconIndex = argsInt;

		saveCharaValueClass tmpChara = sVMS.getSaveCharaValue (selectedIconIndex);

		expLevelInfo tmpExpInfo = characterLevelManagerGetter.getManager ().calcLv (tmpChara.exp);

		string tmpCap = "Level " + tmpChara.level + "\n\n";
		tmpCap += tmpChara.getCharaName() + "\n";
		tmpCap += "Next Level\n" + tmpExpInfo.nextExp + "\n";
		tmpCap += "\n";
		tmpCap += "Equipment:\n";

		_charaInfoText.text = tmpCap;
	}


	public bool[] getSorityStatus(){

		bool[] retBool = new bool[9];
		
		for (int loopI = 0; loopI < 9; loopI++) {
			//active check
			if (_faceIconObject[loopI].activeSelf){
				faceIconScript fIC = _faceIconObject[loopI].GetComponent<faceIconScript>();

				if (fIC.getThisSoriteFlag()){
					retBool[loopI] = true;
				} else {
					retBool[loopI] = false;
				}
			} else {
				retBool[loopI] = false;
			}
		}

		return retBool;
	}

	private void setRockFlag(){
		int route = sVMS.getStoryRoute();
		int progress = sVMS.getStoryProgress();
		int stage = sVMS.getStoryStage();

		string tmpS = route.ToString () + "-" + progress.ToString () + "-" + stage.ToString ();

		Debug.Log (tmpS);

		switch(tmpS){
		case "0-0-0":
			faceIconScript fIC = _faceIconObject [0].GetComponent<faceIconScript> ();
			fIC.setStatusLock ();

			break;
		}
	}
}
