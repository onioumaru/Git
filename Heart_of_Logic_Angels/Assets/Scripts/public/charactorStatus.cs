using UnityEngine;
using System.Collections;

/*
 * 戦闘用・デフォルトパラメータ集
 * 戦闘用シーンのみで使える
 * 
 */
public class charaUserStatus : Behaviour{
	[System.NonSerialized]
	public charaBattleStatus battleStatus;

	[System.NonSerialized]
	public enumCharaNum charaNo;
	public int nowLv;

	public float nowHP;
	public float maxHP;

	public float totalExp;
	public float nextExp;

	public bool flyingFlag = false;
	
	public float flyingAtkMagn;
	public float flyingDefMagn;

	public float MaxSkillCoolTime;
	//クールタイムのカウントダウンで使用するため、0
	public float restSkillCoolTime = 0f;
	
	public charaUserStatus(enumCharaNum charaNo, float charaExp){
		this.battleStatus = new charaBattleStatus (charaNo);
		this.totalExp = charaExp;
		this.charaNo = charaNo;

		expLevelInfo tmpExp = characterLevelManagerGetter.getManager ().calcLv (charaExp);

		this.nowLv = tmpExp.Lv;
		this.nextExp = tmpExp.nextExp;

		this.initParameter ();
	}
	
	public void setMode(characterMode argsMode){
		this.battleStatus.setCharacterMode(argsMode);
	}

	public void initParameter(){
		//restSkillCoolTime = 0;

		switch (this.charaNo){
		case enumCharaNum.enju_01:
			this.nowHP = (this.nowLv * 57f) + 20f; // 100 +
			flyingAtkMagn = 0.2f;
			flyingDefMagn = 2.5f;
			MaxSkillCoolTime = 45f;
			break;

		case enumCharaNum.syusuran_02:
			this.nowHP = (this.nowLv * 49f) + 20f; // 100 +
			flyingAtkMagn = 0.2f;
			flyingDefMagn = 2.5f;
			MaxSkillCoolTime = 75f;
			break;

		case enumCharaNum.suzusiro_03:
			this.nowHP = (this.nowLv * 56f) + 20f; // 100 +
			flyingAtkMagn = 0.4f;
			flyingDefMagn = 1.5f;
			MaxSkillCoolTime = 300f;
			break;

		case enumCharaNum.akane_04:
			this.nowHP = (this.nowLv * 49f) + 20f; // 100 +
			flyingAtkMagn = 0.2f;
			flyingDefMagn = 2.5f;
			MaxSkillCoolTime = 90f;
			break;

		case enumCharaNum.houzuki_05:
			this.nowHP = (this.nowLv * 62f) + 20f; // 100 +
			flyingAtkMagn = 0.9f;
			flyingDefMagn = 2.5f;
			MaxSkillCoolTime = 300f;
			break;

		case enumCharaNum.mokuren_06:
			this.nowHP = (this.nowLv * 39f) + 20f; // 100 +
			flyingAtkMagn = 0.2f;
			flyingDefMagn = 2.5f;
			MaxSkillCoolTime = 15f;
			break;

		case enumCharaNum.sakura_07:
			this.nowHP = (this.nowLv * 49f) + 20f; // 100 +
			flyingAtkMagn = 0.2f;
			flyingDefMagn = 2.5f;
			MaxSkillCoolTime = 15f;
			break;

		case enumCharaNum.sion_08:
			this.nowHP = (this.nowLv * 41f) + 20f; // 100 +
			flyingAtkMagn = 0.2f;
			flyingDefMagn = 2.5f;
			MaxSkillCoolTime = 15f;
			break;

		case enumCharaNum.hiragi_09:
			this.nowHP = (this.nowLv * 54f) + 20f; // 100 +
			flyingAtkMagn = 0.2f;
			flyingDefMagn = 2.5f;
			MaxSkillCoolTime = 15f;
			break;
		}

		this.maxHP = this.nowHP;

		// 一旦保留
		//this.resetMaxParameter ();
	}

