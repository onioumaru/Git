using UnityEngine;
using System.Collections;

public class soriteCharaDotChanger : MonoBehaviour {
	public Sprite[] charaSprite;
	public RuntimeAnimatorController[] charaAnime; 

	private Animator thisAnimator;
	private SpriteRenderer thisSR;
	
	// Use this for initialization
	void Start () {
		thisSR = this.transform.GetComponent<SpriteRenderer> ();
		thisAnimator = this.transform.GetComponent<Animator> ();
	}

	public void setAnimation(int argsCharaNum){
		thisSR.sprite = charaSprite [argsCharaNum];
		thisAnimator.runtimeAnimatorController = charaAnime [argsCharaNum];
	}
}
