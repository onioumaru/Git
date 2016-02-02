using UnityEngine;
using System.Collections;

public class charaSkill_FreezeManager : MonoBehaviour {
	public float totalFreezeSec;
	public float colliderCreateSec;

	public GameObject thisCollider;
	public enumCharaNum thisCharaNo;

	private allCharaBase thisChara;

	public bool _OtherDestoryFlag;
	public float _OtherDestoryTotalSec;

	private bool movingFlag = false;

	// Use this for initialization
	void Start () {
		thisChara = this.transform.parent.GetComponent<allCharaBase> ();


		//自立移動は親子解除
		switch (thisCharaNo){
		case enumCharaNum.akane_04:
		case enumCharaNum.mokuren_06: 
		case enumCharaNum.hiragi_09: 
			this.transform.parent = null;
			break;
		case enumCharaNum.sion_08: 
			this.transform.parent = null;
			this.transform.position += Vector3.forward * -1f;
			break;
		}

		StartCoroutine (waitPositionChange_hiragi());
		StartCoroutine (createColliderTimer());
		StartCoroutine (freezeAndDestoryCheck());
	}

	void Update(){
		//移動の計算
		if (Time.timeScale > 0){
			if (movingFlag == true) {

				switch (thisCharaNo){
				case enumCharaNum.sion_08:
					this.transform.position += this.transform.right * 0.01f; 
					break;

				case enumCharaNum.akane_04:
					this.transform.position += this.transform.right * 0.01f; 
					break;

				case enumCharaNum.mokuren_06:
					this.transform.position += this.transform.right * 0.02f; 
					break;
				}
			}
		}
	}

	public void setMovingFlag(bool argsBool){
		movingFlag = argsBool;
	}


	IEnumerator waitPositionChange_hiragi(){
		if (thisCharaNo == enumCharaNum.hiragi_09) {
			//Debug.Log ("wait");
			yield return new WaitForSeconds (3f);

			//今のところhiragiだけ
			this.transform.position += this.transform.right * 4f;
		}
	}

	IEnumerator freezeAndDestoryCheck(){
		yield return new WaitForSeconds (totalFreezeSec);

		// todo？
		//アニメーションのトリガーをここで入れる？

		movingFlag = true;	//スキルのコライダーの移動開始フラグ。移動範囲のスキルのみ

		thisChara.setBeforeCharaMode ();
		if (_OtherDestoryFlag) {
			//別途破壊時間が設定されている場合
			//さらにWait
			yield return new WaitForSeconds (_OtherDestoryTotalSec - totalFreezeSec);
			Destroy (this.gameObject);

		} else {
			Destroy (this.gameObject);
		}
	}
	
	IEnumerator createColliderTimer(){
		yield return new WaitForSeconds (colliderCreateSec);
		GameObject tmpColldr = Instantiate(thisCollider) as GameObject;

		switch (thisCharaNo){
		case enumCharaNum.houzuki_05:
		case enumCharaNum.suzusiro_03:
			tmpColldr.GetComponent<charaSkillDefenceColliderScript>().setBaseCharaScript (thisChara);
			break;
		case enumCharaNum.sion_08:
			tmpColldr.GetComponent<charaSkillColliderSionScript> ().setBaseCharaScript (thisChara);

			if (this.transform.right.x < 0) {
				//反転
				tmpColldr.transform.localScale = new Vector3 (1f, 1f, -1f);
			};
			//Debug.Log (tmpColldr.transform.localRotation);
			break;

		case enumCharaNum.sakura_07:
			tmpColldr.GetComponent<charaSkillColliderSakuraScript>().setBaseCharaScript(thisChara);
			break;
		default:
			tmpColldr.GetComponent<charaSkillColliderScript>().setBaseCharaScript(thisChara);
			tmpColldr.transform.localRotation = Quaternion.identity;
			break;
		}
			
		tmpColldr.transform.parent = this.transform;
		tmpColldr.transform.localPosition = Vector3.zero;

		if (thisCharaNo == enumCharaNum.syusuran_02) {
			tmpColldr.transform.localRotation = Quaternion.identity;
		}
	}

}
