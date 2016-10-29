using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class mainClickCollider : MonoBehaviour {
	public Text _testcountText;

	private float testClickcount;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		//Debug.Log ("click");
		testClickcount += 1f;
		_testcountText.text = testClickcount.ToString ();

	}






}
