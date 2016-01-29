using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using MiniJSON;

public class saveDataNodeParent : MonoBehaviour {
	public Text _nodeTxt01;
	public Text _nodeTxt02;
	public Text _nodeTxt03;
	
	void Start(){
		this.createList ();
	}

	private void createSaveDataNode(int argsSaveIndx){
		int tmpStoryProgress;
		staticValueManagerS sVMS = staticValueManagerGetter.getManager ();

		//ストーリー進捗の取得
		tmpStoryProgress = sVMS.getStoryProgress_pinpoint (argsSaveIndx);
		//最大レベルキャラの取得
		saveCharaValueClass retCharaData = sVMS.getSaveChara_MaxLevel (argsSaveIndx);
		string charaName = "";

		switch (retCharaData.Number) {
		case 0:
			charaName = "エンジュ";
			break;
		case 1:
			charaName = "シュスラン";
			break;
		case 2:
			charaName = "スズシロ";
			break;
		case 3:
			charaName = "アカネ";
			break;
		case 4:
			charaName = "ホオズキ";
			break;
		case 5:
			charaName = "モクレン";
			break;
		case 6:
			charaName = "サクラ";
			break;
		case 7:
			charaName = "シオン";
			break;
		case 8:
			charaName = "ヒイラギ";
			break;
		}

		string setTxt = "SaveDate" + (argsSaveIndx + 1) + "\n Chapter " + (long)tmpStoryProgress + "\n " + charaName + " Level " + retCharaData.level.ToString("0");

		if (tmpStoryProgress == 0) {
			//newGameの場合は上書き
			setTxt = "SaveDate" + (argsSaveIndx + 1) + "\n 始めから";
				}


		switch(argsSaveIndx){
		case 0:
			_nodeTxt01.text = setTxt;
			break;
		case 1:
			_nodeTxt02.text = setTxt;
			break;
		default:
			_nodeTxt03.text = setTxt;
			break;
		}
	}

	public void createList(){
		for (int indexNo = 0; indexNo < 3; indexNo++) {
			this.createSaveDataNode(indexNo);
		}
	}

}
