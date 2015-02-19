using UnityEngine;
using System.Collections;

public class test_Regeneiter : MonoBehaviour {

	public GameObject mobEnemy;

	bool boundFlag = false;
	int cntI = 0;

	// Use this for initialization
	IEnumerator Start () {
		while (true) {
			GameObject retMobs = Instantiate (mobEnemy, this.transform.position, this.transform.rotation) as GameObject;

			retMobs.GetComponentInChildren<allEnemy>().setMoving(1, 0.5f);

			boundFlag = true ;

			yield return new WaitForSeconds (0.3f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (boundFlag == true) {
			//float tmpY = 0.2f + Mathf.PingPong (Time.time, 0.02f);
			float tmpY = 0.2f;
			float tmpX = 0.2f;

			if (cntI < 5){
				tmpX = 0.2f + cntI * 0.005f;
				tmpY = 0.2f - cntI * 0.005f;
			}else {
				tmpX = 0.2f + 0.025f - cntI * 0.005f;
				tmpY = 0.2f - 0.025f + cntI * 0.005f;
			}

			Vector3 tmpV = new Vector3 (tmpX, tmpY);
			
			transform.localScale = tmpV;

			cntI +=1;
			if (cntI > 9){
				cntI = 0;
				boundFlag = false;
				transform.localScale = new Vector3 (0.2f, 0.2f);
			}
		}
	}
}
