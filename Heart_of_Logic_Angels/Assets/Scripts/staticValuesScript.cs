using UnityEngine;
using System.Collections;

public class staticValuesScript : MonoBehaviour {
	private storyProgress thisStoryProgress;

	// Use this for initialization
	void Start () {
		//for debug 初期データ
		thisStoryProgress = new storyProgress ();
	}

	public storyProgress getStoryProgress(){
		return thisStoryProgress;
	}
}
