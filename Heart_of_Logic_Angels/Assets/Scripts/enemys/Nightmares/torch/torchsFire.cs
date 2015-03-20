using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class torchsFire : MonoBehaviour {
	private Rigidbody2D thisRidiB;
	private GameManagerScript gms;

	private float movingStartWait = 6f;
	private float movingSpeed = 1f;

	// Use this for initialization
	void Start () {
		thisRidiB = this.GetComponent<Rigidbody2D>();
		//thisRidiB.angularVelocity = 360f;

		gms = GameManagerGetter.getGameManager ();

		StartCoroutine (mainLoop ());
	}

	void Update(){
		//force up
		this.transform.rotation = Quaternion.AngleAxis(0f, Vector3.forward);
		}

	IEnumerator mainLoop(){
		yield return new WaitForSeconds (movingStartWait);

		//親からパージして飛ばす
		this.transform.parent = null;

		GameObject tgtChara = gms.getMostNearCharacter (this.transform.position);

		if (tgtChara == null) {
			Destroy (this.gameObject);
			yield break;
				}

		Vector3 tmpV3 = tgtChara.transform.position - this.transform.position;
		Vector2 tmpVelo = new Vector2 (tmpV3.x, tmpV3.y);

		//モーメント設定
		thisRidiB.velocity = tmpVelo.normalized * movingSpeed;
		
		yield return new WaitForSeconds (10f);
		Destroy (this.gameObject);
	}
}
