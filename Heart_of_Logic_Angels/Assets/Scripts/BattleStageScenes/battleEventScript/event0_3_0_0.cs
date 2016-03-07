using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class event0_3_0_0 : MonoBehaviour {
	private GameObject _talkPartPerefab;
	private GameObject _missionTargetPrefab;
	private GameObject _battleStartCaption;
	private GameObject _stageClearCaption;

	private GameManagerScript GMS;

	public GameObject[] _clearTargetEnemy;

	private List<GameObject> generatedTargetEnemys;

	public float _defaultLevel;

	// Use this for initialization
	void Start () {
		soundManagerGetter.getManager ().playBGM (1);

		generatedTargetEnemys = new List<GameObject>();
		//最初の1匹
		this.clearTargetGenerater (_clearTargetEnemy [0], new Vector3(3.5f, -0.5f, 0f) );
		this.clearTargetGenerater (_clearTargetEnemy [0], new Vector3(-6.5f, -0.5f, 0f) );
		
		this.clearTargetGenerater (_clearTargetEnemy [1], new Vector3(-1.415f, -0.911f, 0f) );
		this.clearTargetGenerater (_clearTargetEnemy [1], new Vector3(-7.4f, 0f, 0f) );

		GMS = GameManagerGetter.getGameManager ();
		GMS.setAllCollider2DEnabale (false);

		Time.timeScale = 0f;

		//talkingParts/talkingMain
		string tmpPath = "Prefabs/talkingParts/talkingMain";
		_talkPartPerefab= Resources.Load(tmpPath) as GameObject;
		//missionTargetCanvas
		tmpPath = "Prefabs/missonTargetCaption/missionTargetCanvas";
		_missionTargetPrefab= Resources.Load(tmpPath) as GameObject;
		//battleStartCaption
		tmpPath = "Prefabs/missonTargetCaption/battleStartCaption";
		_battleStartCaption= Resources.Load(tmpPath) as GameObject;
		//battleClearCaption
		tmpPath = "Prefabs/missonTargetCaption/battleClearCaption";
		_stageClearCaption= Resources.Load(tmpPath) as GameObject;


		this.setDefaultCharaStartPosition ();


		Time.timeScale = 1f;
		GMS.setAllCollider2DEnabale (true);

		StartCoroutine ( startTargetCall() );

		StartCoroutine ( timeEvent() );
	}

	private void setDefaultCharaStartPosition(){
		allCharaBase[] tmpBases = GameObject.FindObjectsOfType<allCharaBase> ();

		foreach (allCharaBase tmpGO in tmpBases) {
			tmpGO.thisCharaFlag.transform.position = tmpGO.transform.position;
		}
	}

	private void clearTargetGenerater(GameObject argsGO, Vector3 argsPosition){

		GameObject tmpGO = (GameObject)Instantiate (argsGO, argsPosition, Quaternion.identity);
		generatedTargetEnemys.Add (tmpGO);

		allEnemyBase tmpBase = tmpGO.GetComponent<allEnemyBase> ();
		tmpBase._defaultLevel = _defaultLevel;
	}

	private bool checkTargetEnemyAlive(){
		for (int loopI = 0; loopI < generatedTargetEnemys.Count; loopI++) {
			if (generatedTargetEnemys[loopI] != null){
				//破壊されていない奴がいる場合
				return true;
			}
		}
		return false;
	}


	IEnumerator timeEvent(){
		// 時間経過 系
		yield return new WaitForSeconds (30f);

		//GMS.talkingPartLoader ("0-3-0-2");

		while (true) {
			yield return new WaitForSeconds (2f);

			if (this.checkTargetEnemyAlive() == false){
				//すべて破壊確認
				//wave 1
				Debug.Log("wave 1 clear");
				break;
			}
		}
		
		yield return new WaitForSeconds (0.2f);
		StartCoroutine ( stageClear() );
	}
	



	void Update(){
		if (Input.GetKeyDown (KeyCode.Z)) {
			//debuf
			//this.OnTriggerStay2D(null);
		}
	}

	void OnTriggerStay2D(Collider2D argsCo){
		//Debug.Log ("Enter Collider");

		//このコライダー停止
		Destroy (this.gameObject.GetComponent<BoxCollider2D> ());  //.enabled = false;
		
		GMS.talkingPartLoader ("0-3-0-3");

	}




	private IEnumerator stageClear(){
		staticValueManagerS sVMS = staticValueManagerGetter.getManager ();

		GMS.talkingPartLoader ("0-3-0-4");

		//引き続きコライダーは停止
		GMS.setAllCollider2DEnabale (false);

		yield return new WaitForSeconds (0.2f);

		Time.timeScale = 1;	//パーティクルを使うため1にする

		GameObject tmpGO = (GameObject)Instantiate (_stageClearCaption);
		while (tmpGO != null) {
			yield return null;
				}

		sceneChangeValue sceneCV = sVMS.getNowSceneChangeValue ();

		GMS.saveBattleResultValues ();

		//0-3-0-5
		sVMS.setStoryProgress("0-3-0-5");
		sVMS.changeScene (sceneChangeStatusEnum.gotoTalkScene);

	}



	
	IEnumerator startTargetCall(){
		float movingFlameSec = 3f;
		float targetX = -20f;
		float targetY = 0.1f;
		float pauseFlameSec = 2f;

		//カメラの移動
		Camera.main.transform.position = new Vector3 (11f, 0f, -20f);
		GMS.setAllCollider2DEnabale (false);

		//開始会話
		staticValueManagerS sVMS = staticValueManagerGetter.getManager ();
		sVMS.addStoryProgresses (enum_StoryProgressType.Step);
		sVMS.getNowSceneChangeValue().sceneFileName = "0-3-0-1";

		GameObject tmpTalkObj = (GameObject)Instantiate (_talkPartPerefab);
		// トークシーンが破壊されるまでループして待つ
		while (tmpTalkObj != null) {
			yield return null;
		}

		Time.timeScale = 0f;

		//作戦目標
		missionTargetTitleS _mTTS;
		GameObject missionTargetCanvas = (GameObject)Instantiate (_missionTargetPrefab);
		_mTTS = missionTargetCanvas.GetComponent<missionTargetTitleS> ();
		
		_mTTS._winDecision.text = "初期配置の敵を殲滅せよ";
		_mTTS._loseDecision.text = "部隊の全滅";

		float tmpX;
		float tmpY;

		float tmpPassedSec = 0f;


		//最初のウェイト
		tmpPassedSec = 0f;
		while(tmpPassedSec < 1f){
			yield return null;
			
			tmpPassedSec += Time.fixedDeltaTime;
		}


		//目標地点までの移動
		tmpPassedSec = 0f;
		while(tmpPassedSec < movingFlameSec){
			yield return null;

			tmpPassedSec += Time.fixedDeltaTime;
			
			tmpX = (targetX / movingFlameSec) * Time.fixedDeltaTime;
			tmpY = (targetY / movingFlameSec) * Time.fixedDeltaTime;

			tmpX += Camera.main.transform.position.x;
			tmpY += Camera.main.transform.position.y;

			Camera.main.transform.position = new Vector3(tmpX, tmpY, -20f);
		}

		//　作戦目標を見せるWait
		//_mTTS.startArrowMotion (pauseFlameSec);


		/*
		tmpPassedSec = 0f;
		while(tmpPassedSec < pauseFlameSec){
			yield return null;
			
			tmpPassedSec += Time.fixedDeltaTime;
		}
		*/


		float returnTime = 3f;

		//スタート位置にカメラを戻す
		tmpPassedSec = 0f;
		while(tmpPassedSec < (movingFlameSec / returnTime)){
			yield return null;
			
			tmpPassedSec += Time.fixedDeltaTime;
			
			tmpX = (targetX / movingFlameSec) * Time.fixedDeltaTime * -1f * returnTime;
			tmpY = (targetY / movingFlameSec) * Time.fixedDeltaTime * -1f * returnTime;
			
			tmpX += Camera.main.transform.position.x;
			tmpY += Camera.main.transform.position.y;
			
			Camera.main.transform.position = new Vector3(tmpX, tmpY, -20f);
		}

		//パーティクルを使うので時間開始
		Time.timeScale = 1f;

		GameObject tmpGO = (GameObject) Instantiate (_battleStartCaption);
		Destroy ( missionTargetCanvas );

		// スタートキャプションが消えるまで待つ
		while (tmpGO != null) {
			// トークシーンが破壊されるまでループして待つ
			yield return null;
		}

		GMS.setAllCollider2DEnabale (true);
	}
}
