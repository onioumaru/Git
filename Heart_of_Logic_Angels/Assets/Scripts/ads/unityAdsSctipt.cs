using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class unityAdsSctipt : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// ゲームIDを入力して、Unity Adsを初期化する
		Advertisement.Initialize ("1045709");

		StartCoroutine (visibleWait() );
	}

	IEnumerator visibleWait(){
		yield return new WaitForSeconds (5f);

		// Unity Adsを表示する準備ができているか確認する
		if (Advertisement.isReady ()) {
			// Unity Adsを表示する
			Advertisement.Show ();
		} else {
			Debug.Log ("not ready");
		}
	}
}