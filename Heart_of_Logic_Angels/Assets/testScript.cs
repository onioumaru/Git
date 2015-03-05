using UnityEngine;
using System.Collections;

public class testScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SpriteRenderer sss = this.GetComponent<SpriteRenderer>();

		textureVector bbb = new textureVector (sss.sprite);

		Debug.Log (bbb.aaa ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
