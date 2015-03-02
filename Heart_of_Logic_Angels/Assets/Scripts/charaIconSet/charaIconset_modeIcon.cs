using UnityEngine;
using System.Collections;

public class charaIconset_modeIcon : MonoBehaviour {
	public Sprite iconAtk;
	public Sprite iconDef;
	public Sprite iconMov;
	public Sprite iconSkill;
	
	private SpriteRenderer thisSR;
	private charaIconsetManager thisManager;
	private chatacterMode nowMode;

	void Start(){
		thisSR = this.GetComponentInChildren<SpriteRenderer>();
		thisManager = this.transform.parent.GetComponent<charaIconsetManager>();
	}
	
	void Update () {
		chatacterMode tmpMode = thisManager.getThisCharaMode ();

		if (this.nowMode == tmpMode) { return;}

		this.setModeIcon (tmpMode);
	}

	public void setModeIcon(chatacterMode argsMode){

		nowMode = argsMode;

		switch (argsMode) {
		case chatacterMode.Attack:
			thisSR.sprite = iconAtk;
			break;
		case chatacterMode.Defence:
			thisSR.sprite = iconDef;
			break;
		case chatacterMode.Move:
			thisSR.sprite = iconMov;
			break;
		case chatacterMode.Skill:
			thisSR.sprite = iconSkill;
			break;
		}
	}
}
