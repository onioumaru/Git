using UnityEngine;
using System.Collections;

public class enemyAnimationDirection : MonoBehaviour {
	private float beforeDirectionX;


	// Use this for initialization
	void Start () {
		beforeDirectionX = this.transform.position.x;

		StartCoroutine ( mainLoop() );
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator mainLoop(){
		while (true) {
			yield return new WaitForSeconds(0.4f);

			if (beforeDirectionX > this.transform.position.x){
				// 
				float tmpC = this.transform.localScale.x;
				if (tmpC < 0f ){
					// スケールがプラスの場合、-1を掛けてマイナスに
					this.transform.localScale = new Vector3(this.transform.localScale.x * -1f, this.transform.localScale.y, this.transform.localScale.z);
				}
			} else {
				float tmpC = this.transform.localScale.x;

				if (tmpC > 0f ){
					this.transform.localScale = new Vector3(this.transform.localScale.x * -1f, this.transform.localScale.y, this.transform.localScale.z);
				}
			}

			beforeDirectionX = this.transform.position.x;
		}
	}
}
