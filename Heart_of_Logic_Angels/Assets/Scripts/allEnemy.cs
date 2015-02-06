﻿using UnityEngine;
using System.Collections;

// Rigidbody2Dコンポーネントを必須にする
[RequireComponent(typeof(Rigidbody2D))]
public class allEnemy : MonoBehaviour {

	private int nowHP;

	// 死亡時エフェクトのPrefab
	public GameObject removeEnemyPrefab;
	public GameObject bitmapFontBasePrefab;

	// Use this for initialization
	void Start () {
		nowHP = 10;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setDamage(int argsInt){
		GameObject retObj = Instantiate (bitmapFontBasePrefab, this.transform.position, this.transform.rotation) as GameObject;
		retObj.GetComponentInChildren<common_damage> ().showDamage (this.transform, argsInt);

		nowHP -= argsInt;

		if (nowHP <= 0) {
			this.removedMe(this.transform);
		}
	}

	public void removedMe(Transform origin){
		Instantiate (removeEnemyPrefab, origin.position, origin.rotation);
		Destroy (this.gameObject);
	}

	public void setMoving(int argsType, float argsMovingSpeed){
		switch (argsType) {
		case 1:
			// simlpe reft Moving
			Vector3 tmpV = new Vector3(-1,0);

			this.rigidbody2D.velocity = tmpV.normalized * argsMovingSpeed;

			break;
		case 2:
			break;
			
		case 3:
			break;

		default:
			break;
		}
	}




}
