using UnityEngine;
using System.Collections;

public class tamanekoWhiteScript : MonoBehaviour {
	/// <summary>
	/// 死んだときに黒を成長させる
	/// </summary>
	void OnDestroy () {
		tamanekoBlackScript[] tmpOs = GameObject.FindObjectsOfType<tamanekoBlackScript> ();

		foreach (tamanekoBlackScript tmpScrpt in tmpOs) {
			tmpScrpt.tamanekoBlackGrowing ();
		}
	}
}

/*
public enum tamanekoType{
	tamanekoType_01Growing,
	tamanekoType_02mirrew,

}
*/