using UnityEngine;
using System.Collections;

public class cameraTrackerScript : MonoBehaviour {
	public SpriteRenderer[] _emptyWaku;

	private Camera trackedCamera;
	private float posiZ;

	private bool charaTracking = false;
	private GameObject trackingChara;

	// Use this for initialization
	void Start () {
		trackedCamera = Camera.main;
		posiZ = this.gameObject.transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		if (charaTracking == true) {
			Vector3 tmpV = new Vector3 (trackingChara.transform.position.x, trackingChara.transform.position.y, -10f);
			Camera.main.transform.position = tmpV;
		}

		this.transform.position = new Vector3 (trackedCamera.transform.position.x, trackedCamera.transform.position.y, posiZ);
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
