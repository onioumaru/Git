using UnityEngine;
using System.Collections;


public class tamanekoBlackScript : MonoBehaviour {
	private allEnemyBase thisBase;
	private GameObject thisAnimeObj;

	void Start(){
		thisBase = this.GetComponent<allEnemyBase> ();
		thisAnimeObj = this.transform.Find ("anime").gameObject;
	}

	public void tamanekoBlackGrowing(){
		thisBase.setAttackDefenceMagnificaion (enemyMagnificationArgsType.attackAdd, 0.1f);
		thisBase.setAttackDefenceMagnificaion (enemyMagnificationArgsType.defenceAdd, 0.1f);

		//全回復
		thisBase.healEnemy (99999999f);

		StartCoroutine ( iconDelay () );
		//soundManagerGetter.getManager().playOneShotSound(enm_oneShotSound.attack03
	}

	IEnumerator iconDelay(){
		yield return new WaitForSeconds (0.5f);

		if (thisAnimeObj.transform.localScale.x > 0) {
			thisAnimeObj.transform.localScale += new Vector3 (0.1f, 0.1f, 0f);
		} else {
			thisAnimeObj.transform.localScale += new Vector3 (-0.1f, 0.1f, 0f);
		}

		GameObject exclamationPrefab = Resources.Load ("Prefabs/charaBase/enemyPowerUpIcon") as GameObject;

		GameObject tmpGO = Instantiate( exclamationPrefab) as GameObject;
		tmpGO.transform.position = this.transform.position;

		textureVector ttv = new textureVector (this.gameObject);
		tmpGO.transform.position += new Vector3(0f, ttv.getHeight(true), 0f);
		tmpGO.transform.parent = this.transform;

		thisBase.createHPBar ();
	}

}
