using UnityEngine;
using System.Collections;

public class enemyAttackAnimation : MonoBehaviour {
	private float tmpT;

	// Use this for initialization
	void OnEnable() {
		StartCoroutine ( attackAnime() );
	}

	IEnumerator attackAnime(){
		for (int loopI = 0; loopI < 5; loopI++) {
			if (this.transform.localScale.x < 0){
				tmpT = 0.1f;
			} else {
				tmpT = -0.1f;
			}

			if (loopI % 2 == 0){
				this.transform.localPosition = new Vector3(0, 0,0);
			} else {
				this.transform.localPosition = new Vector3(tmpT, 0,0);
			}

			yield return null;
		}

		this.enabled = false;
	}
}
