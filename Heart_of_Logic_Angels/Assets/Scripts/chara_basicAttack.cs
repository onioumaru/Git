using UnityEngine;
using System.Collections;

public class chara_basicAttack : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerStay2D(Collider2D c){
		if (c.gameObject.name != "AttackErea") {
			//Attack erea でない
			c.gameObject.GetComponent<allEnemy>().removedMe(c.transform);
		}
	}

}
