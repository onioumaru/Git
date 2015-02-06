using UnityEngine;
using System.Collections;

public class common_damage : MonoBehaviour {

	//GUI上でアタッチ済み
	public GameObject[] bitMapFont;

	private int destroyCnt = 40;

	void Start(){
		showDamage (this.transform, 123);

		Vector3 tmpV = new Vector3 (0f, 1f);
		this.rigidbody2D.velocity = tmpV;
	}

	void Update(){
		//float tmpPP = Mathf.PingPong (Time.time, 0.4f);
		destroyCnt -=1;

		if (destroyCnt < 0){
			Destroy(this.gameObject);
		}
	}


	public void showDamage(Transform origin, int argsVal){

		//offset
		Vector3 tmpV = new Vector3 (0f, 0f);

		this.transform.position = origin.position + tmpV;

		string convStr = argsVal.ToString();

		for (int tmpI = 0; tmpI < convStr.Length; tmpI++) {
			var tmpInt = int.Parse (convStr.Substring(tmpI,1));
			
			//offset2
			Vector3 tmpV2 = new Vector3 (-1 * (convStr.Length - tmpI) * 0.09f, 0f);

			//bitMapFont[tmpInt]
			GameObject retObj =	Instantiate(bitMapFont[tmpInt],(this.transform.position + tmpV2), this.transform.rotation) as GameObject;

			retObj.transform.parent = this.transform;
		}
	}
}
