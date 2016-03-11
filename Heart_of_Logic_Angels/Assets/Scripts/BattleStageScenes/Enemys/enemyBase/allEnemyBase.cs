using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//マイフレーム wakeupするのに必要
[RequireComponent(typeof(Rigidbody2D))]
public class allEnemyBase : MonoBehaviour {
	public GameObject _HPBar;

	private enemyHPBarScript hpBar;
	
	private GameObject thisGameManager;
	private GameManagerScript gmS;

	[System.NonSerialized]
	public bool destoryF = false;

	// 死亡時エフェクトのPrefab
	public GameObject removeEnemyPrefab;
	public GameObject bitmapFontBasePrefab;

	private HashSet<int> grantExpChara;
	private typeEnemyStatus thisEnemyStat;
	//private GameObject 

	private soundManager_Base sMB;
	
	public float _defaultLevel;
	public enumEnemyType _defaultEnemyType = enumEnemyType.small001;

	public Sprite _enemyShadow;
	private Rigidbody2D thisRigiBody2D;

	[System.NonSerialized]
	public bool charaFindFlag = false;

	private float defenceMagnification = 1f;
	private float attackMagnification = 1f;

	public bool animeScalingFlag = true;

	private staticValueManagerS sVMS;


	// Use this for initialization
	void Start() {
		thisRigiBody2D = this.GetComponent<Rigidbody2D> ();
		sMB = soundManagerGetter.getManager ();
		sVMS = staticValueManagerGetter.getManager ();

		grantExpChara = new HashSet<int>();
		thisEnemyStat = new typeEnemyStatus(_defaultLevel, _defaultEnemyType);

		this.createHPBar ();

		//影の作成
		//重いので一時的に停止
		if (_enemyShadow != null && sVMS.getRenderingShadowFlag()) {
			//影がセットされているときは表示
			GameObject tmpEnemyShadow = new GameObject ("enemyShadow");
			tmpEnemyShadow.AddComponent<SpriteRenderer> ();
			tmpEnemyShadow.GetComponent<SpriteRenderer> ().sprite = _enemyShadow;
			tmpEnemyShadow.GetComponent<SpriteRenderer> ().color = new Color(1f, 1f, 1f, 0.3f);

			tmpEnemyShadow.transform.parent = this.transform;
			tmpEnemyShadow.transform.localPosition = Vector3.zero;

			textureVector ttv = new textureVector (this.gameObject);
			Vector3 tmpThisWidth = ttv.getBottomOffset_ForCenterPivot(0f, 0.05f, animeScalingFlag);
			tmpEnemyShadow.transform.localPosition += tmpThisWidth;

			//影画像は128
			float tmpScale = ttv.getWidth(animeScalingFlag) / 1.28f;
			tmpEnemyShadow.transform.localScale = new Vector3(tmpScale,tmpScale,1f);

		}

		gmS = GameManagerGetter.getGameManager ();
	}

	public void createHPBar(){
		//HP Barの作成
		if (hpBar != null) {
			Destroy (hpBar.gameObject);
		}

		GameObject tmpHpbarGO = Instantiate (_HPBar) as GameObject;
		tmpHpbarGO.transform.parent = this.transform;

		tmpHpbarGO.transform.localPosition = Vector3.zero;
		//位置補正
		textureVector ttv = new textureVector (this.gameObject);
		Vector3 tmpV = ttv.getBottomOffset_ForCenterPivot(0, -0.05f, animeScalingFlag);
		tmpHpbarGO.transform.localPosition += tmpV;

		hpBar = tmpHpbarGO.GetComponent<enemyHPBarScript>();
		hpBar.setMaxBarLength_argsWidth (ttv.getWidth(animeScalingFlag), thisEnemyStat.maxHP);

	}

	void Update(){
		thisRigiBody2D.WakeUp ();
	}

