using UnityEngine;
using System.Collections;

public class charaIcon_PageChangeArrow : MonoBehaviour {

	//ツール上からアタッチする事
	public GameObject gameManeger;
		// 1 or -1
	public int scrollVector;
	private GameManagerScript gms;

	// Use this for initialization
	void Start () {
		gms = GameManagerGetter.getGameManager ();
	}

	void OnMouseUp(){
		//gameManagerに丸投げ
		gms.scrollCharaIcons (scrollVector);
	}

}
