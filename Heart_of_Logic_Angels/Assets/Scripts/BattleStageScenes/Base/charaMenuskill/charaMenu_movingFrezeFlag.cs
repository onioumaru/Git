using UnityEngine;
using System.Collections;

public class charaMenu_movingFrezeFlag : MonoBehaviour {
	public Sprite _offButtonSprite;
	public Sprite _onButtonSprite;

	private allCharaBase parentChara;
	private charaMenu_parent parentMenu;

	private SpriteRenderer thisSR;

	void Start(){
		parentMenu = this.gameObject.GetComponentInParent<charaMenu_parent> ();
		parentChara = parentMenu.getAllCharaScript ();

		thisSR = this.GetComponent<SpriteRenderer> ();

		this.autoSetSprite ();
	}

	void OnMouseDown(){
		//フラグの反転
		parentChara.attackFreezeFlag = !parentChara.attackFreezeFlag;

		this.autoSetSprite ();

		soundManagerGetter.getManager ().playOneShotSound (enm_oneShotSound.nomalButton);
	}

	private void autoSetSprite(){
		if (parentChara.attackFreezeFlag) {
			thisSR.sprite = _onButtonSprite;
		}else {
			thisSR.sprite = _offButtonSprite;
		}
	}
}
