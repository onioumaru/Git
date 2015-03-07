using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Rigidbody2Dコンポーネントを必須にする
[RequireComponent(typeof(Rigidbody2D))]
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
	
	// Use this for initialization
	void Start() {
		grantExpChara = new HashSet<int>();
		thisEnemyStat = new typeEnemyStatus(100f, 100f,2f,1f);

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

		StartCoroutine (checkGameManager ());
	}

	IEnumerator checkGameManager(){
		//Findコマンドはなるべくやりたくないので、
		//0.1秒後に確認する
		yield return new WaitForSeconds (0.1f);

		if (thisGameManager != null) {
			yield break;
		}

		setGameManager (GameObject.Find ("GameManager"));
	}

	public void setGameManager(GameObject argsGM){
		thisGameManager = argsGM;
		
		gmS = thisGameManager.GetComponent<GameManagerScript> ();
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

		Debug.Log (thisEnemyStat.nowHP);

		if (thisEnemyStat.nowHP <= 0) {
			this.destoryF = true;

			gmS.grantExp_forAllChara(grantExpChara, thisEnemyStat.grantExp);

			this.removedMe (this.transform);
			return 0;
		} else {
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
			Vector2 tmpV = new Vector2(-1,0);

			this.GetComponent<Rigidbody2D>().velocity = tmpV.normalized * argsMovingSpeed;

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

public class typeEnemyStatus{
	public float maxHP;
	public float nowHP;
	public float grantExp;
	public float attackDeleySec;
	public float multiAttackCount;

	public typeEnemyStatus(float argsmaxHp,float argsgrantExp,float argsatkDeley,float argsmltAttack){
		this.maxHP            = argsmaxHp;
		this.nowHP            = this.maxHP;
		this.grantExp         = argsgrantExp;
		this.attackDeleySec   = argsatkDeley;
		this.multiAttackCount = argsmltAttack;
	}
}
