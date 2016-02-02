using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class eventMissionFailure : MonoBehaviour {
	public float _waitTime;
	public Image _fadeOutImage;

	private float nowSec = 0f;

	void Start () {
		soundManagerGetter.getManager ().playBGM (9);

		//StartCoroutine ( fadeOutLoop() );
	}

	void FixedUpdate(){
		nowSec += Time.deltaTime;

		float tmpA = 1f - ((_waitTime - nowSec) / _waitTime);
		_fadeOutImage.color = new Color(0,0,0, tmpA);
	}
}
