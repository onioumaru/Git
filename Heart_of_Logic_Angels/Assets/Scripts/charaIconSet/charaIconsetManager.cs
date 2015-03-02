using UnityEngine;
using System.Collections;

public class charaIconsetManager : MonoBehaviour {

	////外的に設定が必要
	public GameObject thisCharaFlag;
	public GameObject thisCharaBase;
	
	private Transform hpBar;
	private Transform expBar;
	private Transform skillTimeBar;
	//private Transform modeIcon;

	private allCharaBase thisCharaBaseScrpt;

	// Use this for initialization
	void Start () {
		hpBar = this.transform.Find("1_hpBar");
		skillTimeBar = this.transform.Find("2_skillBar");
		expBar = this.transform.Find("3_expBar");
		//modeIcon = this.transform.Find("4_modeIcon");
	}

	// Update is called once per frame
	void Update () {
		//
		//各種ゲージの更新
		//
		thisCharaBaseScrpt = thisCharaBase.GetComponentInChildren<allCharaBase> ();

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
	}
	
	public void destoryMe(){
		Destroy (this.gameObject);
	}

	public chatacterMode getThisCharaMode(){
		return thisCharaBaseScrpt.thisChara.battleStatus.charaMode;
	}
}