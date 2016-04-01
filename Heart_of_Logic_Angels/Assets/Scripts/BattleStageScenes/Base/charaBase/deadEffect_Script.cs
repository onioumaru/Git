using UnityEngine;
using System.Collections;

public class deadEffect_Script : MonoBehaviour {
	public Vector3 _movingMoment;

	void Start () {
		StartCoroutine (velocityDowner());
	}

	IEnumerator velocityDowner(){
		while (true) {
			yield return new WaitForSeconds(0.001f);

			this.transform.localPosition += _movingMoment * 35f * Time.deltaTime;
		}
	}
}