	//現在保留中
	public void resetMaxParameter(){
		switch (this.charaNo){
		case enumCharaNum.enju_01:
			this.maxHP = (this.nowLv * 61f) + 20f; // 100 +
			break;
		case enumCharaNum.syusuran_02:
			this.maxHP = (this.nowLv * 29f) + 20f; // 100 +
			break;
		case enumCharaNum.suzusiro_03:
		case enumCharaNum.akane_04:
		case enumCharaNum.houzuki_05:
		case enumCharaNum.mokuren_06:
		case enumCharaNum.sakura_07:
		case enumCharaNum.sion_08:
		case enumCharaNum.hiragi_09:
		case enumCharaNum.test_10:
			this.maxHP = (this.nowLv * 29f) + 20f; // 100 +
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
			//近接タンク型
			_atk_info = new charaBattle_info(1.2f , 1f, 0.6f, 1f,  0.1f, 0.9f);
			_def_info = new charaBattle_info(0.33f, 1f, 0.6f, 0 ,0.06f, 0.15f);
			_mov_info = new charaBattle_info(0.1f , 3f, 0.6f, 0 ,0.16f, 1.5f);
			_skill_info = new charaBattle_info(6f, 1f, 0f, 0f, 0f , 0f);
			break;

		case enumCharaNum.syusuran_02:
			//遠距離バランス型
			_atk_info = new charaBattle_info(0.8f , 1.2f, 2f, 0.9f , 0.09f , 1.05f);
			_def_info = new charaBattle_info(0.2f , 1f, 1.5f, 0.01f, 0.05f, 0.6f);
			_mov_info = new charaBattle_info(0.6f , 3f, 0.2f, 0.2f, 0.13f , 1.5f);
			_skill_info = new charaBattle_info(6f, 1f, 0, 0, 0, 0f);
			break;

		case enumCharaNum.suzusiro_03:
			//バランスタンク型
			_atk_info = new charaBattle_info(1f  ,1.2f,  1f,0.5f, 0.07f,0.8f);
			_def_info = new charaBattle_info(1f  ,1.2f,0.7f,0.5f, 0.03f,0.1f);
			_mov_info = new charaBattle_info(0.1f,1.2f,0.6f,0.5f, 0.13f, 1f);
			_skill_info = new charaBattle_info(6f, 1f, 0, 0, 0, 0f);
			break;

		case enumCharaNum.akane_04:
			//近距離高火力型
			_atk_info = new charaBattle_info(1.1f, 0.9f,   1f, 0f,  0.1f, 0.85f);
			_def_info = new charaBattle_info(0.1f, 0.2f, 0.7f, 0.2f, 0.08f, 0.7f);
			_mov_info = new charaBattle_info(0.1f ,1.1f, 0.1f, 0f, 0.16f, 1f);
			_skill_info = new charaBattle_info(6f, 1f, 0, 0, 0 , 0f);
			break;

		case enumCharaNum.houzuki_05:
			//近距離スピード型ヒーラー
			_atk_info = new charaBattle_info(1.53f, 2.1f, 0.8f, 2.1f, 0.13f, 1.1f);
			_def_info = new charaBattle_info(1.53f, 3.5f, 0.5f , 0.5f ,0.1f, 0.3f);
			_mov_info = new charaBattle_info(0.1f, 3f, 0.1f , 0, 0.3f, 2f);
			_skill_info = new charaBattle_info(6f, 1f, 0, 0, 0 , 0f);
			break;

		case enumCharaNum.mokuren_06:
			//遠距離特化型
			_atk_info = new charaBattle_info(  1f ,1f , 1f  ,0.5f, 0.1f, 1);
			_def_info = new charaBattle_info(0.5f ,2f ,0.5f ,0 ,0.05f ,0.1f);
			_mov_info = new charaBattle_info(0.1f ,3f ,0.2f ,0 ,0.2f ,1.5f);
			_skill_info = new charaBattle_info(6f, 1f, 0, 0, 0 , 0f);
			break;
		case enumCharaNum.sakura_07:
			//遠距離スピード型
			_atk_info = new charaBattle_info(  1f ,1f , 1f  ,0.5f, 0.1f, 1);
			_def_info = new charaBattle_info(0.5f ,2f ,0.5f ,0 ,0.05f ,0.1f);
			_mov_info = new charaBattle_info(0.1f ,3f ,0.2f ,0 ,0.2f ,1.5f);
			_skill_info = new charaBattle_info(6f, 1f, 0, 0, 0 , 0f);
			break;
		case enumCharaNum.sion_08:
			//遠距離特殊型
			_atk_info = new charaBattle_info(  1f ,1f , 1f  ,0.5f, 0.1f, 1);
			_def_info = new charaBattle_info(0.5f ,2f ,0.5f ,0 ,0.05f ,0.1f);
			_mov_info = new charaBattle_info(0.1f ,3f ,0.2f ,0 ,0.2f ,1.5f);
			_skill_info = new charaBattle_info(6f, 1f, 0, 0, 0 , 0f);
			break;
		case enumCharaNum.hiragi_09:
			//万能型
			_atk_info = new charaBattle_info(  1f ,1f , 1f  ,0.5f, 0.1f, 1);
			_def_info = new charaBattle_info(0.5f ,2f ,0.5f ,0 ,0.05f ,0.1f);
			_mov_info = new charaBattle_info(0.1f ,3f ,0.2f ,0 ,0.2f ,1.5f);
			_skill_info = new charaBattle_info(6f, 1f, 0, 0, 0 , 0f);
			break;
		case enumCharaNum.test_10:
			//for debug
			_atk_info = new charaBattle_info(  1f ,1f ,1f   ,0.5f ,0.1f ,1);
			_def_info = new charaBattle_info(0.5f ,2f ,0.5f ,0    ,0.05f ,0.2f);
			_mov_info = new charaBattle_info(0.1f ,3f ,0.2f ,0    ,0.2f ,1.5f);
			_skill_info = new charaBattle_info(6f, 1f, 0, 0, 0 , 0f);
			break;
		}
		this.setCharacterMode(characterMode.Attack);
	}

	
	public float getDamage_CalCharaMode(){

		switch (this.charaMode) {
		case characterMode.Attack:
			return _atk_info.defenceMagn;
			break;

		case characterMode.Defence:
			return _def_info.defenceMagn;
			break;

		case characterMode.Move:
			return _mov_info.defenceMagn;
			break;

		case characterMode.Skill:
		default:
			return _skill_info.defenceMagn;
			break;
		}
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
	enju_01, syusuran_02, suzusiro_03, akane_04, houzuki_05, mokuren_06, sakura_07, sion_08, hiragi_09 ,test_10, maxCnt
}
