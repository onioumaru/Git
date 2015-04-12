using UnityEngine;
using System.Collections;

public class charaIconset_modeIcon : MonoBehaviour {
	public Sprite iconAtk;
	public Sprite iconDef;
	public Sprite iconMov;
	public Sprite iconSkill;
	
	private SpriteRenderer thisSR;
	private charaIconsetManager thisManager;
	private characterMode nowMode;

	void Start(){
		thisSR = this.GetComponentInChildren<SpriteRenderer>();
		thisManager = this.transform.parent.GetComponent<charaIconsetManager>();
	}
	
	void Update () {
		characterMode tmpMode = thisManager.getThisCharaMode ();

		if (this.nowMode == tmpMode) { return;}

		this.setModeIcon (tmpMode);
	}

	public void setModeIcon(characterMode argsMode){

		nowMode = argsMode;

		switch (argsMode) {
		case characterMode.Attack:
			thisSR.sprite = iconAtk;
			break;
		case characterMode.Defence:
			thisSR.sprite = iconDef;
			break;
		case characterMode.Move:
			thisSR.sprite = iconMov;
			break;
		case characterMode.Skill:
			thisSR.sprite = iconSkill;
			break;
		}
	}
}
