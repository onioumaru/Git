using UnityEngine;
using System.Collections;

public class charaSkill_okCancel : MonoBehaviour {

	public bool cancelFlag = false;
	
	void OnMouseDown(){
		charaSkill_TargetMain tmpScr = this.transform.parent.GetComponent<charaSkill_TargetMain> ();
		tmpScr.buttonActionBase (cancelFlag);
	}
}
