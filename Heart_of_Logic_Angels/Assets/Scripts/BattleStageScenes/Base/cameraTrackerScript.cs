using UnityEngine;
using System.Collections;

public class cameraTrackerScript : MonoBehaviour {
	public SpriteRenderer[] _emptyWaku;

	private Camera trackedCamera;
	private float posiZ;

	private bool charaTracking = false;
	private GameObject trackingChara;

	private battleStageScript mainBattleStageS;
	private float dragLimitX;
	private float dragLimitY;

	// Use this for initialization
	void Start () {
		trackedCamera = Camera.main;
		posiZ = this.gameObject.transform.position.z;

		mainBattleStageS = GameObject.FindWithTag ("mainBattleStage").GetComponent<battleStageScript>();
		dragLimitX = mainBattleStageS.getDragErea ().x / 2f;
		dragLimitY = mainBattleStageS.getDragErea ().y / 2f;
	}
	
	// Update is called once per frame
	void Update () {
		bool limitFlag = false;
		float tmpX;
		float tmpY;

		if (charaTracking == true) {
			Vector3 tmpV = new Vector3 (trackingChara.transform.position.x, trackingChara.transform.position.y, -20f);
			trackedCamera.transform.position = tmpV;
		}

		// Limit
		if (Mathf.Abs (trackedCamera.transform.position.x) > dragLimitX) {
			//閾値を越している
			limitFlag = true;

			if (trackedCamera.transform.position.x > 0f) {
				tmpX = dragLimitX;
			} else {
				tmpX = dragLimitX * -1f;
			}
		} else {
			tmpX = trackedCamera.transform.position.x;
		}

		if (Mathf.Abs (trackedCamera.transform.position.y) > dragLimitY) {
			//閾値を越している
			limitFlag = true;

			if (trackedCamera.transform.position.y > 0f) {
				tmpY = dragLimitY;
			} else {
				tmpY = dragLimitY * -1f;
			}
		} else {
			tmpY = trackedCamera.transform.position.y;
		}


		this.transform.position = new Vector3 (tmpX, tmpY, posiZ);

		//上限に達しているのでカメラは戻す
		if (limitFlag) {
			trackedCamera.transform.position =  new Vector3 (tmpX, tmpY, -20f);
		}
	}

	public void setCharaTracking(GameObject srgsGO){
		charaTracking = true;
		trackingChara = srgsGO;
	}
	public void setCharaTrackReset(){
		charaTracking = false;
	}

	public SpriteRenderer[] getemptyWaku(){
		return _emptyWaku;
	}
}
