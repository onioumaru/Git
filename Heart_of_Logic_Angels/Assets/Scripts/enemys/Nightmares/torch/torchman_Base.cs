using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class torchman_Base : MonoBehaviour {
	public GameObject _torchFireBase;

	private float maxSpeed = 0.6f;
	private float addSpeed = 0.1f;

	private Rigidbody2D thisRigi;
	private GameManagerScript gms;

	// Use this for initialization
	void Start () {
		thisRigi = this.GetComponent<Rigidbody2D>();
		gms = GameManagerGetter.getGameManager ();
		
		StartCoroutine (mainLoop ());
		StartCoroutine (createFireBase());
	}

	IEnumerator mainLoop(){
		while (true) {
			yield return new WaitForSeconds(1f);

			GameObject tmpGO = gms.getMostNearCharacter(this.transform.position);
			if (tmpGO == null){yield break;}
			
			Vector3 targetVctr = tmpGO.transform.position - this.transform.position;

			//現在の向きと比べて、90度以上ベクトルが違う場合
			//且つ、移動スピードが0.1以上の場合
			if (Vector2.Angle(thisRigi.velocity ,targetVctr) > 90f ){
				if (thisRigi.velocity.magnitude > 0.2f){
					thisRigi.velocity = thisRigi.velocity / 1.5f;
				} else {
					thisRigi.velocity = targetVctr.normalized * addSpeed;
				}
			} else {
				if (thisRigi.velocity.magnitude >= maxSpeed){
					//最大速度で再セット
					thisRigi.velocity = targetVctr.normalized * maxSpeed;
				} else {
					float calcSpeed = thisRigi.velocity.magnitude + addSpeed;
					thisRigi.velocity = targetVctr.normalized * calcSpeed;
				}
			}
		}
	}

	IEnumerator createFireBase(){
		while (true) {

			GameObject tmpGO = Instantiate(_torchFireBase) as GameObject;
			tmpGO.transform.parent = this.transform;
			tmpGO.transform.localPosition = Vector3.zero;

			tmpGO.GetComponent<torchBase>().setDealDamade(1f);

			yield return new WaitForSeconds (15f);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
	
	public void hitThisEnemy(Transform t){
		Vector3 targetVctr = t.transform.position - this.transform.position;

		thisRigi.velocity = targetVctr.normalized * -1.5f;
	}
}
