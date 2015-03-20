using UnityEngine;
using System.Collections;

public class torchBase : MonoBehaviour {
	public GameObject _torchFire;

	private float dealDamage = 5f;

	// Use this for initialization
	void Start () {
		StartCoroutine (selfRotasion());
		StartCoroutine (mainLoop ());
	}

	public void setDealDamade(float argsDamage){
		this.dealDamage = argsDamage;
	}

	IEnumerator selfRotasion(){
		float angle = 0;

		while (true) {
			yield return new WaitForSeconds (0.001f);

			this.transform.rotation *= Quaternion.AngleAxis(300f * Time.deltaTime, Vector3.forward);
		}
	}


	IEnumerator mainLoop(){
		for (int i = 0; i < 6; i++){
			yield return new WaitForSeconds (1f);
			
			GameObject tmpGO = Instantiate(_torchFire);
			tmpGO.GetComponentInChildren<damageErea>().setDealDamage(this.dealDamage);
			
			Vector3 tmpV = this.transform.position + (Vector3.up * 0.5f);
			tmpGO.transform.position = tmpV ;
			
			tmpGO.transform.parent = this.transform;
		}
		
		yield return new WaitForSeconds (30f);

		Destroy(this.gameObject);
	}

}
