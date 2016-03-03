using UnityEngine;
using System.Collections;

public class LotoSpirit_bulletScript : MonoBehaviour {
	public GameObject _largeBulletPrefab;
	public Vector3 _createBulletPosi;

	private GameObject largeBulletInstance;
	private allEnemyBase aEB;

	// Use this for initialization
	void Start () {
		largeBulletInstance = Instantiate (_largeBulletPrefab) as GameObject;
		largeBulletInstance.transform.position = _createBulletPosi;

		aEB = this.gameObject.GetComponent<allEnemyBase>();

		StartCoroutine ( mainLoop() );
	}

	IEnumerator mainLoop(){
		while(true){

			if (aEB.charaFindFlag == true) {
				//遠隔の玉の発生停止
				largeBulletInstance.GetComponent<NatureBullet_Shine3> ().stopGenarate ();

				//アニメーションの通常化
				this.transform.Find("anime").GetComponent<Animator>().SetTrigger("gotoNomal");

				this.StopAllCoroutines ();
				break;
			}
			yield return new WaitForSeconds (1f);
		}
	}
}
