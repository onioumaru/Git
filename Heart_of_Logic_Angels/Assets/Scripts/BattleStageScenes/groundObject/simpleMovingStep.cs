using UnityEngine;
using System.Collections;

public class simpleMovingStep : MonoBehaviour {
	public Vector3 _leftLimitPosition;
	public Vector3 _rightLimitPosition;
	public float _movingTotalTime = 10f;
	public float _movingSpeed = 0.5f;


	// Use this for initialization
	void Start () {
		StartCoroutine( mainLoop() );
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator mainLoop(){
		float xVecter = -1f;
		float yVecter = -1f;

		Vector3 tmpV = (_rightLimitPosition - _leftLimitPosition) / _movingTotalTime;

		while (true) {

			this.transform.localPosition = this.transform.localPosition + (tmpV * xVecter * Time.deltaTime);
			
			yield return new WaitForSeconds(0.00001f);

			if (_leftLimitPosition.x > this.transform.localPosition.x){
				xVecter = 1;
			}
			if (_rightLimitPosition.x < this.transform.localPosition.x){
				xVecter = -1;
			}
			//iTween.MoveTo(this.gameObject, iTween.Hash("x", _rightLimitPosition.x, "y", _rightLimitPosition.y, "time", _movingTotalTime));
			//yield return new WaitForSeconds(_movingTotalTime);
		}


		yield return null;
	}

	void OnTriggerEnter2D(Collider2D co){
		//Debug.Log ("enter");
		co.transform.parent.parent = this.transform;
	}
	
	void OnTriggerExit2D(Collider2D co){
		//Debug.Log ("exit");
		co.transform.parent.parent = null;
	}


}
