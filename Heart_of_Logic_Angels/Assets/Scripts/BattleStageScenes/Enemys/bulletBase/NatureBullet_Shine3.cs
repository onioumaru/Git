using UnityEngine;
using System.Collections;

public class NatureBullet_Shine3 : MonoBehaviour {
	public GameObject _childBullet;
	public float genarateCycleSec = 1f;
	public float _rotateAngle = 0.3f;

	private Coroutine genarateLoopCor;

	private float dealDamege;

	// Use this for initialization
	void Start () {
		StartCoroutine ( selfRotate() );
		genarateLoopCor = StartCoroutine ( genarateLoop() );
	}

	IEnumerator selfRotate(){
		while (true) {
			this.transform.Rotate (new Vector3(0,0, _rotateAngle * Time.deltaTime * 35f)); //Quaternion.AngleAxis (5f, Vector3.forward)
			//Debug.Log(Time.deltaTime);
			yield return new WaitForSeconds (0.0001f);
		}
	}

	IEnumerator genarateLoop(){
		GameObject tmpGO = null;

		while (true) {
			for (int loopI = 0; loopI < 6; loopI++) {
				tmpGO = Instantiate(_childBullet) as GameObject;
				tmpGO.transform.position = this.transform.position;

				tmpGO.transform.parent = this.transform;
				tmpGO.transform.rotation = Quaternion.AngleAxis(60f * loopI ,Vector3.forward);

				tmpGO.GetComponent<simpleMovingScript> ().setSimpleMovingUp ();

				tmpGO.GetComponent<bulletBase_CannotDestory> ().setDealDamage(dealDamege);
			}

			yield return new WaitForSeconds (genarateCycleSec);
		}
	}

	public void stopGenarate(){
		StopCoroutine (genarateLoopCor);
	}

	public void setDealDamege(float argsDam){
		dealDamege = argsDam;
	}
}
