using UnityEngine;
using System.Collections;

public class mob_basicAttack : MonoBehaviour {

	private int attackDeleyCnt = 0;

	private int attackDeley = 30;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


	}

	void OnTriggerStay2D(Collider2D c){

		if (attackDeleyCnt == 0) {
			//ディレイが終わっている場合

			if (c.gameObject.name != "AttackErea") {
				Debug.Log (c.gameObject.name);
				//everyFlameCollider.Add(c);
				attackDeleyCnt = attackDeley;

			}
		}
	}
}
