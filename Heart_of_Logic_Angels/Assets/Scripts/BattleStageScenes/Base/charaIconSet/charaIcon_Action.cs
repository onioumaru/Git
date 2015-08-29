using UnityEngine;
using System.Collections;

public class charaIcon_Action : MonoBehaviour {
	//固定.
	public GameObject characterMenu;

	//start時,init
	private GameObject thisCharaFlag;
	private GameObject thisCharaBase;

	//
	private GameObject cloneCharaFlag;
	private GameObject trackingObj;
	
	private bool cloneFlag = false;
	private bool dragCancelF = false;
	
	private bool leftDragF = false;
	private bool rightDragF = false;

	private bool doubleClickCheck = false;
	private float coubleClickDeleySec = 0.3f;

	private GameObject charaMenuInstance;
	private GameManagerScript GMS;

	void Start(){
		trackingObj = GameObject.Find("CameraTracker");

		GMS = GameManagerGetter.getGameManager ();

	}

	
	void OnMouseDown(){
		thisCharaBase = this.gameObject.GetComponentInParent<charaIconsetManager> ().thisCharaBase;
		thisCharaFlag = this.gameObject.GetComponentInParent<charaIconsetManager> ().thisCharaFlag;

		dragCancelF = false;

		if (doubleClickCheck == false) {
			doubleClickCheck = true;
			StartCoroutine(doubleClickWait());
		} else {
			//ダブルクリックされた
			doubleClickCheck = false;

			Vector3 tmpV = new Vector3(thisCharaBase.transform.position.x, thisCharaBase.transform.position.y, -20f);
			Camera.main.transform.position = tmpV;

			soundManagerGetter.getManager().playOneShotSound(enm_oneShotSound.charaMenu);

			charaMenuInstance = Instantiate(characterMenu) as GameObject;
			charaMenuInstance.GetComponentInChildren<charaMenu_parent>().setParentChara(thisCharaBase);
			charaMenuInstance.GetComponentInChildren<charaMenu_parent>().setParentIconSet(this.gameObject.transform.parent.gameObject);
		}
	}

	IEnumerator doubleClickWait(){
		yield return new WaitForSeconds(coubleClickDeleySec);
		doubleClickCheck = false;
		//Debug.Log ("clear");
		}

	void OnMouseDrag(){
		if (charaMenuInstance == null) {
			Vector2 firstMouseDown = Camera.main.ScreenToWorldPoint (Input.mousePosition);
	
			float tmpY = this.gameObject.transform.localPosition.y + this.gameObject.transform.parent.transform.localPosition.y + this.gameObject.transform.parent.transform.parent.transform.localPosition.y + 0.5f;
			float tmpX = this.gameObject.transform.localPosition.x + this.gameObject.transform.parent.transform.localPosition.x+ this.gameObject.transform.parent.transform.parent.transform.localPosition.x;

					if (firstMouseDown.y > tmpY) {
							//アイコン上部に移動した場合

							//キャラのトラッキングフラグは直ぐにOFF
							leftDragF = false;
							rightDragF = false;

							if (cloneFlag == false) {
									cloneCharaFlag = Instantiate (thisCharaFlag, firstMouseDown, Quaternion.identity) as GameObject;
									//半透明にする
									Color tmpColor = cloneCharaFlag.GetComponentInChildren<SpriteRenderer> ().color;
									tmpColor.a = 0.5f;
									cloneCharaFlag.GetComponentInChildren<SpriteRenderer> ().color = tmpColor;

									cloneFlag = true;
							} else {
									//ボーダーより上、且つ、クローン作製済み
									//クローンの移動
									cloneCharaFlag.transform.position = firstMouseDown;
							}

					} else {
							if (cloneFlag == false) {
									if (dragCancelF == false) {
											//ボーダーより下、且つ、クローン作製していない
											//左右ドラッグの判定

											//クローン作製されて、削除されている場合、dragCancelF=trueになる

											float diffX = firstMouseDown.x - tmpX;
				
											if (Mathf.Abs (diffX) > 1f) {
													if (diffX > 0) {
															//右
															//Debug.Log("diffX > 0");
															rightDragF = true;
													} else {
															//左
															//Debug.Log("diffX < 0");
															leftDragF = true;
													}
											}
									}
							} else {
									//ボーダーより下、且つ、クローン作製済み
									//クローンの削除
									cloneFlag = false;
									Destroy (cloneCharaFlag);

									dragCancelF = true;
							}
					}
			}
	}

	void OnMouseUp(){
		if (cloneFlag == true) {
			//本物のフラグを移動させる
			thisCharaFlag.transform.position = cloneCharaFlag.transform.position;
			//移動開始
			thisCharaBase.GetComponentInChildren<allCharaBase> ().stopFlag = false;
			//Clone削除
			Destroy (cloneCharaFlag);
			
			this.dragFlagReset();
		} else if (leftDragF == true) {
			//フラグのクローンがつくられていた場合は、ここは通らない
			trackingObj.GetComponentInChildren<cameraTrackerScript> ().setCharaTracking (thisCharaBase);

			GMS.getBattleTextCanvasS().showHoldText();

			this.dragFlagReset();
		} else if (rightDragF == true) {
			
			float cameraZ = Camera.main.transform.localPosition.z;
			//Tracking中止
			trackingObj.GetComponentInChildren<cameraTrackerScript>().setCharaTrackReset();
			
			Vector3 tmpV_right = new Vector3(thisCharaFlag.transform.position.x, thisCharaFlag.transform.position.y, cameraZ);
			Camera.main.transform.position = tmpV_right;

			this.dragFlagReset();

			
			GMS.getBattleTextCanvasS().showFlagText();
		}
	}

	void dragFlagReset(){
		cloneFlag = false;
		leftDragF = false;
		rightDragF = false;
	}

}
