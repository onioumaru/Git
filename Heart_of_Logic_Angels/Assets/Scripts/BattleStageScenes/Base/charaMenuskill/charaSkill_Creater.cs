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

		switch(argsSkillTgt.charaNo){
		case enumCharaNum.syusuran_02:

			retGO = Instantiate(_charaSkillEffects[1]) as GameObject;
			//親に設定(必須)
			retGO.transform.parent = argsParentTransform;

			retGO.transform.localPosition = Vector3.zero;
			retGO.transform.localRotation = Quaternion.AngleAxis(argsSkillTgt.zAngle, Vector3.forward);

			break;
		case enumCharaNum.enju_01:
		default:
			retGO = Instantiate(_charaSkillEffects[0]) as GameObject;
			//親に設定(必須)
			retGO.transform.parent = argsParentTransform;
			retGO.transform.localPosition = Vector3.zero;

			break;
		}

		return retGO;
	}

}
