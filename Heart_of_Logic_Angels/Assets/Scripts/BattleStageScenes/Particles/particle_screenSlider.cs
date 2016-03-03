using UnityEngine;
using System.Collections;

public class particle_screenSlider : MonoBehaviour {
	private float sideLimite = 2f;
	private float movingSpeed = 0.001f;
	private bool plusFlag = true;

	private bool colorFlag;
	private SpriteRenderer thisSR;

	void Start(){
		thisSR = this.GetComponent<SpriteRenderer> ();
		StartCoroutine (alfaChange() );
	}

	// Update is called once per frame
	void Update () {
		if (plusFlag) {
			this.transform.localPosition += Vector3.right * movingSpeed;
		} else {
			this.transform.localPosition -= Vector3.right * movingSpeed;
		}

		float  tmpX =this.transform.localPosition.x;

		if (tmpX < (sideLimite * -1f)  || sideLimite < tmpX) {
			plusFlag = !plusFlag ;
		}
	}

	IEnumerator alfaChange(){
		while (true) {
			float tmpA = thisSR.color.a;

			if (colorFlag) {
				tmpA += 0.005f;
			} else {
				tmpA -= 0.002f;
			}

			thisSR.color = new Color (1, 1, 1, tmpA);

			if ((tmpA < 0.3f && colorFlag ==false) || (0.8f <= tmpA && colorFlag)) {
				colorFlag = !colorFlag;
			}

			yield return new WaitForSeconds (0.1f);
		}
	}
}