	public void healEnemy(float healVal){
		thisEnemyStat.nowHP += healVal;

		if (thisEnemyStat.maxHP < thisEnemyStat.nowHP) {
			thisEnemyStat.nowHP = thisEnemyStat.maxHP;
		}
	}

	public void setThisEnemyStatus(typeEnemyStatus argsVal){
		thisEnemyStat = argsVal;
	}
	
	public float getAttackingPower(){
		return thisEnemyStat.AttackingPower * attackMagnification;
	}
	
	public float getAttackingDelay(){
		return thisEnemyStat.attackDeleySec;
	}

	public void setFindFlag(bool argsBool, bool leftFlag){
		GameObject exclamationPrefab = Resources.Load ("Prefabs/charaBase/exclamation") as GameObject;

		charaFindFlag = argsBool;

		GameObject tmpGO = Instantiate( exclamationPrefab) as GameObject;
		tmpGO.transform.position = this.transform.position;

		textureVector ttv = new textureVector (this.gameObject);
		tmpGO.transform.position += new Vector3(0f, ttv.getHeight(animeScalingFlag), 0f);

		tmpGO.transform.parent = this.transform;


		Vector3 tmpV3 = this.transform.Find ("anime").transform.localScale;
		if (leftFlag == true && tmpV3.x < 0) {
			this.transform.Find ("anime").transform.localScale = new Vector3 (tmpV3.x * -1f, tmpV3.y, tmpV3.z);
			//Debug.Log (leftFlag);
		}

		if (leftFlag == false && tmpV3.x > 0) {
			this.transform.Find ("anime").transform.localScale = new Vector3 (tmpV3.x * -1f, tmpV3.y, tmpV3.z);
			//Debug.Log (leftFlag);
		}
	}

	public float setDamage(int argsInt, int argsCharaIndex){
		//戻り値で経験値を返す
		grantExpChara.Add (argsCharaIndex);

		//defenceMagnification

		float tmpVal = argsInt * defenceMagnification;
		int calcVal = Mathf.RoundToInt( tmpVal);

		GameObject retObj = Instantiate (bitmapFontBasePrefab, this.transform.position, this.transform.rotation) as GameObject;
		retObj.GetComponentInChildren<common_damage> ().showDamage (this.transform, calcVal);

		thisEnemyStat.nowHP -= calcVal;

		if (thisEnemyStat.nowHP <= 0) {
			this.destoryF = true;

			gmS.grantExp_forAllChara(grantExpChara, thisEnemyStat.grantExp);
			
			sMB.playOneShotSound(enm_oneShotSound.attack03);

			Instantiate (removeEnemyPrefab, this.transform.position,Quaternion.identity);
			Destroy (this.gameObject);
			return 0;

		} else {
			sMB.playOneShotSound(enm_oneShotSound.enemyHit);
			hpBar.setHP(thisEnemyStat.nowHP);
			return 0;

		}
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

	public void setDefaultLevel(float argsLevel){
		_defaultLevel = argsLevel;
		thisEnemyStat = new typeEnemyStatus(argsLevel, _defaultEnemyType);

		textureVector ttv = new textureVector (this.gameObject);
		hpBar.setMaxBarLength_argsWidth (ttv.getWidth(animeScalingFlag), thisEnemyStat.maxHP);
	}

	public void setAttackDefenceMagnificaion(enemyMagnificationArgsType argsType, float argsVal){

		switch (argsType) {
		case enemyMagnificationArgsType.attackAdd:
			attackMagnification += argsVal;
			break;
		case enemyMagnificationArgsType.defenceAdd:
			defenceMagnification += argsVal;
			break;
		case enemyMagnificationArgsType.attackSet:
			attackMagnification = argsVal;
			break;
		case enemyMagnificationArgsType.defenceSet:
			defenceMagnification = argsVal;
			break;
		}
	}
}

public enum enemyMagnificationArgsType{
	defenceAdd
	,attackAdd
	,defenceSet
	,attackSet
}