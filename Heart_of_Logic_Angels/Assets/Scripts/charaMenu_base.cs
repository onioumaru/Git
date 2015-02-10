using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class charaMenu_base : MonoBehaviour {

	private Transform thisPosition;
	private SpriteRenderer thisSpriteR;
	
	public float scrollInDelay;
	public int thisButtonType;

	private float movingFrame = 5;

	// Use this for initialization
	void Start () {
		thisPosition = this.gameObject.transform;

		Vector3 tmpV = new Vector3 (-0.2f, 0f, 0f);
		tmpV = tmpV + thisPosition.localPosition ;

		this.gameObject.transform.localPosition = tmpV;

		thisSpriteR = this.gameObject.GetComponentInChildren<SpriteRenderer>();

		Color tmpC = thisSpriteR.color;
		tmpC.a = 1f;
		thisSpriteR.color = tmpC;
	}
	
	// Update is called once per frame
	void Update () {
		if (scrollInDelay < 0) {
			if (movingFrame > 0) {
					movingFrame -= 1;
					Vector3 tmpV = new Vector3 (0.04f, 0f, 0f);
					this.gameObject.transform.localPosition += tmpV;
			}
		} else {
			scrollInDelay -= 1;		
		}
	}

	void OnMouseDown(){
		switch(thisButtonType){
		case 1:
			break;
		case 2:
			break;
		case 3:
			break;
		case 4:
			break;
		}
	}

}
