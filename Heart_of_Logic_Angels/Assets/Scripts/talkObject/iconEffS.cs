using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class iconEffS : MonoBehaviour {
	public int iconType;
	public float delayFame = 0f;
	float movingSpeed = 1.5f;
	private Image thisImage;



	// Use this for initialization
	void Start () {
		//このスプライトのレンダラーは最初に取る
		thisImage = this.transform.GetComponent<Image>();
		//透明化
		Color tmpC = new Color(1f,1f,1f,0f);
		thisImage.color = tmpC;
		//等倍化
		this.transform.localScale = Vector3.one;


		// 個別処理

		switch(iconType){
		case 0:
			StartCoroutine ( loop_ase () );
			break;
		case 1:
			StartCoroutine ( loop_awate () );
			break;
		}

	}

	
	IEnumerator loop_ase(){
		yield return null;
		
		for (int i = 0; i < 10; i++) {
			yield return null;

			float alfa = thisImage.color.a + 0.1f;
			Color tmpC = new Color(1f,1f,1f,alfa);
			
			thisImage.color = tmpC;
			//wait
		}

		for (int i = 0; i < 20; i++) {
			yield return null;

			float tmpY = this.transform.localPosition.y - movingSpeed;
			Vector3 tmpV3 = new Vector3(transform.localPosition.x, tmpY ,this.transform.localPosition.z);

			this.transform.localPosition = tmpV3;
			}

		for (int i = 0; i < 30; i++) {
			yield return null;
			
			float tmpY = this.transform.localPosition.y - movingSpeed / 5f;
			Vector3 tmpV3 = new Vector3(transform.localPosition.x, tmpY ,this.transform.localPosition.z);
			
			this.transform.localPosition = tmpV3;
		}

		for (int i = 0; i < 30; i++) {
			yield return null;

			float alfa = thisImage.color.a -0.05f;
			Color tmpC = new Color(1f,1f,1f,alfa);

			thisImage.color = tmpC;
			//wait
		}

		Destroy (this.gameObject);
	}




	
	
	IEnumerator loop_awate(){
		Vector3 tmpV3_start = new Vector3 (0f,0f, 1);
		this.transform.localScale = tmpV3_start;

		for (int i = 0; i < delayFame; i++) {
			yield return null;
			//DelayFameの消化
		}
		
		for (int i = 0; i < 5; i++) {
			//fade in
			yield return null;

			float alfa = thisImage.color.a + 0.1f;
			Color tmpC = new Color(1f,1f,1f,alfa);
			
			thisImage.color = tmpC;

			// Zoom
			Vector3 tmpV3 = new Vector3 (this.transform.localScale.x + 0.2f,this.transform.localScale.y + 0.2f, 1);
			this.transform.localScale = tmpV3;
		}

		for (int i = 0; i < 40; i++) {
			//wait
			yield return null;
		}

		for (int i = 0; i < 30; i++) {
			//fade out
			yield return null;

			float alfa = thisImage.color.a -0.05f;
			Color tmpC = new Color(1f,1f,1f,alfa);


			Vector3 tmpV3 = new Vector3(this.transform.localScale.x +0.05f, this.transform.localScale.y + 0.05f, 1f);
			this.transform.localScale = tmpV3;

			thisImage.color = tmpC;
			//wait
		}
		
		Destroy (this.gameObject);
	}






}
