using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class visualEffect_fadeInOut : MonoBehaviour {
	private float _fadeTime = 0.5f;
	private float _blackOutTime = 0.5f;
	private bool _fadeInFlag = false;

	private float cntOutSec = 0f;
	private float cntInSec = 0f;

	private Image thisImage;

	// Use this for initialization
	void Start () {
		thisImage = this.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (_fadeInFlag == false) {
			cntOutSec += Time.fixedDeltaTime;

			float tmpA =  1 - ((_fadeTime - cntOutSec) / _fadeTime);

			thisImage.color = new Color (0f, 0f, 0f, tmpA);

			if (cntOutSec > (_blackOutTime + _fadeTime)) {
				//フェードタイムとブラックアウト時間が済んだら
				//フラグ切り替え
				_fadeInFlag = true;
			}

		} else {
			cntInSec +=  Time.fixedDeltaTime;

			float tmpA2 =  ((_fadeTime - cntInSec) / _fadeTime);

			if (tmpA2 <= 0) {
				//最大透明度になったら勝手に死ぬ
				Destroy (this.gameObject);
			} else {
				thisImage.color = new Color (0f, 0f, 0f, tmpA2);
			}
		}
	}

	public void setDefaultSet(float fageTime,float blackOutTime, bool fadeInFlag){
		_fadeTime = fageTime;
		_blackOutTime = blackOutTime;
		_fadeInFlag = fadeInFlag;
	}
}
