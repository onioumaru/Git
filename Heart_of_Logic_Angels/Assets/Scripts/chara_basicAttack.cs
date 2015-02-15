using UnityEngine;
using System.Collections;

public class chara_basicAttack : MonoBehaviour {
	
	public float attackDeley = 0.5f;
	private bool deleyFlag = false;

	private Animator thisAnimetor;
	private allChara parentCharaScrpt;

	// Use this for initialization
	void Start () {
		thisAnimetor = this.gameObject.GetComponentInParent<Animator>();
		parentCharaScrpt = this.gameObject.GetComponentInParent<allChara>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerStay2D(Collider2D c){
		if (deleyFlag == false) {
			if (c.gameObject.name != "AttackErea") {
				//Attack erea でない

				thisAnimetor.SetTrigger("gotoAttack");

				int tmpDm = Mathf.FloorToInt(parentCharaScrpt.getCharaDamage()) + Mathf.FloorToInt(Random.value * 4);
				float retExt = c.gameObject.GetComponent<allEnemy> ().setDamage (tmpDm);

				//敵が倒せている場合
				if (retExt > 0f) {
					//Lv Up Check
					parentCharaScrpt.getExp(retExt);
				}

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
