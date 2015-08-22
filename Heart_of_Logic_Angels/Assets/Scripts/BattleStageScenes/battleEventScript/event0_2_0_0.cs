using UnityEngine;
using System.Collections;

public class event0_2_0_0 : MonoBehaviour {
	private GameObject _talkPartPerefab;
	private GameObject _missionTargetPrefab;
	private GameObject _battleStartCaption;
	private GameObject _stageClearCaption;

	private GameManagerScript GMS;


	// Use this for initialization
	void Start () {
		soundManagerGetter.getManager ().playBGM (1);

		GMS = GameManagerGetter.getGameManager ();
		GMS.setAllCollider2DEnabale (false);

		Time.timeScale = 0f;

		//talkingParts/talkingMain
		string tmpPath = "Prefabs/mtalkingParts/talkingMain";
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

		StartCoroutine ( startTargetCall() );

		StartCoroutine ( timeEvent() );
	}


	IEnumerator timeEvent(){
		// 時間経過 系

		yield return new WaitForSeconds (5f);
		
		//GMS.talkingPartLoader ("0-1-0-1");


	}

	void OnTriggerStay2D(Collider2D argsCo){
		//Debug.Log ("Enter Collider");
	}

	private IEnumerator stageClear(){
		staticValueManagerS sVMS = staticValueManagerGetter.getManager ();

		//会話表示
		sVMS.getNowSceneChangeValue().sceneFileName = "0-1-0-3";
		GameObject tmpGO = (GameObject)Instantiate (_talkPartPerefab);

		// トークシーンが破壊されるまでループして待つ
		while (tmpGO != null) {
			yield return null;
		}

		//引き続きコライダーは停止
		GMS.setAllCollider2DEnabale (false);

		Time.timeScale = 1;	//パーティクルを使うため1にする

		tmpGO = (GameObject)Instantiate (_stageClearCaption);
		while (tmpGO != null) {
			yield return null;
				}

		sceneChangeValue sceneCV = sVMS.getNowSceneChangeValue ();

		//
		sceneCV.addStoryProgress (1);

		//Debug.Log ("gotoStageSelect");
		sVMS.changeScene (sceneChangeStatusEnum.gotoStageSelect);

	}

	
	IEnumerator startTargetCall(){
		float movingFlameSec = 2f;
		float targetX = this.transform.localPosition.x;
		float targetY = this.transform.localPosition.y + 0.5f;
		float pauseFlameSec = 4f;
		
		missionTargetTitleS _mTTS;
		GameObject missionTargetCanvas = (GameObject)Instantiate (_missionTargetPrefab);
		_mTTS = missionTargetCanvas.GetComponent<missionTargetTitleS> ();

		float tmpX;
		float tmpY;

		float tmpPassedSec = 0f;


		
		tmpPassedSec = 0f;
		while(tmpPassedSec < 1f){
			yield return null;
			
			tmpPassedSec += Time.fixedDeltaTime;
		}
		
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


		//　pause
		_mTTS.startArrowMotion (pauseFlameSec);

		tmpPassedSec = 0f;
		while(tmpPassedSec < pauseFlameSec){
			yield return null;
			
			tmpPassedSec += Time.fixedDeltaTime;
		}



		//スタート位置にカメラを戻す
		tmpPassedSec = 0f;
		while(tmpPassedSec < (movingFlameSec / 2)){
			yield return null;
			
			tmpPassedSec += Time.fixedDeltaTime;
			
			tmpX = (targetX / movingFlameSec) * Time.fixedDeltaTime * -2f;
			tmpY = (targetY / movingFlameSec) * Time.fixedDeltaTime * -2f;
			
			tmpX += Camera.main.transform.position.x;
			tmpY += Camera.main.transform.position.y;
			
			Camera.main.transform.position = new Vector3(tmpX, tmpY, -20f);
		}

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
