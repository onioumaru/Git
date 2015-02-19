using UnityEngine;
using System.Collections;

public class charaMenu_heal : MonoBehaviour {

	public GameObject healEffect;

	void OnMouseUp(){
		GameObject tmpGO = Instantiate (healEffect) as GameObject;

		Vector3 tmpV = new Vector3(0,0,0);

		tmpGO.transform.parent = this.gameObject.transform.parent;
		tmpGO.transform.localPosition = tmpV;
	}
}
