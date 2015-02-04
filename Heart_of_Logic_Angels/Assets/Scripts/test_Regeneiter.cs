using UnityEngine;
using System.Collections;

public class test_Regeneiter : MonoBehaviour {

	public GameObject mobEnemy;

	// Use this for initialization
	IEnumerator Start () {
		while (true) {
			GameObject retMobs = Instantiate (mobEnemy, this.transform.position, this.transform.rotation) as GameObject;

			retMobs.GetComponentInChildren<allEnemy>().setMoving(1, 0.5f);

			yield return new WaitForSeconds (2f);
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
