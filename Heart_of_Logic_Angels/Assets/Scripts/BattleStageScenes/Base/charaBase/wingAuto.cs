using UnityEngine;
using System.Collections;

public class wingAuto : MonoBehaviour {
	private float angleVal = 0;
	private float angleRvrs = 1;
	private int loopCnt = 0;

	// Use this for initialization
	void OnEnable () {
		if (this.transform.localScale.x < 0) {
			angleRvrs = -1;
				}

		StartCoroutine(wingMosion());
	}
	
	// Update is called once per frame
	void Update () {

	}

	IEnumerator wingMosion(){
		while (true) {
			
			switch (loopCnt) {
			case 0:
				angleVal = 0;
				break;
			case 1:
			//case 2:
			//case 4:
			//case 5:
				angleVal += (20 * angleRvrs);
				break;
			default:
				angleVal -= (2.5f * angleRvrs);
				break;
			}
			
			Vector3 tmpV = new Vector3 (0, 0, 1);
			this.transform.rotation = Quaternion.AngleAxis(angleVal, tmpV);

			loopCnt +=1;
			if (loopCnt >= 13){loopCnt = 0;}

			yield return new WaitForSeconds(0.05f);
				}
	}
}
