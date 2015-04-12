using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * ストーリの進行度管理
 * progress : イベント進行度
 * eventFlag : フラグ管理
 */


public class storyProgress{
	public storyProgressEnum progress;
	public Hashtable eventFlag;

	public storyProgress(){
		//初期化
		progress = storyProgressEnum.newGame;
		eventFlag = new Hashtable();
		}


	public bool checkEventFlag(string argsFlagName){
		return eventFlag.Contains (argsFlagName);
	}
}

public enum storyProgressEnum{
	newGame,
	endTutorial_nextSyusuranBattle,
	addSyusuran_nextHouzuki,
	addHouzuki_nextStageSelect01,
	endStageSelect01_nextAlrunaBattle,
}
