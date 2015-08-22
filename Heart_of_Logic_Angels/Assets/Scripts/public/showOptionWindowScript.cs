using UnityEngine;
using System.Collections;

public class showOptionWindowScript : MonoBehaviour {
	public GameObject _optionWindowsPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void showOptionWindow(){
		
		Instantiate (_optionWindowsPrefab);
	}

}
