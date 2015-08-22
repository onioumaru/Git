using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class waitIconFazerS : MonoBehaviour {
	public Sprite[] fazerAnime;	//0-2
	private int loopCnt;
	private Image thisImage;

	// Use this for initialization
	void Start () {
		thisImage = this.GetComponent<Image> ();

		StartCoroutine ( mainAnimeLoop() );
	}

	IEnumerator mainAnimeLoop(){
		loopCnt = 0;

		while (true) {
			yield return null;

			switch(loopCnt){
			case 0:
				thisImage.sprite = fazerAnime[0];
				break;
			case 10:
				thisImage.sprite = fazerAnime[1];
				break;
			case 20:
				thisImage.sprite = fazerAnime[2];
				break;
			}

			loopCnt++;
			if (loopCnt > 30){
				loopCnt = 0;
			}
		}
	}
}
