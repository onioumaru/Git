using UnityEngine;
using System.Collections;

public class cameraTrackerScript : MonoBehaviour {

	private Camera trackedCamera;

	// Use this for initialization
	void Start () {
		trackedCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3 (trackedCamera.transform.position.x, trackedCamera.transform.position.y, -7f);
	}
}
