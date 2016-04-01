using UnityEngine;
using System.Collections;

public class enemyThrowingBullet : MonoBehaviour {
	public GameObject _throwBullet;
	public float _genarateIntervalSec;

	private GameManagerScript GMS;
	private allEnemyBase thisEnemy;

	// Use this for initialization
	void Start () {
		thisEnemy = this.gameObject.GetComponent<allEnemyBase> ();
		GMS = GameManagerGetter.getGameManager ();
		StartCoroutine ( mainLoop() );
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator mainLoop(){
		while(true){
			yield return new WaitForSeconds (_genarateIntervalSec);

			//敵発見してない場合はやらない
			if (thisEnemy.charaFindFlag == true) {
				GameObject tmpGO = Instantiate (_throwBullet) as GameObject;
				tmpGO.transform.position = this.transform.position;

				enemyStandardMovingScript eTB = tmpGO.GetComponent<enemyStandardMovingScript> ();

				Vector3 tmpV = this.transform.position;
				GameObject nearChara =	GMS.getMostNearCharacter (tmpV);
				if (nearChara == null){yield break;}

				eTB.setMoveTypeTargetPosi (1, nearChara.transform.position - tmpV);

				tmpGO.transform.Find ("AttackErea").GetComponent<damageErea>().setDealDamage(thisEnemy.getAttackingPower() * 0.8f);

				eTB.setDestoryMe (1f);
			}

		}		
	}
}
