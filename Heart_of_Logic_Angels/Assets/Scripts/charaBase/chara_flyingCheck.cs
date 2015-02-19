using UnityEngine;
using System.Collections;

public class chara_flyingCheck : MonoBehaviour {
	//GUI上でアタッチ
	public GameObject wingEffect;
	private GameObject instanceWing = null;
	private allChara thisChara;

	void Start(){
		thisChara = this.transform.parent.GetComponent<allChara> ();

		}

	void OnTriggerEnter2D(Collider2D co){
		if (co.name.Substring(0,5) == "stage") {
			if (instanceWing != null) {
				Destroy(instanceWing);
				thisChara.setFlying(false);
			}

		}
	}

	void OnTriggerExit2D(Collider2D co){
		if (co.name.Substring(0,5) == "stage") {
			instanceWing = Instantiate (wingEffect) as GameObject;
			
			instanceWing.transform.parent = this.transform;
			
			Vector3 tmpV = new Vector3 (0f, -0.15f, 0f);
			instanceWing.transform.localPosition = tmpV;

			if (thisChara != null){
				thisChara.setFlying(true);
			}
		}
	}
}
