using UnityEngine;
using System.Collections;

// Rigidbody2Dコンポーネントを必須にするExp
[RequireComponent(typeof(Rigidbody2D))]
public class allCharaBase : MonoBehaviour {

	//以下、開始時にSetが必要
	// 死亡時エフェクトのPrefab
	[System.NonSerialized]
	public GameObject thisCharaFlag;

	public int thisCharaIndex;

	//静的に設定済み。Prefabs上でアタッチ
	public GameObject bitmapFontBasePrefab;
	public GameObject dyingAnimation;
	public GameObject _charaSkillCreater;
	
	public Sprite attackCycle_red;
	public Sprite attackCycle_blue;
	public Sprite attackCycle_green;
	
	//以下設定不要、自動Set
	[System.NonSerialized]
	public GameManagerScript gmScript;
	[System.NonSerialized]
	public charaUserStatus thisChara;
	public expLevelInfo calcdExp;
	[System.NonSerialized]
	public bool destroyF = false;
	
	//スクリプト起動時はTrue（停止）にしておく事
	[System.NonSerialized]
	public bool stopFlag;

	//private
	private cameraTrackerScript cmrTracker;
	private Animator thisAnimeter;
	private GameObject thisAnimeGO;

	private AudioSource thisAudio;
	private SpriteRenderer thisAtkCircle;
	private GameObject thisAttackErea;
	
	private skillTargetInfo thisSkillTatgetInfo;

	private soundManager_Base sMB;
	private Rigidbody2D thisRigiBody;

	//この値が0になるまで、硬直時間とする
	[System.NonSerialized]
	public bool movingFreezeFlag = false;
	
	float offsetX = -0.28f;
	float offsetY = 0f;

	private bool thisCharaUnbreakable = false;

	public GameObject _charaStatusIconCtrler;
	private charaStatusIconCtrl charaStatusIconCtrlS; 

	private float regenarateSec = 0;
	private Coroutine reganateCounterCor;

	[SerializeField]
	private float showNowLevel;
	[SerializeField]
	private float showNextExp;


	// Use this for initialization
	void Start () {
		thisRigiBody = this.GetComponent<Rigidbody2D> ();
		sMB = soundManagerGetter.getManager ();

		gmScript = GameObject.Find("GameManager").GetComponentInChildren<GameManagerScript>();
		cmrTracker = GameObject.Find ("CameraTracker").GetComponentInChildren<cameraTrackerScript> ();
		
		thisAnimeGO = this.transform.Find("charaAnime").gameObject;
		thisAnimeter = thisAnimeGO.gameObject.GetComponentInChildren<Animator>();

		thisAudio = this.gameObject.GetComponentInChildren<AudioSource>();
		thisAtkCircle = this.transform.Find("atkCircle").gameObject.GetComponentInChildren<SpriteRenderer> ();
		thisAttackErea = this.transform.Find ("AttackErea").gameObject;
		//this.gameObject.Find("")

		//ステータスアイコン表示のオブジェクトは起動時に作成
		charaStatusIconCtrlS = Instantiate(_charaStatusIconCtrler).GetComponent<charaStatusIconCtrl>();
		charaStatusIconCtrlS.transform.parent = this.transform;

		this.setMode (characterMode.Attack);

		StartCoroutine (mainLoop());
		StartCoroutine (calcCoolTimeLoop ());
	}

	void Update(){
		thisRigiBody.WakeUp ();

		#if DEBUG
			showNowLevel = thisChara.nowLv;
			showNextExp = thisChara.nextExp;
		#endif
	}

	IEnumerator calcCoolTimeLoop(){
		while (true) {
			yield return new WaitForSeconds(0.2f);
			if (thisChara.restSkillCoolTime > 0){
				thisChara.restSkillCoolTime -= 0.2f;
			}
		}
	}

