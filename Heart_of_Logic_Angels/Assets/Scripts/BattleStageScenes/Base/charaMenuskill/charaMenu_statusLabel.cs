using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class charaMenu_statusLabel : MonoBehaviour {
	[System.NonSerialized]
	public charaMenu_statusLabelValus _labelStatus;
	// Use this for initialization

	void Start () {
		/*
		GameManagerScript gms = GameManagerGetter.getGameManager ();

		gameStartingVariable_Single tmpSgvs = null;

		foreach (gameStartingVariable_Single gsvs in gms.loadedCharaList.charalist) {
			if ((int)gsvs.CharaNumber == _charaIndex) {
				tmpSgvs = gsvs;
				break;
			}
		}
*/
		//charaUserStatus tmpCha = tmpSgvs.charaScript.thisChara;

		string setStr = "Lv." + _labelStatus.nowLv + "\nHP : " + _labelStatus.nowHP + " / " + _labelStatus.maxHP + "\n";
		setStr += "Next Level : " + _labelStatus.nextExp + "\n";

		if (_labelStatus.restSkillCoolTime == "0.00") {
			setStr += "CoolTime : なし　準備OK！";
		} else {
			setStr += "CoolTime : 残り" + _labelStatus.restSkillCoolTime + " Sec";
		}


		this.GetComponent<Text> ().text = setStr;
	}
}

public class charaMenu_statusLabelValus{
	public string name;
	public string nowLv;
	public string nowHP;
	public string maxHP;
	public string nextExp;
	public string restSkillCoolTime;
	public enumCharaNum charaIndex;
}