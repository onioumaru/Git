using UnityEngine;
using System.Collections;

public class charaIcon_Action : MonoBehaviour {
	
	public GameObject thisCharaFlag;
	public GameObject thisCharaStates;

	private GameObject cloneCharaFlag;

	private bool cloneFlag = false;

	//private Transform firstMouseDown;

	// Use this for initialization
	void Start () {

	}
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseDown(){

	}

	void OnMouseDrag(){

		Vector2 firstMouseDown = Camera.main.ScreenToWorldPoint (Input.mousePosition);

		float tmpY = this.gameObject.transform.localPosition.y + this.gameObject.transform.parent.transform.localPosition.y + 0.5f;

		if (firstMouseDown.y > tmpY) {
			//
			if (cloneFlag == false){
				cloneCharaFlag = Instantiate(thisCharaFlag,firstMouseDown , Quaternion.identity) as GameObject;
				//半透明にする
				Color tmpColor = cloneCharaFlag.GetComponentInChildren<SpriteRenderer>().color;
				tmpColor.a = 0.5f;
				cloneCharaFlag.GetComponentInChildren<SpriteRenderer>().color = tmpColor;

				cloneFlag = true;
			} else {
				//ボーダーより上、且つ、クローン作製済み
				//クローンの移動
				cloneCharaFlag.transform.position = firstMouseDown;
			}

		} else {
			if (cloneFlag == false){
				//ボーダーより下、且つ、クローン作製していない
				
				//左右ドラッグの判定

			}else {
				//ボーダーより下、且つ、クローン作製済み
				//クローンの削除
				cloneFlag = false;
				Destroy(cloneCharaFlag);
			}
		}

	}
	void OnMouseUp(){
		if (cloneFlag == true) {
			//本物のフラグを移動させる
			thisCharaFlag.transform.position  = cloneCharaFlag.transform.position ;
			//移動開始
			thisCharaStates.GetComponentInChildren<allChara>().stopFlag= false;
			//Clone削除
			cloneFlag = false;
			Destroy(cloneCharaFlag);
		}
	}

}
