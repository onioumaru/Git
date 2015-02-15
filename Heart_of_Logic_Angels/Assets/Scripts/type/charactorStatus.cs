using UnityEngine;
using System.Collections;

public class charactorStatus : MonoBehaviour {
	
}


public class charaUserStatus{
	public charaBattleStatus battleStatus;
	
	public int nowLv;
	
	public int nowHP;
	public int maxHP;

	public float totalExp;
	public float nextExp;
	
	public GameManagerScript gmScript;

	
	public charaUserStatus(int charaNo, float charaExp){
		this.battleStatus = new charaBattleStatus (charaNo);
		this.totalExp = charaExp;

		gmScript = GameObject.Find("GameManager").GetComponentInChildren<GameManagerScript>();
		retTypeExp tmpExp = gmScript.calcLv (charaExp);
		this.nowLv = tmpExp.Lv;
		this.nextExp = tmpExp.nextExp;
	}
	
	public void setMode(string argsStr){
		this.battleStatus.setCharacterMode(argsStr);
	}
}

//
//
//
//
public class charaBattleStatus{
	private string charaMode;

	public charaBattle_info thisInfo;
		
	private charaBattle_info _atk_info;
	private charaBattle_info _def_info;
	private charaBattle_info _mov_info;
	private charaBattle_info _skill_info;


	public charaBattleStatus(int charaNo){
		//init
		switch (charaNo) {
		case 10:
			_atk_info = new charaBattle_info(1 ,0.5f ,1f ,1 ,0.1f ,1);
			_def_info = new charaBattle_info(0.5f ,0.5f ,0.3f ,1 ,0.05f ,0.2f);
			_mov_info = new charaBattle_info(0.1f ,0.5f ,1f ,1 ,0.2f ,1.5f);
			_skill_info = new charaBattle_info(0,0,0,0,0,0);
			break;
		}
		this.setCharacterMode("attack");
	}

	public void setCharacterMode(string argsStr){
		charaMode = argsStr;

		switch (argsStr) {
		case "attack":
			thisInfo = _atk_info;
			break;
		case "defence":
			thisInfo = _def_info;
			break;
		case "move":
			thisInfo = _mov_info;
			break;
		case "skill":
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


