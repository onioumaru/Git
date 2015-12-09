using UnityEngine;
using System.Collections;


[RequireComponent(typeof(enemyStandardMovingScript))]
public class enemyCharaSearchScript : MonoBehaviour {
	private enemyStandardMovingScript eSMS;
	private GameManagerScript gms;

	public float _findingLength;


	// Use this for initialization
	void Start () {
		eSMS = this.GetComponent<enemyStandardMovingScript> ();
		gms = GameManagerGetter.getGameManager ();

		StartCoroutine (mainLoop () );
	}

	IEnumerator mainLoop(){

		while(true){

			yield return new WaitForSeconds(2f);

			GameObject charaGO = gms.getMostNearCharacter(this.transform.position);

			Vector3 tmpV3 = charaGO.transform.position - this.transform.position;

			if (tmpV3.magnitude < _findingLength){
				//発見範囲に入ったら自動追尾
				eSMS.setMoveType(0);
				break;
			}
		}
	}
}
