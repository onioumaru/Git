using UnityEngine;
using System.Collections;

public class typeEnemyStatus : MonoBehaviour{
	public float maxHP;
	public float nowHP;

	public float level;
	public float grantExp;
	public float attackDeleySec;
	public float multiAttackCount;
	public float AttackingPower;

	public typeEnemyStatus(float argsLevel, enumEnemyType argsType){

		this.multiAttackCount = 1f;

		switch (argsType) {

		case enumEnemyType.largeWolf01:
			//Lv15 
			this.maxHP            = argsLevel * argsLevel * 16f + 200f;
			this.nowHP            = this.maxHP;
			this.grantExp         = argsLevel * 112f;

			this.attackDeleySec = 3f;
			this.AttackingPower = argsLevel * 9.6f;

			break;

		case enumEnemyType.piyo:
			//Lv3 で　HP500　を目安
			this.maxHP            = argsLevel * 50f;
			this.nowHP            = this.maxHP;
			this.grantExp         = argsLevel + 10;

			this.attackDeleySec = 3f;
			this.AttackingPower = argsLevel * 3f;

			break;

		case enumEnemyType.rabbitman01:
			//Lv10 で　HP100
			//早くて弱い設計
			this.maxHP            = argsLevel * argsLevel * 0.8f;
			this.nowHP            = this.maxHP;
			this.grantExp         = argsLevel * argsLevel * 0.9f + 50f;

			this.attackDeleySec = 1f;
			this.AttackingPower = argsLevel * 0.84f;

			break;


		case enumEnemyType.Tower01:
			//Lv10 で　HP1000
			//Lv20 で　HP4000
			//固い
			this.maxHP            = argsLevel * argsLevel * 25f;
			this.nowHP            = this.maxHP;
			this.grantExp         = argsLevel * argsLevel * 2.2f;

			this.attackDeleySec = 5f;
			this.AttackingPower = argsLevel * 5.5f;

			break;

		case enumEnemyType.daemon01:
			this.maxHP            = argsLevel * argsLevel * 6.5f;
			this.nowHP            = this.maxHP;
			this.grantExp         = argsLevel * 11f;

			this.attackDeleySec = 3f;
			this.AttackingPower = argsLevel * 2.56f;

			break;

		case enumEnemyType.tamanekoB01:
			//固い&攻撃間隔が長い
			this.maxHP            = argsLevel * argsLevel * 29f;
			this.nowHP            = this.maxHP;
			this.grantExp         = argsLevel * argsLevel * 1.123f;

			this.attackDeleySec = 5f;
			this.AttackingPower = argsLevel * 4.17f;

			break;

		case enumEnemyType.daikon01:
			//固い&攻撃間隔が長い
			this.maxHP            = argsLevel * argsLevel * 11f;
			this.nowHP            = this.maxHP;
			this.grantExp         = argsLevel * argsLevel * 1.123f;

			this.attackDeleySec = 2f;
			this.AttackingPower = argsLevel * 1.1f;

			break;


		case enumEnemyType.lotoSpirit01:
			this.maxHP            = argsLevel * argsLevel * 25f;
			this.nowHP            = this.maxHP;
			this.grantExp         = argsLevel * argsLevel * 1.123f;

			this.attackDeleySec = 3f;
			this.AttackingPower = argsLevel * 2.3f;

			break;


		default:
			this.maxHP            = argsLevel * argsLevel * 0.8f;
			this.nowHP            = this.maxHP;
			this.grantExp         = argsLevel * 10;

			this.attackDeleySec = 3f;
			this.AttackingPower = argsLevel * 1.1f;

			break;
		}

	}
}

public enum enumEnemyType{
	small001
	,piyo
	,rabbitman01
	,largeWolf01
	,Tower01
	,daemon01
	,tamanekoB01
	,daikon01
	,lotoSpirit01
}
