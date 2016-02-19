using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class iconEffS : MonoBehaviour {
	public enum_iconEffType iconType;
	public float delayFrame = 0f;
	float movingSpeed = 1.5f;
	private Image thisImage;



	// Use this for initialization
	void Start () {
		//このスプライトのレンダラーは最初に取る
		thisImage = this.transform.GetComponent<Image>();
		//透明化
		Color tmpC = new Color(1f,1f,1f,0f);
		thisImage.color = tmpC;


		// 個別処理
		switch(iconType){
		case enum_iconEffType.ase:
			StartCoroutine ( loop_ase () );
			break;

		case enum_iconEffType.awate:
			StartCoroutine ( loop_awate () );
			break;

		case enum_iconEffType.emotionShine:
			this.transform.localScale = Vector3.one * 0.5f;
			StartCoroutine ( loop_shine () );
			break;

		case enum_iconEffType.bikkuri:
			StartCoroutine ( loop_bikkuri () );
			break;

		case enum_iconEffType.oko:
			StartCoroutine ( loop_oko () );
			break;
		}

		//loop_bikkuri
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


		for (int i = 0; i < 60; i++) {
			yield return null;
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

		for (int i = 0; i < delayFrame; i++) {
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
		

	IEnumerator loop_shine(){
		yield return null;
		thisImage.color = Color.white;

		for (int i = 0; i < delayFrame; i++) {
			yield return null;
			//DelayFameの消化
		}


		for (int i = 0; i < 5; i++) {
			yield return null;
			//this.transform.localScale += new Vector3(0.1f, 0.1f, 1f ) ;
		}

		for (int i = 0; i < 5; i++) {
			yield return null;
			this.transform.localScale -= new Vector3(0.1f, 0.1f, 1f ) ;
		}

		for (int i = 0; i < 6; i++) {
			yield return null;
			this.transform.localScale = new Vector3(i * 0.05f, i * 0.05f, 1f ) ;
		}


		for (int i = 0; i < 30; i++) {
			yield return null;
			//this.transform.localScale += new Vector3(0.1f, 0.1f, 1f ) ;
		}

		for (int i = 0; i < 60; i++) {
			yield return null;

			float alfa = thisImage.color.a - 0.025f;
			Color tmpC = new Color(1f,1f,1f,alfa);

			thisImage.color = tmpC;
			//wait
		}

		Destroy (this.gameObject);
	}


	IEnumerator loop_bikkuri(){
		yield return null;
		thisImage.color = Color.white;

		for (int i = 0; i < delayFrame; i++) {
			yield return null;
			//DelayFameの消化
		}

		for (int i = 0; i < 3; i++) {
			yield return null;
			this.transform.localScale += new Vector3(0.1f, 0.1f, 0f);
		}

		for (int i = 0; i < 20; i++) {
			yield return null;
		}

		//一気にフェードアウト

		thisImage.color = new Color (1, 1, 1, 0.5f);


		for (int i = 0; i < 5; i++) {
			yield return null;

			float alfa = thisImage.color.a - 0.1f;
			Color tmpC = new Color(1f,1f,1f,alfa);

			thisImage.color = tmpC;

			this.transform.localScale += new Vector3(0.2f, 0.2f, 0f);
		}

		Destroy (this.gameObject);
	}

	IEnumerator loop_oko(){
		yield return null;
		thisImage.color = Color.white;

		for (int i = 0; i < delayFrame; i++) {
			yield return null;
			//DelayFameの消化
		}

		Vector3 tmpDefScale = this.transform.localScale;
		float tmpY = tmpDefScale.y / 5f;

		this.transform.localScale = new Vector3(tmpDefScale.x, tmpDefScale.y / 2f, tmpDefScale.z);

		for (int i = 0; i < 7; i++) {
			yield return null;

			this.transform.localScale = new Vector3(tmpDefScale.x, tmpDefScale.y + (tmpY * (float)i), tmpDefScale.z);
		}


		for (int i = 0; i < 4; i++) {
			yield return null;

			this.transform.localScale -= new Vector3(0f, (tmpY * (float)i), 0f);

			Debug.Log (this.transform.localScale);
		}


		for (int i = 0; i < 60; i++) {
			yield return null;
		}

		Destroy (this.gameObject);
	}
}

public enum enum_iconEffType{
	ase
	, awate
	, emotionShine
	, bikkuri
	, oko
}
