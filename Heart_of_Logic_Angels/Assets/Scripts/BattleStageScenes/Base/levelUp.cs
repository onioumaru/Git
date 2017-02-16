using UnityEngine;
using System.Collections;

public class levelUp : MonoBehaviour {
	private int destroyCnt = 50;

	public float pingPongSpeed = 0.6f;
	public float pingPongLength = 0.5f;
	public float pingPongDiffY = 0.01f;

	void Start(){
		//showDamage (this.transform, 123);

		StartCoroutine (mainLoop ());
	}

	IEnumerator mainLoop(){
		while (true) {
			yield return new WaitForSeconds(0.001f);

			destroyCnt -= 1;

			if (destroyCnt > 35) {
				this.transform.localPosition = new Vector3 (0f, Mathf.PingPong (Time.time / pingPongSpeed, pingPongLength) + pingPongDiffY, 0f);
				pingPongLength = pingPongLength / 1.1f;
			}

			if (destroyCnt < 10) {
				SpriteRenderer tmpSR = this.transform.GetComponent<SpriteRenderer> ();
				float tmpAlfa = tmpSR.color.a;
				tmpAlfa -= 0.1f;

				tmpSR.color = new Color (tmpSR.color.r,tmpSR.color.g ,tmpSR.color.g, tmpAlfa);

			}

			if (destroyCnt < 0) {
				Destroy (this.gameObject);
			}
		}
	}


}
