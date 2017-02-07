using UnityEngine;
using System.Collections;

public class chara_flyingCheck : MonoBehaviour {
	//GUI上でアタッチ
	public GameObject wingEffect;
	public GameObject wingHuwahuwaMosion;

	private GameObject instanceWing = null;
	private allCharaBase thisChara;
	
	private bool flyingCheckF = false;
	private bool flyingDoneF = false;

	void Start(){
		thisChara = this.transform.parent.GetComponent<allCharaBase> ();

		StartCoroutine (flyingCheckLoop ());
		}

	void OnTriggerStay2D(Collider2D co){
		//入った時、着地とする
		flyingCheckF = false;
	}

	void OnTriggerExit2D(Collider2D co){
		//入った時、着地とする
		flyingCheckF = true;
	}

	IEnumerator flyingCheckLoop(){
		while (true) {
			yield return new WaitForSeconds(0.35f);

			if (flyingDoneF == false && flyingCheckF == true){
				/*
				if (instanceWing == null) {
					instanceWing = Instantiate (wingEffect) as GameObject;

					instanceWing.transform.parent = this.transform;

					Vector3 tmpV = new Vector3 (0f, -0.15f, 0f);
					instanceWing.transform.localPosition = tmpV;
					thisChara.setFlying (true);
				}*/

				thisChara.setFlying (true);

				//自動生成から設置済みに変更
				wingEffect.SetActive(true);
				wingHuwahuwaMosion.transform.GetComponent<charaAnimationSubScript>().flyingHuwahuwaSet(true);
				flyingDoneF = true;

			} else if(flyingDoneF == true && flyingCheckF == false) {
				/*
				if (instanceWing != null) {
					Destroy(instanceWing);
					thisChara.setFlying(false);
				}*/

				thisChara.setFlying (false);

				wingEffect.SetActive(false);
				wingHuwahuwaMosion.transform.GetComponent<charaAnimationSubScript> ().flyingHuwahuwaSet (false);
				flyingDoneF = false;
			}

			//毎秒飛行フラグを立てておき、StayでFalse戻す
			//flyingCheckF = true;
		}
	}
}
