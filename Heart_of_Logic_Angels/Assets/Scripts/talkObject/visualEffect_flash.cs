using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class visualEffect_flash : MonoBehaviour {
	public float loop_Cnt = 2f;
	public float _fadeTime = 0.2f;
	public float _blackOutTime = 0.2f;

	private bool _fadeInFlag = false;

	private float cntOutSec = 0f;
	private float cntInSec = 0f;

	private Image thisImage;

	private float nowLoopCnt = 0;

	// Use this for initialization
	void Start () {
		thisImage = this.GetComponent<Image>();
	}

	// Update is called once per frame
	void Update () {

		if (nowLoopCnt < loop_Cnt) {
			this.singleLoop ();
		} else {
			GameObject.Destroy(this.gameObject);
		}
	}

	private void singleLoop(){
		if (_fadeInFlag == false) {
			cntOutSec += Time.fixedDeltaTime;

			float tmpA =  1 - ((_fadeTime - cntOutSec) / _fadeTime);

			thisImage.color = new Color (1f, 1f, 1f, tmpA);

			if (cntOutSec > (_blackOutTime + _fadeTime)) {
				//フェードタイムとブラックアウト時間が済んだら
				//フラグ切り替え
				_fadeInFlag = true;
			}

		} else {
			cntInSec +=  Time.fixedDeltaTime;

			float tmpA2 =  ((_fadeTime - cntInSec) / _fadeTime);

			if (tmpA2 <= 0) {
				//reset
				nowLoopCnt++;
				_fadeInFlag = false;
				cntOutSec = 0;
			} else {
				thisImage.color = new Color (1f, 1f, 1f, tmpA2);
			}
		}
	}

	public void setDefaultSet(float fageTime,float blackOutTime, bool fadeInFlag){
		_fadeTime = fageTime;
		_blackOutTime = blackOutTime;
		_fadeInFlag = fadeInFlag;
	}
}
