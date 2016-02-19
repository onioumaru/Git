using UnityEngine;
using System.Collections;

public class genaratorKicker : MonoBehaviour {
	public genaratorDaemonScript _gDS;


	/// <summary>
	/// アニメーターからキックされる
	/// </summary>
	public void genarateStart(){
		_gDS.genarateMonster ();
	}
}
