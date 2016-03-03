using UnityEngine;
using System.Collections;

public class wolfSkillAnimationPosition : MonoBehaviour {
	private float leftRight;
	private Vector3 targetV3;

	public wolfSkillAttackScript _wSAS;

	public void setTargetPosition(Vector3 argsV){
		targetV3 = argsV;
	}

	public void startXPosition(){
		Hashtable table = new Hashtable();      // ハッシュテーブルを用意

		Vector3[] paths = new Vector3[5];
		
		paths[0] = new Vector3(this.transform.position.x, this.transform.position.y + 1f, 0f);
		paths[1] = new Vector3(this.transform.position.x, this.transform.position.y + 1f, 0f);
		paths[2] = new Vector3(targetV3.x, targetV3.y, 0f);
		paths[3] = this.transform.parent.position;
		paths[4] = this.transform.parent.position;

		//table.Add("islocal", true);
		table.Add("loopType", iTween.LoopType.none);
		table.Add("path", paths);
		table.Add("time", 3f);

		iTween.MoveTo (this.gameObject, table);

		StartCoroutine ( resetPosition() );
	}

	public void startSkillAction(){
		_wSAS.skillAttack ();
	}

	IEnumerator resetPosition(){
		yield return new WaitForSeconds (3.2f);
		this.transform.localPosition = Vector3.zero;
	}
}
