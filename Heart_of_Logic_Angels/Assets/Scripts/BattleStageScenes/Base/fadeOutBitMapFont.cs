using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class fadeOutBitMapFont : MonoBehaviour {

	private SpriteRenderer thisSR; 

	// Use this for initialization
	void Start () {
		thisSR = this.GetComponentInChildren<SpriteRenderer>();

		StartCoroutine(fadeOutThis());
	}
	
	// Update is called once per frame
	void Update () {

	}

	IEnumerator fadeOutThis(){
		for (int i = 0; i < 30; i++) {
			Color tmpColor = thisSR.color;
			tmpColor.a -= 0.1f;
			
			thisSR.color = tmpColor;
			yield return new WaitForSeconds(0.1f);
		}
	}
}
