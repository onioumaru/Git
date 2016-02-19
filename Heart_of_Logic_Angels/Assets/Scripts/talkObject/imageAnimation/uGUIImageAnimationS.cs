using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Image))]
public class uGUIImageAnimationS : MonoBehaviour {
	public Sprite[] _firstAnimation;
	public Sprite[] _LoopAnimation;

	public float _animationInterval = 1f;

	private bool _introFrag = false; 
	private int frameCnt = 0;
	private int animePosi = 0;

	private Image thisImage;

	// Use this for initialization
	void Start () {
		thisImage = this.transform.GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (_introFrag == false && _firstAnimation.Length != 0) {
			//イントロが終わってない
			//無い場合もスルー
			thisImage.sprite = _firstAnimation[animePosi];
		} else {
			//ループ部
			if (_LoopAnimation.Length == 0){
				//ループ部の指定なしの場合、ここで終了
				return;
			}
			thisImage.sprite = _LoopAnimation[animePosi];
		}


		frameCnt++;

		if (frameCnt % _animationInterval == 0) {
			frameCnt = 0;
			animePosi++;

			if (_introFrag == false && _firstAnimation.Length != 0) {

				if (_firstAnimation.Length <= animePosi) {
					//イントロ終了
					_introFrag = true;
					animePosi = 0;
				}

			} else {
				//ループ部
//				thisImage.sprite = _LoopAnimation[animePosi];

				if (_LoopAnimation.Length <= animePosi) {
					animePosi = 0;
				}
			}
		}
	}
}
