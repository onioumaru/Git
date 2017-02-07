using UnityEngine;
using System.Collections;

public class chara_attackEreaVisible : MonoBehaviour {

	private float defaultAlfa = 1f;
	private float defaultAlfa_smooth = 0.05f;

	private SpriteRenderer thisSR;

	// Use this for initialization
	void OnEnable () {
		thisSR = this.gameObject.GetComponent<SpriteRenderer> ();

		this.setVisibleThisCicle (0.1f);

		StartCoroutine (fageOutThis());
	}

	IEnumerator fageOutThis(){
		while (true) {
			if (thisSR.color.a > 0) {
				Color tmpC = thisSR.color;
				tmpC.a -= defaultAlfa_smooth;
				thisSR.color = tmpC;
			}

			yield return new WaitForSeconds(0.1f);
		}
	}

	public void setVisibleThisCicle(){
		Color tmpC = thisSR.color;
		tmpC.a = defaultAlfa;

		thisSR.color = tmpC;
	}

	public void setVisibleThisCicle(float alf){
		Color tmpC = thisSR.color;
		tmpC.a = alf;

		thisSR.color = tmpC;
	}
}
