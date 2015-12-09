using UnityEngine;
using System.Collections;

/*
 * 戦闘用・デフォルトパラメータ集
 * 戦闘用シーンのみで使える
 * 
 */

public class charaUserStatus{
	public charaBattleStatus battleStatus;

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
	public float restSkillCoolTime;
	
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
		switch (this.charaNo){
		case enumCharaNum.enju_01:
			this.nowHP = (this.nowLv * 61f) + 20f; // 100 +
			flyingAtkMagn = 0.2f;
			flyingDefMagn = 4f;
			MaxSkillCoolTime = 15f;
			restSkillCoolTime = 0;
			break;

		case enumCharaNum.syusuran_02:
			this.nowHP = (this.nowLv * 35f) + 20f; // 100 +
			flyingAtkMagn = 0.2f;
			flyingDefMagn = 4f;
			MaxSkillCoolTime = 15f;
			restSkillCoolTime = 0;
			break;

		case enumCharaNum.suzusiro_03:
			this.nowHP = (this.nowLv * 56f) + 20f; // 100 +
			flyingAtkMagn = 0.4f;
			flyingDefMagn = 3f;
			MaxSkillCoolTime = 15f;
			restSkillCoolTime = 0;
			break;

		case enumCharaNum.gyokuran_04:
			this.nowHP = (this.nowLv * 59f) + 20f; // 100 +
			flyingAtkMagn = 0.2f;
			flyingDefMagn = 4f;
			MaxSkillCoolTime = 15f;
			restSkillCoolTime = 0;
			break;

		case enumCharaNum.houzuki_05:
			this.nowHP = (this.nowLv * 47f) + 20f; // 100 +
			flyingAtkMagn = 0.2f;
			flyingDefMagn = 4f;
			MaxSkillCoolTime = 15f;
			restSkillCoolTime = 0;
			break;

		case enumCharaNum.mokuren_06:
			this.nowHP = (this.nowLv * 32f) + 20f; // 100 +
			flyingAtkMagn = 0.2f;
			flyingDefMagn = 4f;
			MaxSkillCoolTime = 15f;
			restSkillCoolTime = 0;
			break;

		case enumCharaNum.sakura_07:
			this.nowHP = (this.nowLv * 29f) + 20f; // 100 +
			flyingAtkMagn = 0.2f;
			flyingDefMagn = 4f;
			MaxSkillCoolTime = 15f;
			restSkillCoolTime = 0;
			break;

		case enumCharaNum.sion_08:
			this.nowHP = (this.nowLv * 41f) + 20f; // 100 +
			flyingAtkMagn = 0.2f;
			flyingDefMagn = 4f;
			MaxSkillCoolTime = 15f;
			restSkillCoolTime = 0;
			break;

		case enumCharaNum.hiragi_09:
			this.nowHP = (this.nowLv * 54f) + 20f; // 100 +
			flyingAtkMagn = 0.2f;
			flyingDefMagn = 4f;
			MaxSkillCoolTime = 15f;
			restSkillCoolTime = 0;
			break;

		case enumCharaNum.test_10:
			this.nowHP = (this.nowLv * 29f) + 20f; // 100 +
			flyingAtkMagn = 0.2f;
			flyingDefMagn = 4f;
			MaxSkillCoolTime = 3f;
			restSkillCoolTime = 0;
			break;
		}

		this.maxHP = this.nowHP;

