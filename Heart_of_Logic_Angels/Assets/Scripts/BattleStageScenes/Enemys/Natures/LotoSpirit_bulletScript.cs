using UnityEngine;
using System.Collections;

public class LotoSpirit_bulletScript : MonoBehaviour {
	public GameObject _largeBulletPrefab;
	public Vector3 _createBulletPosi;

	public GameObject _bulletSourcePrefab;
	public float _bulletSourceMovingTotalFrame = 600f;

	private GameObject largeBulletInstance;
	private allEnemyBase aEB;
	private GameObject bulletSourceInstance;

	public float _bulletDamageMagn = 1f;

	// Use this for initialization
	void Start () {
		largeBulletInstance = Instantiate (_largeBulletPrefab) as GameObject;
		largeBulletInstance.transform.position = _createBulletPosi;

		aEB = this.gameObject.GetComponent<allEnemyBase>();

		largeBulletInstance.GetComponent<NatureBullet_Shine3> ().setDealDamege (aEB.getAttackingPower() * _bulletDamageMagn);

		StartCoroutine( magicSouceMoving() );

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

				GameObject.Destroy (bulletSourceInstance);

				break;
			}
			yield return new WaitForSeconds (1f);
		}
	}

	IEnumerator magicSouceMoving(){

		bulletSourceInstance = Instantiate (_bulletSourcePrefab) as GameObject;
		bulletSourceInstance.transform.position = this.transform.position;

		//球の作成点までの距離
		Vector3 tmpV = (_createBulletPosi - this.transform.position) / _bulletSourceMovingTotalFrame;

		for (int loopI = 0; loopI < _bulletSourceMovingTotalFrame; loopI++) {
			bulletSourceInstance.transform.position += tmpV;

			yield return new WaitForSeconds (0.001f);
		}

		Destroy (bulletSourceInstance);

		//ループ
		StartCoroutine( magicSouceMoving() );
	}
}
