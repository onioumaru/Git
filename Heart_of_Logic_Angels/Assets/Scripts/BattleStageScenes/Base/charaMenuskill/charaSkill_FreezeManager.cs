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
		}


		StartCoroutine (waitPositionChange_hiragi());
		StartCoroutine (createColliderTimer());
		StartCoroutine (freezeAndDestoryCheck());
	}

	void Update(){
		//移動の計算
		if (movingFlag == true) {

			switch (thisCharaNo){
			case enumCharaNum.akane_04:
				this.transform.position += this.transform.right * 0.01f; 
				break;

			case enumCharaNum.mokuren_06:
				this.transform.position += this.transform.right * 0.02f; 
				break;
			}
		}
	}


	IEnumerator waitPositionChange_hiragi(){
		if (thisCharaNo == enumCharaNum.hiragi_09) {
			Debug.Log ("wait");
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
		case enumCharaNum.sakura_07:
			tmpColldr.GetComponent<charaSkillColliderSakuraScript>().setBaseCharaScript(thisChara);
			break;
		default:
			tmpColldr.GetComponent<charaSkillColliderScript>().setBaseCharaScript(thisChara);
			break;
		}
			
		tmpColldr.transform.parent = this.transform;
		tmpColldr.transform.localPosition = Vector3.zero;
		tmpColldr.transform.localRotation = Quaternion.identity;
	}

}
