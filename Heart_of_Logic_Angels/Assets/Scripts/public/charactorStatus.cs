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
		case enumCharaNum.syusuran_02:
		case enumCharaNum.suzusiro_03:
		case enumCharaNum.gyokuran_04:
		case enumCharaNum.houzuki_05:
		case enumCharaNum.mokuren_06:
		case enumCharaNum.shion_07:
		case enumCharaNum.syukaido_08:
		case enumCharaNum.hiragi_09:
		case enumCharaNum.test_10:
			this.nowHP = (this.nowLv * 29f) + 20f; // 100 +
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
		case enumCharaNum.syusuran_02:
		case enumCharaNum.suzusiro_03:
		case enumCharaNum.gyokuran_04:
		case enumCharaNum.houzuki_05:
		case enumCharaNum.mokuren_06:
		case enumCharaNum.shion_07:
		case enumCharaNum.syukaido_08:
		case enumCharaNum.hiragi_09:
		case enumCharaNum.test_10:
			_atk_info = new charaBattle_info(  1f ,1f ,1f   ,0.5f ,0.1f ,1);
			_def_info = new charaBattle_info(0.5f ,2f ,0.5f ,0    ,0.05f ,0.2f);
			_mov_info = new charaBattle_info(0.1f ,3f ,0.2f ,0    ,0.2f ,1.5f);
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

public enum enumEnemyType{
	small001
	,a
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


