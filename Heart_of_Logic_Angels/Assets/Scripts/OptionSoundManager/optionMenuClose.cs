using UnityEngine;
using System.Collections;

public class optionMenuClose : MonoBehaviour {
	public GameObject baseManu;

	// Use this for initialization
	void Start () {
	
	}

	public void closeThisMenu(){
		Time.timeScale = 1f;
		this.enabeleAllColliders ();

		Debug.Log ("optionMenuClose : closeThisMenu : Time.timeScale = 1");
		Destroy(baseManu);
	}

	
	void enabeleAllColliders(){
		Collider2D[] allCollider = GameObject.FindObjectsOfType<Collider2D> ();
		
		foreach (Collider2D tmpCr in allCollider) {
			//Debug.Log (tmpCr.name);
			tmpCr.enabled = true;
		}
		//Debug.Log (allCollider.Length);
	}

}
