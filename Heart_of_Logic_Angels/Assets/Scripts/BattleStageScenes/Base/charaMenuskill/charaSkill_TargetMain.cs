using UnityEngine;
using System.Collections;

public class charaSkill_TargetMain : MonoBehaviour {
	
	public GameObject _SkillTarget_Arrow;
	public GameObject _SkillTarget_Circle;

	private skillTargetInfo thisTrgt;
	private GameObject thisSkilltgt;

	
	private allCharaBase parentChara;
	private charaMenu_parent parentMenu;

	// Use this for initialization
	void Start () {
		thisTrgt = new skillTargetInfo();

		thisTrgt.worldPosision = new Vector3 (0, 0, 0);
		thisTrgt.zAngle = 0f;

		parentMenu = this.transform.parent.GetComponent<charaMenu_parent>();
		parentChara = parentMenu.getAllCharaScript (); 
		thisTrgt.charaNo = parentChara.thisChara.charaNo;

		Vector3 tmpV_zero = new Vector3(0,0,0);

		switch (parentChara.thisChara.charaNo) {
			case enumCharaNum.syusuran_02:
			case enumCharaNum.akane_04:
			case enumCharaNum.mokuren_06:
			case enumCharaNum.sion_08:
			case enumCharaNum.hiragi_09:
					
				thisSkilltgt = Instantiate(_SkillTarget_Arrow) as GameObject;

				thisSkilltgt.transform.parent = this.transform;
				thisSkilltgt.transform.localPosition = tmpV_zero;

				thisTrgt.zAngle = thisSkilltgt.transform.localRotation.z;

				break;

			default:
				
				thisSkilltgt = Instantiate(_SkillTarget_Circle) as GameObject;
				
				thisSkilltgt.transform.parent = this.transform;
				thisSkilltgt.transform.localPosition = tmpV_zero;
				
				break;
		}
	}

	
	void OnMouseDown(){
		Vector3 firstMouseDown = Camera.main.ScreenToWorldPoint (Input.mousePosition) - this.transform.position;

		switch (parentChara.thisChara.charaNo) {
		case enumCharaNum.syusuran_02:
		case enumCharaNum.akane_04:
		case enumCharaNum.mokuren_06:
		case enumCharaNum.sion_08:
		case enumCharaNum.hiragi_09:
			Vector3 tmpV = new Vector3( firstMouseDown.x, firstMouseDown.y,0f);
			Vector3 tmpRoteAxis = new Vector3(0,0,1);

			float tmpFl = Vector3.Angle(Vector3.right, tmpV);
			float tmpFl2 = Vector3.Angle(Vector3.up, tmpV);

			if (tmpV.y < 0){
				tmpFl = tmpFl * -1f;
			}

			if (tmpV.x > 0){
				tmpFl2 = tmpFl2 * -1f;
			}

			thisTrgt.zAngle = tmpFl;

			thisSkilltgt.transform.rotation = Quaternion.AngleAxis(tmpFl2, tmpRoteAxis);

			break;
		default:
			//自分中心円なのでクリックされても何もしない
			break;
		}
	}

	public void buttonActionBase(bool cancelFlag){

		if (cancelFlag) {
			//true
			parentMenu.closeMe();
		} else {
			//スキル情報を戻す
			parentChara.setSkillTatgetInfo(thisTrgt);
			parentChara.setMode(characterMode.Skill);
			
			//thisAnimetor.SetTrigger("gotoAttack");
			Animator tmpAnime = parentChara.gameObject.GetComponentInChildren<Animator>();
			tmpAnime.SetTrigger("gotoSkill");

			parentMenu.setCharaModeIcon(characterMode.Skill);
			parentMenu.closeMe();
		}
	}
}

public class skillTargetInfo{
	public Vector3 worldPosision;
	public float zAngle;
	public enumCharaNum charaNo;
}