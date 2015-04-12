using UnityEngine;
using System.Collections;

public class charaSkill_FreezeManager : MonoBehaviour {
	public float totalFreezeSec;
	public float colliderCreateSec;

	public GameObject thisCollider;
	public enumCharaNum thisCharaNo;

	private allCharaBase thisChara;

	// Use this for initialization
	void Start () {
		thisChara = this.transform.parent.GetComponent<allCharaBase> ();

		StartCoroutine (createColliderTimer());
		StartCoroutine (freezeAndDestoryCheck());
	}

	IEnumerator freezeAndDestoryCheck(){
		yield return new WaitForSeconds (totalFreezeSec);

		// todo
		//アニメーションのトリガーをここで淹れる

		thisChara.setBeforeCharaMode ();
		Destroy (this.gameObject);
	}
	
	IEnumerator createColliderTimer(){
		yield return new WaitForSeconds (colliderCreateSec);
		GameObject tmpColldr = Instantiate(thisCollider) as GameObject;

		tmpColldr.GetComponent<charaSkillColliderScript> ().setBaseCharaScript (thisChara);

		tmpColldr.transform.parent = this.transform;
		tmpColldr.transform.localPosition = Vector3.zero;
		tmpColldr.transform.localRotation = Quaternion.identity;
	}

}
