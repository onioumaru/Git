using UnityEngine;
using System.Collections;

public class soriteCharaDotChanger : MonoBehaviour {
	public Sprite[] charaSprite;
	public RuntimeAnimatorController[] charaAnime; 

	private Animator thisAnimator;
	private SpriteRenderer thisSR;

	public GameObject _dialog1;
	
	// Use this for initialization
	void Start () {
		thisSR = this.transform.GetComponent<SpriteRenderer> ();
		thisAnimator = this.transform.GetComponent<Animator> ();

		//_dialog1 = GameObject.Find("msgBG");
	}

	void Update(){
		if (_dialog1.activeSelf == true) {
			thisSR.enabled = false;
		}

		if (thisSR.enabled == false) {
			if (_dialog1.activeSelf == false) {
				thisSR.enabled = true;
			}
		}
	}

	public void setAnimation(int argsCharaNum){
		thisSR.sprite = charaSprite [argsCharaNum];
		thisAnimator.runtimeAnimatorController = charaAnime [argsCharaNum];
	}
}