		// 一旦保留
		//this.resetMaxParameter ();
	}

	public void resetMaxParameter(){
		switch (this.charaNo){
		case enumCharaNum.enju_01:
			this.maxHP = (this.nowLv * 61f) + 20f; // 100 +
			break;
		case enumCharaNum.syusuran_02:
			this.maxHP = (this.nowLv * 29f) + 20f; // 100 +
			break;
		case enumCharaNum.suzusiro_03:
		case enumCharaNum.gyokuran_04:
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
			_atk_info = new charaBattle_info(  1f ,1f , 1f  ,0.5f, 0.1f, 1);
			_def_info = new charaBattle_info(0.5f ,2f ,0.5f ,0 ,0.05f ,0.1f);
			_mov_info = new charaBattle_info(0.1f ,3f ,0.2f ,0 ,0.2f ,1.5f);
			_skill_info = new charaBattle_info(4f, 1f, 0, 0, 0 , 0);
			break;
		case enumCharaNum.syusuran_02:
			//遠距離バランス型
			_atk_info = new charaBattle_info(  1f , 1.2f, 2f, 0.9f , 0.09f , 1.05f);
			_def_info = new charaBattle_info(0.5f , 2f ,0.1f , 0.1f, 0.05f , 0.2f);
			_mov_info = new charaBattle_info(0.6f , 3f ,1.5f , 0.2f, 0.15f , 2.5f);
			_skill_info = new charaBattle_info(4f, 1f, 0, 0, 0, 0);
			break;
		case enumCharaNum.suzusiro_03:
			//バランスタンク型
			_atk_info = new charaBattle_info(  1f ,1f , 1f  ,0.5f, 0.1f, 1);
			_def_info = new charaBattle_info(0.5f ,2f ,0.5f ,0 ,0.05f ,0.1f);
			_mov_info = new charaBattle_info(0.1f ,3f ,0.2f ,0 ,0.2f ,1.5f);
			_skill_info = new charaBattle_info(4f, 1f, 0, 0, 0 , 0);
			break;
		case enumCharaNum.gyokuran_04:
			//近距離攻撃型
			_atk_info = new charaBattle_info(  1f ,1f , 1f  ,0.5f, 0.1f, 1);
			_def_info = new charaBattle_info(0.5f ,2f ,0.5f ,0 ,0.05f ,0.1f);
			_mov_info = new charaBattle_info(0.1f ,3f ,0.2f ,0 ,0.2f ,1.5f);
			_skill_info = new charaBattle_info(4f, 1f, 0, 0, 0 , 0);
			break;
		case enumCharaNum.houzuki_05:
			//近距離スピード型
			_atk_info = new charaBattle_info(  1f ,1f , 1f  ,0.5f, 0.1f, 1);
			_def_info = new charaBattle_info(0.5f ,2f ,0.5f ,0 ,0.05f ,0.1f);
			_mov_info = new charaBattle_info(0.1f ,3f ,0.2f ,0 ,0.2f ,1.5f);
			_skill_info = new charaBattle_info(4f, 1f, 0, 0, 0 , 0);
			break;
		case enumCharaNum.mokuren_06:
			//遠距離特化型
			_atk_info = new charaBattle_info(  1f ,1f , 1f  ,0.5f, 0.1f, 1);
			_def_info = new charaBattle_info(0.5f ,2f ,0.5f ,0 ,0.05f ,0.1f);
			_mov_info = new charaBattle_info(0.1f ,3f ,0.2f ,0 ,0.2f ,1.5f);
			_skill_info = new charaBattle_info(4f, 1f, 0, 0, 0 , 0);
			break;
		case enumCharaNum.sakura_07:
			//遠距離スピード型
			_atk_info = new charaBattle_info(  1f ,1f , 1f  ,0.5f, 0.1f, 1);
			_def_info = new charaBattle_info(0.5f ,2f ,0.5f ,0 ,0.05f ,0.1f);
			_mov_info = new charaBattle_info(0.1f ,3f ,0.2f ,0 ,0.2f ,1.5f);
			_skill_info = new charaBattle_info(4f, 1f, 0, 0, 0 , 0);
			break;
		case enumCharaNum.sion_08:
			//遠距離特殊型
			_atk_info = new charaBattle_info(  1f ,1f , 1f  ,0.5f, 0.1f, 1);
			_def_info = new charaBattle_info(0.5f ,2f ,0.5f ,0 ,0.05f ,0.1f);
			_mov_info = new charaBattle_info(0.1f ,3f ,0.2f ,0 ,0.2f ,1.5f);
			_skill_info = new charaBattle_info(4f, 1f, 0, 0, 0 , 0);
			break;
		case enumCharaNum.hiragi_09:
			//万能型
			_atk_info = new charaBattle_info(  1f ,1f , 1f  ,0.5f, 0.1f, 1);
			_def_info = new charaBattle_info(0.5f ,2f ,0.5f ,0 ,0.05f ,0.1f);
			_mov_info = new charaBattle_info(0.1f ,3f ,0.2f ,0 ,0.2f ,1.5f);
			_skill_info = new charaBattle_info(4f, 1f, 0, 0, 0 , 0);
			break;
		case enumCharaNum.test_10:
			//for debug
			_atk_info = new charaBattle_info(  1f ,1f ,1f   ,0.5f ,0.1f ,1);
			_def_info = new charaBattle_info(0.5f ,2f ,0.5f ,0    ,0.05f ,0.2f);
			_mov_info = new charaBattle_info(0.1f ,3f ,0.2f ,0    ,0.2f ,1.5f);
			_skill_info = new charaBattle_info(4f, 1f, 0, 0, 0, 0);
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
	enju_01, syusuran_02, suzusiro_03, gyokuran_04, houzuki_05, mokuren_06, sakura_07, sion_08, hiragi_09 ,test_10, maxCnt
}

public enum enumEnemyType{
	small001
	,largeWolf01
}

public class typeEnemyStatus{
	public float maxHP;
	public float nowHP;

	public float level;
	public float grantExp;
	public float attackDeleySec;
	public float multiAttackCount;
	public float AttackingPower;

	public typeEnemyStatus(float argsLevel, enumEnemyType argsType){
		switch (argsType) {
		case enumEnemyType.largeWolf01:
			this.maxHP            = argsLevel * 32f + 200f;
			this.nowHP            = this.maxHP;
			this.grantExp         = argsLevel * 112;
			
			this.attackDeleySec = 3f;
			this.multiAttackCount = 1f;
			this.AttackingPower = argsLevel * 1f;
			
			break;

		default:
			this.maxHP            = argsLevel * 14f;
			this.nowHP            = this.maxHP;
			this.grantExp         = argsLevel * 10;

			this.attackDeleySec = 3f;
			this.multiAttackCount = 2f;
			this.AttackingPower = argsLevel * 1f;

			break;
		}

	}
}


