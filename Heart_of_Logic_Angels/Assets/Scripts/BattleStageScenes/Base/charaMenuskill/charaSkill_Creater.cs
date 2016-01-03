using UnityEngine;
using System.Collections;

public class charaSkill_Creater : MonoBehaviour {

	public GameObject[] _charaSkillEffects;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public GameObject instantiateSkillEffect(Transform argsParentTransform, skillTargetInfo argsSkillTgt){
		// スキルエフェクトのクローンを作成し
		// 呼び出し元を親に設定
		// 以降は charaSkill_FreezeManager が、自分で時間とステータスを管理する
		//この関数は、クローンを作るだけ

		GameObject retGO = null;
		int tmpIndex = (int)argsSkillTgt.charaNo;

		switch(argsSkillTgt.charaNo){
		case enumCharaNum.mokuren_06:
		case enumCharaNum.akane_04:
		case enumCharaNum.syusuran_02:
		case enumCharaNum.hiragi_09:
			//指向性があるスキル
			retGO = Instantiate(_charaSkillEffects[tmpIndex]) as GameObject;
			//親に設定(必須)
			retGO.transform.parent = argsParentTransform;

			retGO.transform.localPosition = Vector3.zero;
			retGO.transform.localRotation = Quaternion.AngleAxis(argsSkillTgt.zAngle, Vector3.forward);

			break;

		default:
			//自分中心スキル
			retGO = Instantiate(_charaSkillEffects[tmpIndex]) as GameObject;

			//親に設定(必須)
			retGO.transform.parent = argsParentTransform;
			retGO.transform.localPosition = Vector3.zero;
			
			break;
		}

		return retGO;
	}

}
