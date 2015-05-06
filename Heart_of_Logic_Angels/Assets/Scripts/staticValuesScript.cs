using UnityEngine;
using System.Collections;

public class staticValuesScript : MonoBehaviour {
	private storyProgress thisStoryProgress;

	public storyProgress getStoryProgress(){
		if (thisStoryProgress == null) {
			//for debug 初期データ
			thisStoryProgress = new storyProgress ();
		}

		return thisStoryProgress;
	}
}
