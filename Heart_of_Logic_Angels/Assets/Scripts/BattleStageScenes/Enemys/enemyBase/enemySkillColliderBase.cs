using UnityEngine;
using System.Collections;

public class enemySkillColliderBase : MonoBehaviour {
	private float dealDamade = 99999f;

	// Use this for initialization
	void Start () {
	
	}

	public void setDealDamage(float argsDamage){
		dealDamade = argsDamage;
	}

	void OnTriggerEnter2D(Collider2D c){
	//void OnTriggerStay2D(Collider2D c){
		if (c.gameObject.name.Substring (0, 9) == "charaBase") {
			
			allCharaBase charaB = c.gameObject.GetComponent<allCharaBase> ();
			charaB.setDamage(dealDamade);

			StartCoroutine( this.colliderDisabled() );
		}
	}

	IEnumerator colliderDisabled(){
		yield return null;

		this.GetComponent<Collider2D>().enabled = false;
	}
}
