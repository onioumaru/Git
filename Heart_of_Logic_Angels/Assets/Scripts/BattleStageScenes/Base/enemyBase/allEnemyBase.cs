using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class allEnemyBase : MonoBehaviour {
	public GameObject _HPBar;

	private enemyHPBarScript hpBar;
	
	private GameObject thisGameManager;
	private GameManagerScript gmS;

	public bool destoryF = false;

	// 死亡時エフェクトのPrefab
	public GameObject removeEnemyPrefab;
	public GameObject bitmapFontBasePrefab;

	private HashSet<int> grantExpChara;
	private typeEnemyStatus thisEnemyStat;
	//private GameObject 

	private soundManager_Base sMB;
	
	public float _defaultLevel = 5f;
	public enumEnemyType _defaultEnemyType = enumEnemyType.small001;

	// Use this for initialization
	void Start() {
		sMB = soundManagerGetter.getManager ();

		grantExpChara = new HashSet<int>();
		thisEnemyStat = new typeEnemyStatus(_defaultLevel, _defaultEnemyType);

		//HP Barの作成
		GameObject tmpHpbarGO = Instantiate (_HPBar) as GameObject;
		tmpHpbarGO.transform.parent = this.transform;

		tmpHpbarGO.transform.localPosition = Vector3.zero;

		//位置補正
		textureVector ttv = new textureVector (this.gameObject);
		Vector3 tmpV = ttv.getBottomOffset_ForCenterPivot(0, -0.05f);
		tmpHpbarGO.transform.localPosition += tmpV;

		hpBar = tmpHpbarGO.GetComponent<enemyHPBarScript>();
		hpBar.setMaxBarLength_argsWidth (ttv.getWidth(), thisEnemyStat.maxHP);

		gmS = GameManagerGetter.getGameManager ();
	}

	public void setThisEnemyStatus(typeEnemyStatus argsVal){
		thisEnemyStat = argsVal;
	}
	
	public float getAttackingPower(){
		return thisEnemyStat.AttackingPower;
	}
	
	public float getAttackingDelay(){
		return thisEnemyStat.attackDeleySec;
	}

	/*
	IEnumerator mainLoop(){

	}
	*/
	
	public float setDamage(int argsInt, int argsCharaIndex){
		//戻り値で経験値を返す
		grantExpChara.Add (argsCharaIndex);

		GameObject retObj = Instantiate (bitmapFontBasePrefab, this.transform.position, this.transform.rotation) as GameObject;
		retObj.GetComponentInChildren<common_damage> ().showDamage (this.transform, argsInt);

		thisEnemyStat.nowHP -= argsInt;

		if (thisEnemyStat.nowHP <= 0) {
			this.destoryF = true;

			gmS.grantExp_forAllChara(grantExpChara, thisEnemyStat.grantExp);
			
			sMB.playOneShotSound(enm_oneShotSound.attack03);

			this.removedMe (this.transform);
			return 0;
		} else {
			sMB.playOneShotSound(enm_oneShotSound.enemyHit);
			hpBar.setHP(thisEnemyStat.nowHP);
			return 0;
		}
	}

	public void removedMe(Transform origin){
		Instantiate (removeEnemyPrefab, origin.position, origin.rotation);
		Destroy (this.gameObject);
	}

	public void setMoving(int argsType, float argsMovingSpeed){
		switch (argsType) {
		case 1:
			// simlpe reft Moving
			
			//this.GetComponent<Rigidbody2D>().isKinematic = true;
			this.GetComponent<Rigidbody2D>().velocity = Vector2.right * argsMovingSpeed * -1f;

			break;
		case 2:
			break;
			
		case 3:
			break;

		default:
			break;
		}
	}
}

