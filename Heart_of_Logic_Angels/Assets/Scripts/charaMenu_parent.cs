using UnityEngine;
using System.Collections;

public class charaMenu_parent : MonoBehaviour {
	
	private Camera trackedCamera;
	private float posiZ;

	private allChara argsChara;

	// Use this for initialization
	void Start () {
		Time.timeScale = 0;
		trackedCamera = Camera.main;

		posiZ = this.gameObject.transform.position.z;
		this.transform.position = new Vector3 (trackedCamera.transform.position.x, trackedCamera.transform.position.y, posiZ);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		this.closeMe();
	}

	public void closeMe(){
		Time.timeScale = 1f;
		Destroy(this.gameObject);
	}

	public void setParentChara(GameObject arsGO){
		argsChara = arsGO.GetComponentInChildren<allChara>();
	}

	public allChara getAllCharaScript(){
		return argsChara;
	}
}
