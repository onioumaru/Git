using UnityEngine;
using System.Collections;

public class generatorSearcherScript : MonoBehaviour {
	public genaratorDaemonScript _genaratorBase;
	public allEnemyBase _enemyBase;

	void OnTriggerEnter2D(Collider2D c){
		if (c.gameObject.name.Substring(0, 9) == "charaBase") {
			//Attack erea でない

			_enemyBase.setFindFlag(true);
			_genaratorBase.enabled = true;

			Destroy (this.gameObject);
		}
	}

}
