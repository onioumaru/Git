using UnityEngine;
using System.Collections;

public class healEffectBase : MonoBehaviour {

	public GameObject kira;

	// Use this for initialization
	void Start () {
		StartCoroutine (kiraDelay());
		StartCoroutine (kiraDelay());
		StartCoroutine (kiraDelay());
		//Debug.Log (this.gameObject.transform.position );
		StartCoroutine (closeDelay());
	}
	
	IEnumerator kiraDelay(){
		yield return new WaitForSeconds (Random.value * 0.20f);
		
		float tmpFlt = (Random.value -0.5f) * 0.5f;
		
		GameObject tmpGO = Instantiate(kira) as GameObject;
		tmpGO.transform.parent = this.gameObject.transform.parent;
		
		Vector3 tmpV = new Vector3 (tmpFlt, tmpFlt, 0);
		tmpGO.transform.localPosition = tmpV;
	}

	IEnumerator closeDelay(){
		yield return new WaitForSeconds (0.21f);
		Destroy (this.gameObject);
	}

	void OnTriggerEnter2D(Collider2D co){
		if (co.name == "charaBase(Clone)") {
			allCharaBase tmpAC = co.gameObject.GetComponent<allCharaBase>();
			tmpAC.setHealing(1);
		}
	}
}