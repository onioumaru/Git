using UnityEngine;
using System.Collections;

public class charactorStatus : MonoBehaviour {
	
}


public class charaUserStatus{
	public charaBattleStatus battleStatus;

	public int charaNo;
	public int nowLv;
	
	public int nowHP;
	public int maxHP;

	public float totalExp;
	public float nextExp;

	public bool flyingFlag = false;
	
	public float flyingAtkMagn;
	public float flyingDefMagn;

	public GameManagerScript gmScript;

	
	public charaUserStatus(int charaNo, float charaExp){
		this.battleStatus = new charaBattleStatus (charaNo);
		this.totalExp = charaExp;
		this.charaNo = charaNo;

		gmScript = GameObject.Find("GameManager").GetComponentInChildren<GameManagerScript>();
		retTypeExp tmpExp = gmScript.calcLv (charaExp);
		this.nowLv = tmpExp.Lv;
		this.nextExp = tmpExp.nextExp;

		this.initParameter ();
	}
	
	public void setMode(chatacterMode argsMode){
		this.battleStatus.setCharacterMode(argsMode);
	}

	public void initParameter(){
		switch (this.charaNo){
		case 1:
		case 2:
		case 3:
		case 4:
		case 5:
		case 6:
		case 7:
		case 8:
		case 9:
		case 10:
			this.nowHP = (this.nowLv*13); // 100 +
			flyingAtkMagn = 0.2f;
			flyingDefMagn = 4f;
			break;
		}

		this.resetMaxParameter ();
	}

	public void resetMaxParameter(){
		switch (this.charaNo){
		case 1:
		case 2:
		case 3:
		case 4:
		case 5:
		case 6:
		case 7:
		case 8:
		case 9:
		case 10:
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
	private chatacterMode charaMode;

	public charaBattle_info thisInfo;
		
	private charaBattle_info _atk_info;
	private charaBattle_info _def_info;
	private charaBattle_info _mov_info;
	private charaBattle_info _skill_info;


	public charaBattleStatus(int charaNo){
		//init
		switch (charaNo) {
		case 1:
		case 2:
		case 3:
		case 4:
		case 5:
		case 6:
		case 7:
		case 8:
		case 9:
		case 10:
			_atk_info = new charaBattle_info(  1f ,0.5f ,1f   ,0.5f ,0.1f ,1);
			_def_info = new charaBattle_info(0.5f ,1.0f ,0.5f ,0    ,0.05f ,0.2f);
			_mov_info = new charaBattle_info(0.1f ,1.0f ,0.2f ,0    ,0.2f ,1.5f);
			_skill_info = new charaBattle_info(0,0,0,0,0,0);
			break;
		}
		this.setCharacterMode(chatacterMode.Attack);
	}

	public void setCharacterMode(chatacterMode argsMode){
		charaMode = argsMode;

		switch (argsMode) {
		case chatacterMode.Attack:
			thisInfo = _atk_info;
			break;
		case chatacterMode.Defence:
			thisInfo = _def_info;
			break;
		case chatacterMode.Move:
			thisInfo = _mov_info;
			break;
		case chatacterMode.Skill:
			thisInfo = _mov_info;
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


public enum chatacterMode{
	Attack, Defence, Move, Skill
}

