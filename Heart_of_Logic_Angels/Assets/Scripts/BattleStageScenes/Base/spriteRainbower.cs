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
		int color_Bule = 0;
		int color_Green = 0;
		int color_Red = 255;


		while (true){
			yield return null;

			if ( color_Red == 255 && color_Green < 255){

				if (color_Bule > 0){
					color_Bule -= 5;
				} else {
					color_Green += 5;
				}

			} else if (color_Green == 255 && color_Bule < 255){

				if (color_Red > 0){
					color_Red -= 5;
				} else {
					color_Bule += 5;
				}

			} else if (color_Bule == 255 && color_Red < 255){
				
				if (color_Green > 0){
					color_Green -= 5;
				} else {
					color_Red += 5;
				}
			}

			Color tmpC;// = new Color(color_Red ,color_Green, color_Bule, 0.5f);
			string tmpS = color_Red.ToString("X2") + color_Bule.ToString("X2") + color_Green.ToString("X2") + "64";

						UnityEngine.ColorUtility.TryParseHtmlString(tmpS,out tmpC);

			//Debug.Log( tmpC.ToHexStringRGBA());
			spR.color = tmpC;
		}
		//spR.color
	}
}
