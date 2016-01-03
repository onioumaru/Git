using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class charaSkillColliderSakuraScript : MonoBehaviour {
	private allCharaBase thisChara;
	private int intervalCnt;
	public GameObject _crystal;

	private List<charaSkill_SakuraClystal> clystalList;
	private int clystalCnt = 3;

	void Start(){
		clystalList = new List<charaSkill_SakuraClystal> ();
		GameObject tmpGO;

		for (int loopInt = 0; loopInt < clystalCnt; loopInt++) {
			tmpGO = Instantiate (_crystal);

			tmpGO.transform.localPosition = Random.insideUnitCircle;
			tmpGO.transform.parent = this.transform;

			clystalList.Add(tmpGO.GetComponent<charaSkill_SakuraClystal> ());
		}

		this.transform.parent = null;
	}

	void Update(){
		this.transform.position = thisChara.transform.position;
	}

	void OnTriggerStay2D(Collider2D c){
		if (commonCharaAttack.checkTargetCollider(c) == false) {
			return;
		}

		intervalCnt += 1;
		if (intervalCnt == 30) {
			intervalCnt = 0;
		}

		int tmpDm;
		tmpDm = Mathf.FloorToInt(thisChara.getCharaDamage()) + Mathf.FloorToInt(Random.value * 2);
		tmpDm = 1;
		commonCharaAttack tmpAtk = new commonCharaAttack (c.gameObject, thisChara.gameObject, tmpDm);

		switch (intervalCnt){
		case 0:
			clystalList[0].setAttack (c.transform.position, tmpAtk);
			break;
		case 5:
			clystalList[1].setAttack (c.transform.position, tmpAtk);
			break;
		case 10:
			clystalList[2].setAttack (c.transform.position, tmpAtk);
			break;
		}
	}

	public void setBaseCharaScript(allCharaBase argsChara){
		thisChara = argsChara;

		Animator tmpAnime = thisChara.gameObject.GetComponentInChildren<Animator>();
		tmpAnime.SetTrigger("gotoSkillDo");
		//Debug.Log (thisChara.thisChara.charaNo);
	}
}