	IEnumerator mainLoop(){
		//内部60f
		while (true) {
			yield return null;//new WaitForSeconds (1f / 61f);

			if (stopFlag == true) {
					Vector2 tmpVct = new Vector2 (0, 0);
					GetComponent<Rigidbody2D>().velocity = tmpVct;

			} else {
					if (movingFreezeFlag == true) {
							//停止
							Vector2 tmpVct = new Vector2 (0, 0);
							GetComponent<Rigidbody2D>().velocity = tmpVct;
	
					} else {
							//移動硬直がない場合
							Vector3 offsetP = new Vector3 (thisCharaFlag.transform.position.x + offsetX, thisCharaFlag.transform.position.y + offsetY);
	
							Vector3 tmpV = (offsetP - transform.position);
	
							if (tmpV.x > 0) {
									Vector3 tmpScl = new Vector3 (-1f, 1f, 1f); 
									thisAnimeGO.transform.localScale = tmpScl;
							} else {
									Vector3 tmpScl = new Vector3 (1f, 1f, 1f); 
									thisAnimeGO.transform.localScale = tmpScl;
							}
	
							if (tmpV.magnitude < 0.02f) {
									stopFlag = true;
							} else {
									GetComponent<Rigidbody2D>().velocity = tmpV.normalized * thisChara.battleStatus.thisInfo.movingSpeedMagn;
							}
					}
			} 
		}
	}


	//
	//ダメージ処理
	public void setDamage(float argsInt){
		GameObject retObj = Instantiate (bitmapFontBasePrefab, this.transform.position, this.transform.rotation) as GameObject;

		//
		//todo : 無敵時の挙動の実装

		float damage = argsInt * thisChara.battleStatus.getDamage_CalCharaMode();
		if (thisChara.flyingFlag == true) {
			damage = damage * thisChara.flyingDefMagn;
		}


		// ここまでにダメージ計算

		retObj.GetComponentInChildren<common_damage> ().showDamage (this.transform, damage);
		thisAnimeter.SetTrigger ("gotoDamage");

		thisChara.nowHP -= damage;
		
		if (thisChara.nowHP < 1) {
			this.destroyF = true;
			this.removedMe (this.transform);
		} else {
			sMB.playOneShotSound(enm_oneShotSound.playerDamage);
		}
	}
	
	//死亡時処理
	public void removedMe(Transform origin){
		//カメラの追従はリセット
		cmrTracker.setCharaTrackReset();

		GameObject tmpGO = Instantiate (dyingAnimation, origin.position, origin.rotation) as GameObject;
		tmpGO.GetComponent<deadEffectParent_Script> ()._defaultC = thisChara.charaNo;

		//remove処理は、マネージャーで行う
		gmScript.destoryChara(thisCharaIndex);
	}

	//
	//Exp
	//
	public void getExp(float argsExp){
		thisChara.totalExp += argsExp;

		this.calcdExp = characterLevelManagerGetter.getManager ().calcLv (thisChara.totalExp);
		
		if(thisChara.nowLv != this.calcdExp.Lv){
			//
			thisChara.initParameter();
			thisChara.nowLv = this.calcdExp.Lv;
			thisAudio.Play();
		}
	}
	//
	// 
	//


	public float getCharaDamage(){
		//基本計算式
		//=10+(A5*A5*0.1)+3*A5

		// 基本計算式 * 飛行適正 * モード倍率
		float retDm = (10 + (thisChara.nowLv * thisChara.nowLv * 0.6f) + 3 * thisChara.nowLv) * thisChara.battleStatus.thisInfo.attackMagn;

		if (thisChara.flyingFlag == true) {
			retDm = retDm * thisChara.flyingAtkMagn ;
		}

		return retDm;
	}

	public void setMode(characterMode argsMode){
		thisChara.setMode(argsMode);

		CircleCollider2D tmpCC2D = thisAttackErea.GetComponent<CircleCollider2D> ();
		tmpCC2D.radius = thisChara.battleStatus.thisInfo.attackRange;

		float tmpAR = thisChara.battleStatus.thisInfo.attackRange * 0.8f;
		Vector3 tmpVct = new Vector3 (tmpAR, tmpAR, 1f);

		thisAtkCircle.gameObject.transform.localScale = tmpVct;

		//サークルの表示
		this.setAttackCycleShow ();

		switch(argsMode){
		case characterMode.Attack:
			thisAtkCircle.sprite = attackCycle_red;
			break;
		case characterMode.Defence:
			thisAtkCircle.sprite = attackCycle_blue;
			break;
		case characterMode.Move:
			thisAtkCircle.sprite = attackCycle_green;
			break;
		case characterMode.Skill:
			//
			charaSkill_Creater tmpScr = _charaSkillCreater.GetComponent<charaSkill_Creater> ();
			tmpScr.instantiateSkillEffect (this.transform, thisSkillTatgetInfo);
			thisChara.restSkillCoolTime = thisChara.MaxSkillCoolTime;

			//攻撃用コライダーは一時停止
			thisAttackErea.GetComponent<CircleCollider2D>().enabled = false;

			break;
		}
	}

