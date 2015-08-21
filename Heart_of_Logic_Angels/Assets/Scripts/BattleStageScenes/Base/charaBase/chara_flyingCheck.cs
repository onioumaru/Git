using UnityEngine;
using System.Collections;

public class chara_flyingCheck : MonoBehaviour {
	//GUI上でアタッチ
	public GameObject wingEffect;
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

	IEnumerator flyingCheckLoop(){
		while (true) {
			yield return new WaitForSeconds(0.35f);

			if (flyingCheckF == true){
				if (flyingDoneF == false){
					flyingDoneF = true;

					instanceWing = Instantiate (wingEffect) as GameObject;
					
					instanceWing.transform.parent = this.transform;
					
					Vector3 tmpV = new Vector3 (0f, -0.15f, 0f);
					instanceWing.transform.localPosition = tmpV;
				}

			} else {
				if (flyingDoneF == true) {
					Destroy(instanceWing);
					thisChara.setFlying(false);

					flyingDoneF = false;
				}
			}

			//毎秒飛行フラグを立てておき、StayでFalse戻す
			flyingCheckF = true;
		}
	}
}
