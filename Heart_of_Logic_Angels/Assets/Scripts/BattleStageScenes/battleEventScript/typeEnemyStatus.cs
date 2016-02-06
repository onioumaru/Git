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
			this.maxHP            = argsLevel * argsLevel * 11f + 200f;
			this.nowHP            = this.maxHP;
			this.grantExp         = argsLevel * 112f;

			this.attackDeleySec = 3f;
			this.AttackingPower = argsLevel * argsLevel / 1.9f;

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
			this.maxHP            = argsLevel * 10f;
			this.nowHP            = this.maxHP;
			this.grantExp         = argsLevel;

			this.attackDeleySec = 1f;
			this.AttackingPower = argsLevel * 1.1f;

			break;


		case enumEnemyType.Tower01:
			//Lv10 で　HP1000
			//Lv20 で　HP4000
			//固い
			this.maxHP            = argsLevel * argsLevel * 10f;
			this.nowHP            = this.maxHP;
			this.grantExp         = argsLevel * argsLevel * 2.2f;

			this.attackDeleySec = 3f;
			this.AttackingPower = argsLevel * 5.5f;

			break;

		case enumEnemyType.daemon01:
			//Lv12 で　316
			//固い&攻撃間隔が長い
			this.maxHP            = argsLevel * argsLevel * 2.2f;
			this.nowHP            = this.maxHP;
			this.grantExp         = argsLevel * argsLevel * 1.123f;

			this.attackDeleySec = 7f;
			this.AttackingPower = argsLevel * 8.79f;

			break;

		default:
			this.maxHP            = argsLevel * 25f;
			this.nowHP            = this.maxHP;
			this.grantExp         = argsLevel * 10;

			this.attackDeleySec = 3f;
			this.AttackingPower = argsLevel * 1f;

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
}