	//サークルを表示
	public void setAttackCycleShow(){
		Color tmpC = thisAtkCircle.color;
		tmpC.a = 0.3f;
		thisAtkCircle.color = tmpC;
	}

	//攻撃硬直
	public void setMovingFreeze(){
		float tmpFrzCnt = thisChara.battleStatus.thisInfo.attackFreezeTime;
		movingFreezeFlag = true;
		StartCoroutine(movingFlagClearer(tmpFrzCnt));
	}

	IEnumerator movingFlagClearer(float argsSec){
		yield return new WaitForSeconds(argsSec);
		movingFreezeFlag = false;
	}
	
	public void setHealing(float argsVal){
		thisChara.nowHP += argsVal;
		
		if (thisChara.nowHP > thisChara.maxHP) {
			thisChara.nowHP = thisChara.maxHP;
		}
	}

	public void setUnbreakable_suzuSkill(bool argsBool){
		//無敵かどうか

		//フラグが一緒だったらスルー
		if (thisCharaUnbreakable == argsBool){
			return;
		}

		thisCharaUnbreakable = argsBool;

		if (argsBool) {
			charaStatusIconCtrlS.setIcon (charaStatusIconAdding.unbreakable00);
		} else {
			charaStatusIconCtrlS.removeIcon (charaStatusIconAdding.unbreakable00);
		}
	}

	public void setRegenerate_hozukiSkill(float argsSec){
		//リジェネは瞬間付与なので設定時間でOK
		regenarateSec += argsSec;

		if (reganateCounterCor == null) {
			reganateCounterCor = StartCoroutine (reganarateHozukiSkill () );
		}

		//Debug.Log ("setRegene");
		charaStatusIconCtrlS.setIcon (charaStatusIconAdding.regenarate01);
	}

	IEnumerator reganarateHozukiSkill(){
		//regenarateSec
		float waitCycleSec = 1f;

		do {
			regenarateSec -= waitCycleSec;

			Debug.Log("Healing : " + (this.thisChara.maxHP / 50));
			this.setHealing(this.thisChara.maxHP / 50);

			//チェック間隔
			yield return new WaitForSeconds (waitCycleSec);
		} while (regenarateSec > 0);

		regenarateSec = 0f;
		reganateCounterCor = null;
		charaStatusIconCtrlS.removeIcon (charaStatusIconAdding.regenarate01);
	}


	public void setFlying(bool argsVal){
		thisChara.flyingFlag = argsVal;
		}

	public void setSkillTatgetInfo(skillTargetInfo argsSkillTgtInfo){
		this.thisSkillTatgetInfo = argsSkillTgtInfo;
	}

	public void setBeforeCharaMode(){
		//アタック用コライダーの復活
		thisAttackErea.GetComponent<CircleCollider2D>().enabled = true;

		//スキル使用後、元の状態に戻す
		this.setMode (this.thisChara.battleStatus.beforeCharaMode);

		this.setMovingFreeze_SkillBefore();
	}

	public void setMovingFreeze_SkillBefore(){
		//暫定で固定値で硬直
		float tmpFrzCnt = 2.5f;
		movingFreezeFlag = true;
		StartCoroutine(movingFlagClearer(tmpFrzCnt));
	}


	public float getRestCoolTime(){
		float retVal = 0.01f;

		if (thisChara.restSkillCoolTime > 0) {
			retVal =  thisChara.restSkillCoolTime / thisChara.MaxSkillCoolTime;
		}
		return retVal;
	}
}
