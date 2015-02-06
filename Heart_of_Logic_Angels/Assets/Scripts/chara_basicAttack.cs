using UnityEngine;
using System.Collections;

public class chara_basicAttack : MonoBehaviour {
	
	public float attackDeley = 0.5f;
	private bool deleyFlag = false;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerStay2D(Collider2D c){
		if (deleyFlag == false) {
				if (c.gameObject.name != "AttackErea") {
					//Attack erea でない

					c.gameObject.GetComponent<allEnemy> ().setDamage (2);

					deleyFlag = true;

					StartCoroutine (this.attackDeleyClearer());
				}
			}
	}

	IEnumerator attackDeleyClearer(){
		yield return new WaitForSeconds(attackDeley);
		deleyFlag = false;
	}

}
