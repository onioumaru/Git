using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class missionTargetTitleS : MonoBehaviour {
	public Text _winDecision;
	public Text _loseDecision;
	public Image _arrowImage;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void startArrowMotion(float argsSec){

		StartCoroutine ( startTargetArrowCall(argsSec) );

	}
	
	IEnumerator startTargetArrowCall(float argsSec){
			float tmpPassedSec = 0f;

			//Y position を０～２０動かす
			_arrowImage.color = Color.white;

			float tmpY = 20f;

			while (tmpPassedSec < argsSec) {
					yield return null;
		
					tmpPassedSec += Time.fixedDeltaTime;

					tmpY -= 2f;
					if (tmpY < 0f) {
							tmpY = 20f;
					}

					_arrowImage.transform.localPosition = new Vector3 (0f, tmpY, 0f);
			}
	
			_arrowImage.color = Color.clear;
	}
}
