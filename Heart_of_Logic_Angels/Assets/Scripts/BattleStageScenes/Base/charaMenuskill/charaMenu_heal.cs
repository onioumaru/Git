using UnityEngine;
using System.Collections;

public class charaMenu_heal : MonoBehaviour {

	public GameObject healEffect;

	void OnMouseUp(){
		GameObject tmpGO = Instantiate (healEffect) as GameObject;

		tmpGO.transform.parent = this.gameObject.transform.parent;
		tmpGO.transform.localPosition = Vector3.zero;
	}
}
