using UnityEngine;
using System.Collections;

public class stage_dragErea : MonoBehaviour {

	private Camera mainCamera;
	
	private Vector3 firstMouseDown;
	//private Vector3 firstCameraPosi;
	private bool mouseDown = false;

	private Vector2 lastVelocity;
	private GameObject cameraTrackingObj;

	void Start(){
		mainCamera = Camera.main;
		cameraTrackingObj = GameObject.Find ("CameraTracker");
	}

	void OnMouseDown(){
		if (Input.GetMouseButtonDown (0)) {
			firstMouseDown = mainCamera.ScreenToWorldPoint (Input.mousePosition);
			//firstCameraPosi = mainCamera.transform.position;
			mouseDown = true;

			cameraTrackingObj.GetComponentInChildren<cameraTrackerScript>().setCharaTrackReset();
		}
	}

	void OnMouseDrag(){
		if (mouseDown == true) {
			Vector3 tapPoint = firstMouseDown - mainCamera.ScreenToWorldPoint(Input.mousePosition);

			//mainCamera.transform.position = firstCameraPosi + tapPoint;
			
			mainCamera.transform.rigidbody2D.velocity= tapPoint * 10f;
		}
	}
	
	void OnMouseUp(){
		mouseDown = false;

		StartCoroutine(this.inertiaMove());
	}

	IEnumerator inertiaMove(){
		//Debug.Log("Loop Start : " + Time.time);
		while (true) {
			if (mainCamera.transform.rigidbody2D.velocity.magnitude > 0.5f) {
				Vector2 tmpV2 = mainCamera.transform.rigidbody2D.velocity;

				mainCamera.transform.rigidbody2D.velocity = new Vector2(tmpV2.x * 0.9F, tmpV2.y * 0.9F);
			} else {
				//stop
				Vector3 tmpZero = new Vector3(0f, 0f, 0f);
				mainCamera.transform.rigidbody2D.velocity = tmpZero;
				break;
			}

			yield return new WaitForSeconds(0.033f);
		}
		//Debug.Log("Loop Done");
	}
}


