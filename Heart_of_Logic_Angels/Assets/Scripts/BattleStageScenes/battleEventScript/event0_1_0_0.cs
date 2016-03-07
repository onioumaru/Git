using UnityEngine;
using System.Collections;

public class event0_1_0_0 : MonoBehaviour {
	public GameObject _talkPartPerefab;
	public GameObject _missionTargetPrefab;

	public GameObject _battleStartCaption;
	public GameObject _stageClearCaption;

	private GameManagerScript GMS;


	// Use this for initialization
	void Start () {

		GMS = GameManagerGetter.getGameManager ();
		GMS.setAllCollider2DEnabale (false);
		
		soundManagerGetter.getManager ().playBGM (1);

		Time.timeScale = 0f;

		StartCoroutine ( startTargetCall() );

		StartCoroutine ( timeEvent() );
	}

	/*
	 * 
	void Update(){
		if (Input.GetKeyDown (KeyCode.Z)) {
			//debuf
			this.OnTriggerStay2D(null);
				}
	}
*/

	IEnumerator timeEvent(){
		// 時間経過 系

		yield return new WaitForSeconds (5f);
		
		GMS.talkingPartLoader ("0-1-0-1");

		yield return new WaitForSeconds (10f);
		
		GMS.talkingPartLoader ("0-1-0-2");

	}

	void OnTriggerStay2D(Collider2D argsCo){
		//Debug.Log ("Enter Collider");
		
		Time.timeScale = 0f;
		//コライダーはこの時点で全て停止・この時点で止めないと複数イベント発生する
		GMS.setAllCollider2DEnabale (false);

		StartCoroutine ( stageClear() );
	}

	private IEnumerator stageClear(){
		staticValueManagerS sVMS = staticValueManagerGetter.getManager ();

		/*
		//会話表示
		sVMS.getNowSceneChangeValue().sceneFileName = "0-1-0-3";
		GameObject tmpGO = (GameObject)Instantiate (_talkPartPerefab);

		// トークシーンが破壊されるまでループして待つ
		while (tmpGO != null) {
			yield return null;
		}

		//引き続きコライダーは停止
		GMS.setAllCollider2DEnabale (false);

		*/

		GameObject tmpGO ;

		Time.timeScale = 1;	//パーティクルを使うため1にする

		tmpGO = (GameObject)Instantiate (_stageClearCaption);
		while (tmpGO != null) {
			yield return null;
				}

		sceneChangeValue sceneCV = sVMS.getNowSceneChangeValue ();

		//例外差分修正
		sVMS.addStoryProgresses (enum_StoryProgressType.Step);

		//Debug.Log ("gotoStageSelect");
		sVMS.changeScene (sceneChangeStatusEnum.gotoTalkScene);

	}

	
	IEnumerator startTargetCall(){
		float movingFlameSec = 1f;
		float targetX = this.transform.localPosition.x;
		float targetY = this.transform.localPosition.y + 0.5f;
		float pauseFlameSec = 2f;
		
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
