﻿using UnityEngine;
using System.Collections;

public class common_damage : MonoBehaviour {

	//GUI上でアタッチ済み
	public GameObject[] bitMapFont;

	private int destroyCnt = 50;

	void Start(){
		//showDamage (this.transform, 123);

	}

	void Update(){
		//float tmpPP = Mathf.PingPong (Time.time, 0.4f);
		destroyCnt -=1;

		if (destroyCnt < 0){
			Destroy(this.gameObject);
		}
	}


	public void showDamage(Transform origin, int argsVal){
		Vector3 tmpVcDef = new Vector3 (0f, 1f);
		this.rigidbody2D.velocity = tmpVcDef;

		//offset
		Vector3 tmpV = new Vector3 (0f, 0f);

		this.transform.position = origin.position + tmpV;

		string convStr = argsVal.ToString();

		for (int tmpI = 0; tmpI < convStr.Length; tmpI++) {
			var tmpInt = int.Parse (convStr.Substring(tmpI,1));
			
			//offset2
			Vector3 tmpV2 = new Vector3 (-1 * (convStr.Length - tmpI) * 0.18f, 0f);

			//bitMapFont[tmpInt]
			GameObject retObj =	Instantiate(bitMapFont[tmpInt],(this.transform.position + tmpV2), this.transform.rotation) as GameObject;
			
			retObj.transform.localScale = new Vector3(2f,2f,1f);

			retObj.transform.parent = this.transform;
		}
	}
}
