using UnityEngine;
using System.Collections;

public class spriteRainbower : MonoBehaviour {
	private SpriteRenderer spR;

	// Use this for initialization
	void Start () {
		spR = this.GetComponent<SpriteRenderer>();

		StartCoroutine ( mainLoop() );
	}

	IEnumerator mainLoop(){
		float color_Bule = 0;
		float color_Green = 0;
		float color_Red = 255f;


		while (true){
			yield return null;

			if ( color_Red == 255f && color_Green < 255f){

				if (color_Bule > 0){
					color_Bule -= 5;
				} else {
					color_Green += 5;
				}

			} else if (color_Green == 255f && color_Bule < 255f){

				if (color_Red > 0){
					color_Red -= 5;
				} else {
					color_Bule += 5;
				}

			} else if (color_Bule == 255f && color_Red < 255f){
				
				if (color_Green > 0){
					color_Green -= 5;
				} else {
					color_Red += 5;
				}
			}

			Color tmpC = new Color((color_Red /255f) ,(color_Green / 255f), (color_Bule / 255f), 0.5f);
			//string tmpS = color_Red.ToString("X2") + color_Bule.ToString("X2") + color_Green.ToString("X2") + "64";
			//UnityEngine.ColorUtility.TryParseHtmlString(tmpS,out tmpC);

			//Debug.Log( tmpC.ToString());
			spR.color = tmpC;
		}
		//spR.color
	}
}
