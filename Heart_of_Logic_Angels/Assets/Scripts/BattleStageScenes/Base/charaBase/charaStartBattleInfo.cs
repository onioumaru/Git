using UnityEngine;
using System.Collections;

public class charaStartBattleInfo {
	private Vector3[] retPosition;

	public charaStartBattleInfo(sceneChangeValue argsVal){
		retPosition = new Vector3[8];

		switch (argsVal.sceneFileName) {
		case "0-1-0-0":
			//エンジュしかいない為、他省略
			retPosition[0] = new Vector3(0f, 0f, 0f);
			break;
		case "0-2-0-0":
			//エンジュしかいない為、他省略
			retPosition[0] = new Vector3(-2f, -0.5f, 0f);
			break;
		}
	}
	/// <summary>
	/// キャラのインデックス番号を渡す事
	/// </summary>
	public Vector3 getCharaPosition(int charaIndex){
		return retPosition[charaIndex];
	}
	/// <summary>
	/// キャラのインデックス番号を渡す事
	/// 旗の位置移動位置はずれている為、こっちが呼ばれた場合は差分込みで返す
	/// </summary>
	public Vector3 getFlagPosition(int charaIndex){

		float tmpF = retPosition[charaIndex].x + 0.28f ;

		return new Vector3(tmpF, retPosition[charaIndex].y, retPosition[charaIndex].z);
	}

}
