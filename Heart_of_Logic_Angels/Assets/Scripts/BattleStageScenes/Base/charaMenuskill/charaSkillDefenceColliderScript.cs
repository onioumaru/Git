using UnityEngine;
using System.Collections;

public class charaSkillDefenceColliderScript : MonoBehaviour {
	private allCharaBase thisChara;

	void Start(){
		switch (thisChara.thisChara.charaNo) {
		case enumCharaNum.houzuki_05:
			//リジェネのセット
			thisChara.setRegenerate_hozukiSkill (150f);

			break;
		case enumCharaNum.suzusiro_03:
			//接触したキャラを無敵にする
			thisChara.setUnbreakable_suzuSkill (true);		//自分のコライダーは検知しないので手動

			break;
		}
	}

	void OnTriggerEnter2D(Collider2D c){
		//OnCollisionStay2D
		//OnTriggerEnter2D

		//味方にステータス付与するときは、攻撃と同じ構造で各属性を与える

		if (c.gameObject.name.Substring(0, 9) != "charaBase") {
			//charaBaseでない
			return ;
		}

		switch (thisChara.thisChara.charaNo) {
		case enumCharaNum.houzuki_05:
			c.transform.GetComponent<allCharaBase> ().setRegenerate_hozukiSkill (150f);

			break;
		case enumCharaNum.suzusiro_03:
			//接触したキャラを無敵にする
			c.transform.GetComponent<allCharaBase> ().setUnbreakable_suzuSkill (true);

			break;
		}
	}

	void OnTriggerExit2D(Collider2D c){
		//味方にステータス付与するときは、攻撃と同じ構造で各属性を与える

		if (c.gameObject.name.Substring(0, 9) != "charaBase") {
			//charaBaseでない
			return ;
		}

		switch (thisChara.thisChara.charaNo) {
		case enumCharaNum.suzusiro_03:
			//出たキャラを無敵解除
			c.transform.GetComponent<allCharaBase>().setUnbreakable_suzuSkill(false);
			
			break;
		}
	}

	void OnDestroy () {

		switch (thisChara.thisChara.charaNo) {
		case enumCharaNum.suzusiro_03:
			GameManagerGetter.getGameManager ().clearAllCharaUnbreakable ();

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
