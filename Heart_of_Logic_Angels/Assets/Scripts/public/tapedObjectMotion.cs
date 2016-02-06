using UnityEngine;
using System.Collections;

public class tapedObjectMotion : MonoBehaviour {
	private Coroutine waitTimeCor;
	private float pingPongSpeed = 2f;

	public float defaultScale = 1f;
	public float scalePingPong = 0.05f;
	
	public bool _soundFlag = true;
	public bool _cancelButtonFlag = false;

	private soundManager_Base sMB;

	void Start(){
		sMB = soundManagerGetter.getManager ();
	}


	public void actionTapEffect(){
		if (waitTimeCor != null) {
			StopCoroutine(waitTimeCor);
			waitTimeCor=null;
				}
		if (_soundFlag) {
			if (_cancelButtonFlag) {
				sMB.playOneShotSound (enm_oneShotSound.closeButton);
			} else {
				sMB.playOneShotSound (enm_oneShotSound.nomalButton);
			}
		}

		waitTimeCor = StartCoroutine ( pushThis () );
	}


	IEnumerator pushThis(){
		//Debug.Log ("start");
		for (int i = 0; i < 5; i++) {
			float tmpf = Mathf.PingPong(Time.time * pingPongSpeed, scalePingPong * 2f) + defaultScale - scalePingPong;
			//tmpf

			Vector3 tmpV = new Vector3(tmpf, tmpf, 1f);
			this.transform.localScale = tmpV;

			yield return new WaitForSeconds(0.01f);
		}

		
		Vector3 resetV3 = new Vector3(defaultScale, defaultScale, 1f);
		this.transform.localScale = resetV3;
		//Debug.Log ("end");
		waitTimeCor = null;
	}
}
