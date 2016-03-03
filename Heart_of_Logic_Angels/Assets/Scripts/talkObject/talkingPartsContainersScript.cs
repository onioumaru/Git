using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class talkingPartsContainersScript : MonoBehaviour {
	public GameObject[] _containerBase;

	public returnTalkingParts getCharaEffect(string argsNum){
		returnTalkingParts retV = new returnTalkingParts();

		/*
		 * 
		string fFullPath = "pictChractorStanding/Eff_abyssCloud";
		GameObject tmpResouce = Resources.Load(fFullPath) as GameObject;
		GameObject tmpGO = (GameObject)Instantiate(tmpResouce);
		*/
		switch (argsNum) {
		case "00":
			//汗
			GameObject[] retGO_00 = new GameObject[] { _containerBase[0] };
			retV.frontPrefas = retGO_00;
			break;
			//
		case "01":
			//汗
			GameObject[] retGO_01 = new GameObject[] { _containerBase[1] };
			retV.frontPrefas = retGO_01;

			break;
		case "02":
			//DarkAura
			GameObject[] retGO_02f = new GameObject[] { _containerBase[3] };
			GameObject[] retGO_02b = new GameObject[] { _containerBase[2] }; 

			retV.frontPrefas = retGO_02f;
			retV.backPrefas = retGO_02b;

			break;
		case "03":
			//
			GameObject[] retGO_03 = new GameObject[] { _containerBase[4] };
			retV.backPrefas = retGO_03;
			
			break;
		case "04":
			//darkCloud
			GameObject[] retGO_04 = new GameObject[] {_containerBase[5]};
			retV.frontPrefas = retGO_04;

			break;

		case "05":
			//きらり
			GameObject[] retGO_05 = new GameObject[] {_containerBase[6]};
			retV.frontPrefas = retGO_05;

			break;

		case "06":
			//あかるいやつ
			GameObject[] retGO_06 = new GameObject[] {_containerBase[7]};
			retV.frontPrefas = retGO_06;

			break;

		case "07":
			//気づき、びっくり
			GameObject[] retGO_07 = new GameObject[] {_containerBase[8]};
			retV.frontPrefas = retGO_07;

			break;

		case "08":
			//おこ
			GameObject[] retGO_08 = new GameObject[] {_containerBase[9]};
			retV.frontPrefas = retGO_08;

			break;
		}

		return retV;
	}
}

public class returnTalkingParts : MonoBehaviour{
	public GameObject[] frontPrefas;
	public GameObject[] backPrefas;
}