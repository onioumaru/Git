using UnityEngine;
using System.Collections;

public class chara_attackEreaVisible : MonoBehaviour {
	private SpriteRenderer thisSR;

	// Use this for initialization
	void Start () {
		thisSR = this.gameObject.GetComponent<SpriteRenderer> ();

		StartCoroutine (fageOutThis());
	}

	IEnumerator fageOutThis(){
		while (true) {
			if (thisSR.color.a > 0) {
				Color tmpC = thisSR.color;
				tmpC.a -= 0.01f;
				thisSR.color = tmpC;
			}

			yield return new WaitForSeconds(0.1f);
		}
	}

	public void setVisibleThisCicle(){
		Color tmpC = thisSR.color;
		tmpC.a = 0.3f;

		thisSR.color = tmpC;
	}
}
