using UnityEngine;
using System.Collections;

public class generatorSearcherScript : MonoBehaviour {
	public genaratorDaemonScript _genaratorBase;
	public allEnemyBase _enemyBase;

	void OnTriggerEnter2D(Collider2D c){
		if (c.gameObject.name.Substring(0, 9) == "charaBase") {
			//Attack erea でない

			GameManagerScript gms = GameManagerGetter.getGameManager ();
			GameObject charaGO = gms.getMostNearCharacter(this.transform.position);
			Vector3 tmpV3 = charaGO.transform.position - this.transform.position;

			bool leftFlag = false;
			if (tmpV3.x < 0) {
				leftFlag = true;
			}

			_enemyBase.setFindFlag(true, leftFlag);
			_genaratorBase.enabled = true;

			Destroy (this.gameObject);
		}
	}

}
