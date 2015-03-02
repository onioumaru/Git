using UnityEngine;
using System.Collections;

// Rigidbody2Dコンポーネントを必須にするExp
[RequireComponent(typeof(Rigidbody2D))]
public class allCharaBase : MonoBehaviour {

	//以下、開始時にSetが必要
	// 死亡時エフェクトのPrefab
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
	public GameManagerScript gmScript;
	public charaUserStatus thisChara;
	public retTypeExp calcdExp;
	
	//スクリプト起動時はTrue（停止）にしておく事
	public bool stopFlag;

	//private
	private cameraTrackerScript cmrTracker;
	private Animator thisAnimeter;
	private GameObject thisAnimeGO;

	private AudioSource thisAudio;
	private SpriteRenderer thisAtkCircle;
	private GameObject thisAttackErea;

	private skillTargetInfo thisSkillTatgetInfo;


	//この値が0になるまで、硬直時間とする
	public bool movingFreezeFlag = false;
	
	float offsetX = -0.28f;
	float offsetY = 0f;

	
	// Use this for initialization
	void Start () {
		gmScript = GameObject.Find("GameManager").GetComponentInChildren<GameManagerScript>();
		cmrTracker = GameObject.Find ("CameraTracker").GetComponentInChildren<cameraTrackerScript> ();
		
		thisAnimeGO = this.transform.Find("charaAnime").gameObject;
		thisAnimeter = thisAnimeGO.gameObject.GetComponentInChildren<Animator>();

		thisAudio = this.gameObject.GetComponentInChildren<AudioSource>();
		thisAtkCircle = this.transform.Find("atkCircle").gameObject.GetComponentInChildren<SpriteRenderer> ();
		thisAttackErea = this.transform.Find ("AttackErea").gameObject;
		//this.gameObject.Find("")
		
		StartCoroutine (mainLoop());
		StartCoroutine (calcCoolTimeLoop ());
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
			yield return new WaitForSeconds (1f / 30f);

			if (stopFlag == true) {
					Vector2 tmpVct = new Vector2 (0, 0);
					rigidbody2D.velocity = tmpVct;

			} else {
					if (movingFreezeFlag == true) {
							//停止
							Vector2 tmpVct = new Vector2 (0, 0);
							rigidbody2D.velocity = tmpVct;
	
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
									rigidbody2D.velocity = tmpV.normalized * thisChara.battleStatus.thisInfo.movingSpeedMagn;
							}
					}
			} 
		}
	}

	//
	//ダメージ処理
	public void setDamage(int argsInt){
		GameObject retObj = Instantiate (bitmapFontBasePrefab, this.transform.position, this.transform.rotation) as GameObject;

		int damage = argsInt;
		if (thisChara.flyingFlag == true) {
			damage = Mathf.FloorToInt(damage * thisChara.flyingDefMagn);
		}

		retObj.GetComponentInChildren<common_damage> ().showDamage (this.transform, damage);
		thisAnimeter.SetTrigger ("gotoDamage");

		thisChara.nowHP -= damage;
		
		if (thisChara.nowHP <= 0) {
			this.removedMe(this.transform);
		}
	}
	
	//死亡時処理
	public void removedMe(Transform origin){
		//カメラの追従はリセット
		cmrTracker.setCharaTrackReset();

		Instantiate (dyingAnimation, origin.position, origin.rotation);

		//remove処理は、マネージャーで行う
		gmScript.destoryChara(thisCharaIndex);
	}

	//
	//Exp
	//
	public void getExp(float argsExp){
		thisChara.totalExp += argsExp;

		this.calcdExp = gmScript.calcLv (thisChara.totalExp);
		
		if(thisChara.nowLv != this.calcdExp.Lv){
			//
			thisChara.resetMaxParameter();
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
		float retDm = (10 + (thisChara.nowLv * thisChara.nowLv * 0.1f) + 3 * thisChara.nowLv) * thisChara.battleStatus.thisInfo.attackMagn;

		if (thisChara.flyingFlag == true) {
			retDm = retDm * thisChara.flyingAtkMagn ;
		}

		return retDm;
	}

	public void setMode(chatacterMode argsMode){
		thisChara.setMode(argsMode);

		CircleCollider2D tmpCC2D = thisAttackErea.GetComponent<CircleCollider2D> ();
		tmpCC2D.radius = thisChara.battleStatus.thisInfo.attackRange;

		float tmpAR = thisChara.battleStatus.thisInfo.attackRange * 0.8f;
		Vector3 tmpVct = new Vector3 (tmpAR, tmpAR, 1f);

		thisAtkCircle.gameObject.transform.localScale = tmpVct;

		//サークルの表示
		this.setAttackCycleShow ();

		switch(argsMode){
		case chatacterMode.Attack:
			thisAtkCircle.sprite = attackCycle_red;
			break;
		case chatacterMode.Defence:
			thisAtkCircle.sprite = attackCycle_blue;
			break;
		case chatacterMode.Move:
			thisAtkCircle.sprite = attackCycle_green;
			break;
		case chatacterMode.Skill:
			//
			charaSkill_Creater tmpScr = _charaSkillCreater.GetComponent<charaSkill_Creater> ();
			tmpScr.instantiateSkillEffect (this.transform, thisSkillTatgetInfo);
			thisChara.restSkillCoolTime = thisChara.MaxSkillCoolTime;

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

	public void setHealing(int argsVal){
		thisChara.nowHP += argsVal;

		if (thisChara.nowHP > thisChara.maxHP) {
			thisChara.nowHP = thisChara.maxHP;
		}
	}

	public void setFlying(bool argsVal){
		thisChara.flyingFlag = argsVal;
		}

	public void setSkillTatgetInfo(skillTargetInfo argsSkillTgtInfo){
		this.thisSkillTatgetInfo = argsSkillTgtInfo;
	}

	public void setBeforeCharaMode(){
		this.setMode (this.thisChara.battleStatus.beforeCharaMode);
	}

	public float getRestCoolTime(){
		float retVal = 0.01f;

		if (thisChara.restSkillCoolTime > 0) {
			retVal =  thisChara.restSkillCoolTime / thisChara.MaxSkillCoolTime;
		}
		return retVal;
	}
}
