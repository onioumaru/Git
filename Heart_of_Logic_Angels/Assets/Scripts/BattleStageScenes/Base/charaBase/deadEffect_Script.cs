using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class deadEffect_Script : MonoBehaviour {
	
	public float VectX;
	public float VectY;
	
	// Use this for initialization
	void Start () {
		Vector2 tmpV = new Vector2(VectX, VectY);
	
		this.transform.GetComponent<Rigidbody2D>().velocity = tmpV * 0.7f;

		//StartCoroutine (velocityDowner());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator velocityDowner(){
		while (true) {
			yield return new WaitForSeconds(0.5f);

			this.transform.GetComponent<Rigidbody2D>().velocity = (this.transform.GetComponent<Rigidbody2D>().velocity * 0.5f);
		}
	}
}
