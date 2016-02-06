using UnityEngine;
using System.Collections;

public class charaIconsetManager : MonoBehaviour {

	////外的に設定が必要
	[System.NonSerialized]
	public GameObject thisCharaFlag;
	[System.NonSerialized]
	public GameObject thisCharaBase;

	public GameObject _iconActionScript;
	
	private Transform hpBar;
	private Transform expBar;
	private Transform skillTimeBar;
	//private Transform modeIcon;

	private allCharaBase thisCharaBaseScrpt;

	public int _charalistIndex;
	public charaMenu_statusLabelValus labelStatus;

	// Use this for initialization
	void Start () {
		hpBar = this.transform.Find("1_hpBar");
		skillTimeBar = this.transform.Find("2_skillBar");
		expBar = this.transform.Find("3_expBar");
		//modeIcon = this.transform.Find("4_modeIcon");

		thisCharaBase = GameManagerGetter.getGameManager ().loadedCharaList.charalist [_charalistIndex].charaBase;
		thisCharaBaseScrpt = thisCharaBase.GetComponentInChildren<allCharaBase> ();

		//charaUserStatus a = new charaUserStatus(

	}

	// Update is called once per frame
	void Update () {
		//
		//各種ゲージの更新
		//

		float hpBarLength = (float)thisCharaBaseScrpt.thisChara.nowHP / (float)thisCharaBaseScrpt.thisChara.maxHP;
		Vector3 tmpV = new Vector3 (hpBarLength, 1, 1);
		hpBar.localScale = tmpV;

		float fullExp = thisCharaBaseScrpt.calcdExp.nextLvExp - thisCharaBaseScrpt.calcdExp.beforeExp;
		float nowExp = thisCharaBaseScrpt.calcdExp.totalExp - thisCharaBaseScrpt.calcdExp.beforeExp;
		float barLength = (float)(nowExp / fullExp);
		if (barLength <= 0.01) {
			barLength = 0.01f;
		}

		Vector3 tmpV2 = new Vector3 (barLength, 1f, 1f);
		expBar.localScale = tmpV2;


		float coolTimeBarLength = thisCharaBaseScrpt.getRestCoolTime ();
		Vector3 tmpV3 = new Vector3 (coolTimeBarLength, 1f, 1f);
		skillTimeBar.localScale = tmpV3;

		labelStatus = new charaMenu_statusLabelValus ();
		//labelStatus.name = thisCharaBaseScrpt.thisChara.name;
		labelStatus.nowLv = thisCharaBaseScrpt.thisChara.nowLv.ToString("0");
		labelStatus.nowHP = thisCharaBaseScrpt.thisChara.nowHP.ToString("0");
		labelStatus.maxHP = thisCharaBaseScrpt.thisChara.maxHP.ToString("0");
		labelStatus.nextExp = thisCharaBaseScrpt.thisChara.nextExp.ToString("0");
		labelStatus.restSkillCoolTime = thisCharaBaseScrpt.thisChara.restSkillCoolTime.ToString ("0.00");
		labelStatus.charaIndex = thisCharaBaseScrpt.thisChara.charaNo;
	}
	
	public void destoryMe(){
		Destroy (this.gameObject);
	}

	public characterMode getThisCharaMode(){
		return thisCharaBaseScrpt.thisChara.battleStatus.charaMode;
	}
}