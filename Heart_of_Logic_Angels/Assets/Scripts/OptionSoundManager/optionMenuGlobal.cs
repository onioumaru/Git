using UnityEngine;
using System.Collections;

public class optionMenuGlobal : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log ("optionMenuGlobal : Set Time.timeScale = 0");
		Time.timeScale = 0f;

		//全てのコライダーを無効化
		this.diseableAllColliders ();
	}

	void diseableAllColliders(){
		//コライダーの親クラスで指定する
		Collider2D[] allCollider = GameObject.FindObjectsOfType<Collider2D> ();

		foreach (Collider2D tmpCr in allCollider) {
			tmpCr.enabled = false;
				}
	}

}
