using UnityEngine;
using System.Collections;

public class standingImageActionController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine ( pushThis() );
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	IEnumerator pushThis(){
		float defaultScale = this.transform.localScale.x;

		//Debug.Log ("start");
		for (int i = 0; i < 10; i++) {
			float tmpf = defaultScale + (Random.value / 4f);
			//tmpf

			//Debug.Log (tmpf);

			Vector3 tmpV = new Vector3(tmpf, tmpf, 1f);
			this.transform.localScale = tmpV;
			
			yield return null;
		}

		Vector3 resetV3 = new Vector3(defaultScale, defaultScale, 1f);
		this.transform.localScale = resetV3;
	}

}
