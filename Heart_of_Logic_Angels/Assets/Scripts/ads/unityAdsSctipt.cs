using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class unityAdsSctipt : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// ゲームIDを入力して、Unity Adsを初期化する
		Advertisement.Initialize ("1045709");

		StartCoroutine (visibleWait() );
	}

	IEnumerator visibleWait(){
		yield return new WaitForSeconds (1.5f);

		while (true) {
			// Unity Adsを表示する準備ができているか確認する
			if (Advertisement.isReady ()) {
				// Unity Adsを表示する
				Advertisement.Show ();

				break;
			} else {
				Debug.Log ("not ready");
			}

			yield return new WaitForSeconds (0.5f);
		}


		while (true) {
			if (Advertisement.isShowing == false) {
				break;
			}
			yield return new WaitForSeconds (0.2f);
		}

		SceneManager.LoadSceneAsync ("title");

	}
}