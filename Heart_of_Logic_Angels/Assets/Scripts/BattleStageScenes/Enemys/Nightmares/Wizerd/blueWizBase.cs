using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class blueWizBase : MonoBehaviour {
	public GameObject _Bullet;
	public float _bulletSpeed;

	private List<Vector2> warpPointList;
	private GameManagerScript gms;

	private float animationWait = 3f;
	private float moveWait = 3f;
	//private float _animationWait;


	// Use this for initialization
	void Start () {
		warpPointList = new List<Vector2>();
		gms = GameManagerGetter.getGameManager ();

		this.setPositionPaturn ();

		StartCoroutine (mainLoop());
	}

	void setPositionPaturn(){
		for (int i =0; i < 4; i++) {
			Vector2 tmpV2 = new Vector2(i-1, 0);
			warpPointList.Add(tmpV2);
		}
	}

	IEnumerator mainLoop(){
		while (true) {
			//yield return new WaitForSeconds(0.001f);

			//this.transform.Rotate

			//animation Wait
			yield return new WaitForSeconds(animationWait);
			
			this.shotBullet();
			
			yield return new WaitForSeconds(1f);
			
			this.shotBullet();
			
			yield return new WaitForSeconds(1f);
			
			this.shotBullet();

			yield return new WaitForSeconds(moveWait);
			
			this.warpToPoint();
		}
	}

	void warpToPoint(){
		Vector2 tmpV2 = warpPointList [0];
		warpPointList.RemoveAt (0);

		//Debug.Log (tmpV2);
		this.transform.position = tmpV2;

		warpPointList.Add (tmpV2);
	}

	void shotBullet(){
		GameObject nearChara = gms.getMostNearCharacter (this.transform.position);
		if (nearChara == null) {return;}

		Vector3 tgtVct = nearChara.transform.position - this.transform.position;
		GameObject tmpBullet = Instantiate (_Bullet, this.transform.position, Quaternion.identity) as GameObject;

		Vector2 tmpV2 = tgtVct.normalized * _bulletSpeed;
		//Debug.Log (tmpV2);

		// todo

		//tmpBullet.GetComponent<bulletBase_CannotDestory> ().setRigidVelocity (tmpV2);
		tmpBullet.GetComponent<bulletBase_CannotDestory> ().setDealDamage (1f);
	}
}
