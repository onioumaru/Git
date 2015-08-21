using UnityEngine;
using System.Collections;

public class systemMenu_Script : MonoBehaviour {
	public GameObject _optionMenuPrefab;

	private tapedObjectMotion tapM;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		tapM = this.GetComponent<tapedObjectMotion> ();
		tapM.actionTapEffect ();

		Instantiate (_optionMenuPrefab);

	}
}
