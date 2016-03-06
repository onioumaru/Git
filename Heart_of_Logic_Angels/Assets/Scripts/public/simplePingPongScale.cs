using UnityEngine;
using System.Collections;

public class simplePingPongScale : MonoBehaviour {
	private Coroutine waitTimeCor;

	public float defaultScale = 1f;
	public float pingPongSpeed = 2f;
	public float scalePingPong = 0.05f;
//	public float animationTime = -1f;

	private soundManager_Base sMB;

	void Start(){
		StartCoroutine ( mainLoop() );
	}
		
	IEnumerator mainLoop(){
		//Debug.Log ("start");
		while (true) {
			float tmpf = Mathf.PingPong(Time.time * pingPongSpeed, scalePingPong * 2f) + defaultScale - scalePingPong;
			//tmpf

			Vector3 tmpV = new Vector3(tmpf, tmpf, 1f);
			this.transform.localScale = tmpV;

			//疑似update()
			yield return new WaitForSeconds(0.001f);
		}
	}
}
