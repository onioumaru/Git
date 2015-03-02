using UnityEngine;
using System.Collections;

public class enemy_basicAttack : MonoBehaviour {
	
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
				
				int tmpDm = 1 + Mathf.FloorToInt(Random.value * 4);
				c.gameObject.GetComponent<allCharaBase> ().setDamage (tmpDm);
				
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
