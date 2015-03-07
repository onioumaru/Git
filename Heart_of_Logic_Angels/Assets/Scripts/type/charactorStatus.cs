using UnityEngine;
using System.Collections;

public class charactorStatus : MonoBehaviour {
	
}

public class charaUserStatus{
	public charaBattleStatus battleStatus;

	public enumCharaNum charaNo;
	public int nowLv;
	
	public int nowHP;
	public int maxHP;

	public float totalExp;
	public float nextExp;

	public bool flyingFlag = false;
	
	public float flyingAtkMagn;
	public float flyingDefMagn;

	public GameManagerScript gmScript;

	public float MaxSkillCoolTime;
	public float restSkillCoolTime;
	
	public charaUserStatus(enumCharaNum charaNo, float charaExp){
		this.battleStatus = new charaBattleStatus (charaNo);
		this.totalExp = charaExp;
		this.charaNo = charaNo;

		gmScript = GameObject.Find("GameManager").GetComponentInChildren<GameManagerScript>();
		retTypeExp tmpExp = gmScript.calcLv (charaExp);
		this.nowLv = tmpExp.Lv;
		this.nextExp = tmpExp.nextExp;

		this.initParameter ();
	}
	
	public void setMode(characterMode argsMode){
		this.battleStatus.setCharacterMode(argsMode);
	}

	public void initParameter(){
		switch (this.charaNo){
		case enumCharaNum.enju_01:
		case enumCharaNum.syusuran_02:
		case enumCharaNum.suzusiro_03:
		case enumCharaNum.gyokuran_04:
		case enumCharaNum.houzuki_05:
		case enumCharaNum.mokuren_06:
		case enumCharaNum.shion_07:
		case enumCharaNum.syukaido_08:
		case enumCharaNum.hiragi_09:
		case enumCharaNum.test_10:
			this.nowHP = (this.nowLv*13); // 100 +
			flyingAtkMagn = 0.2f;
			flyingDefMagn = 4f;
			MaxSkillCoolTime = 3f;
			restSkillCoolTime = 0;
			break;
		}

		this.resetMaxParameter ();
	}

	public void resetMaxParameter(){
		switch (this.charaNo){
		case enumCharaNum.enju_01:
		case enumCharaNum.syusuran_02:
		case enumCharaNum.suzusiro_03:
		case enumCharaNum.gyokuran_04:
		case enumCharaNum.houzuki_05:
		case enumCharaNum.mokuren_06:
		case enumCharaNum.shion_07:
		case enumCharaNum.syukaido_08:
		case enumCharaNum.hiragi_09:
		case enumCharaNum.test_10:
			this.maxHP = (this.nowLv*13); // 100 +
			break;
		}
	}
}

//
//
//
//

public class charaBattleStatus{
	public characterMode charaMode;
	public characterMode beforeCharaMode;
	public charaBattle_info thisInfo;
		
	private charaBattle_info _atk_info;
	private charaBattle_info _def_info;
	private charaBattle_info _mov_info;
	private charaBattle_info _skill_info;

	public charaBattleStatus(enumCharaNum charaNo){
		//init
		switch (charaNo) {
		case enumCharaNum.enju_01:
		case enumCharaNum.syusuran_02:
		case enumCharaNum.suzusiro_03:
		case enumCharaNum.gyokuran_04:
		case enumCharaNum.houzuki_05:
		case enumCharaNum.mokuren_06:
		case enumCharaNum.shion_07:
		case enumCharaNum.syukaido_08:
		case enumCharaNum.hiragi_09:
		case enumCharaNum.test_10:
			_atk_info = new charaBattle_info(  1f ,0.5f ,1f   ,0.5f ,0.1f ,1);
			_def_info = new charaBattle_info(0.5f ,0.6f ,0.5f ,0    ,0.05f ,0.2f);
			_mov_info = new charaBattle_info(0.1f ,1.0f ,0.2f ,0    ,0.2f ,1.5f);
			_skill_info = new charaBattle_info(4f, 1f, 0, 0, 0, 0);
			break;
		}
		this.setCharacterMode(characterMode.Attack);
	}

	public void setCharacterMode(characterMode argsMode){
		//直前のモードは取っておく
		this.beforeCharaMode = this.charaMode;

		this.charaMode = argsMode;

		switch (argsMode) {
		case characterMode.Attack:
			thisInfo = _atk_info;
			break;
		case characterMode.Defence:
			thisInfo = _def_info;
			break;
		case characterMode.Move:
			thisInfo = _mov_info;
			break;
		case characterMode.Skill:
			thisInfo = _skill_info;
			break;
		default:
			break;
		}
	}
}

public class charaBattle_info{
	public float attackMagn;
	public float attackDelayTime;
	public float attackRange;
	public float attackFreezeTime;
	public float movingSpeedMagn;
	public float defenceMagn;

	//overload
	public charaBattle_info(float atkMagn, float atkDelayT, float atkRange, float atkFreezeTime, float mvSpd, float defM){
		this.attackMagn = atkMagn;
		this.attackDelayTime = atkDelayT;
		this.attackRange = atkRange;
		this.attackFreezeTime = atkFreezeTime;
		this.movingSpeedMagn = mvSpd;
		this.defenceMagn = defM;
	}
}



public enum characterMode{
	Attack, Defence, Move, Skill
}

public enum enumCharaNum{
	enju_01, syusuran_02, suzusiro_03, gyokuran_04, houzuki_05, mokuren_06, shion_07, syukaido_08, hiragi_09 ,test_10,noInit
}



