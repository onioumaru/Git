using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManagerScript : MonoBehaviour {

	//ArrayList playerCharaList = new ArrayList();

	private List<float> expList;


	// Use this for initialization
	void Start () {
		this.initExpMaster ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public retTypeExp calcLv(float argsExp){
		retTypeExp retExp = new retTypeExp ();

		for (int i = 0; i < 200; i++) {
			if (expList[i] > argsExp){
				//対象の経験値より小さい場合,このレベル

				retExp.Lv = i;
				retExp.nextExp = expList[i] - argsExp;

				break;
			}
		}
		return retExp;
	}



	// init

	private void initExpMaster(){
		expList = new List<float>();
		float tmpF = 0f;

		for (int i=0; i < 200; i++) {
			tmpF = (i * i * i)+(37 * i * i)-38;
			expList.Add(tmpF);
		}
	}
}

public class retTypeExp{
	public int Lv;
	public float nextExp;
}

	