using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class charaMenu_base : MonoBehaviour {
	private Transform thisPosition;
	private SpriteRenderer thisSpriteR;
	private AudioSource thisAudioS;
	private Animator thisAnime;
	
	public float scrollInDelay;
	public float closeFlag;

	public int thisButtonType;

	private float movingFrameCnt = 5;
	
	private allCharaBase parentChara;
	private charaMenu_parent parentMenu;

	// Use this for initialization
	void Start () {
		//
		parentMenu = this.gameObject.GetComponentInParent<charaMenu_parent> ();
		parentChara = parentMenu.getAllCharaScript ();
		thisAudioS = this.gameObject.GetComponentInParent<AudioSource> ();
		thisAnime = this.gameObject.GetComponentInParent<Animator> ();

		//

		thisPosition = this.gameObject.transform;

		Vector3 tmpV = new Vector3 (-10f, 0f, 0f);
		tmpV = tmpV + thisPosition.localPosition ;

		this.gameObject.transform.localPosition = tmpV;

		thisSpriteR = this.gameObject.GetComponentInChildren<SpriteRenderer>();

		Color tmpC = thisSpriteR.color;
		tmpC.a = 1f;
		thisSpriteR.color = tmpC;

		//スキルボタン、且つクールタイムが終わっていない場合、ボタンは削除される
		if (thisButtonType == 3) {
			float tmpF = parentChara.getRestCoolTime();
			if (tmpF > 0.01f){
				Destroy(this.gameObject); 
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (scrollInDelay < 0) {
			if (movingFrameCnt > 0) {
					movingFrameCnt -= 1;
					Vector3 tmpV = new Vector3 (2f, 0f, 0f);
					this.gameObject.transform.localPosition += tmpV;
			}
		} else {
			scrollInDelay -= 1;		
		}
	}

	void OnMouseDown(){
		parentMenu.buttonActionFlag = true;

		switch(thisButtonType){
		case 0:
			parentChara.setMode(characterMode.Attack);
			parentMenu.setCharaModeIcon(characterMode.Attack);
			thisAudioS.Play();
			break;
		case 1:
			parentChara.setMode(characterMode.Defence);
			parentMenu.setCharaModeIcon(characterMode.Defence);
			thisAudioS.Play();
			break;
		case 2:
			parentChara.setMode(characterMode.Move);
			parentMenu.setCharaModeIcon(characterMode.Move);
			thisAudioS.Play();
			break;
		case 3:
			//アニメーション終了時にイベント発生
			break;
		}
		thisAnime.updateMode = AnimatorUpdateMode.UnscaledTime;
	}

	public void closeParent(){
		parentMenu.closeMe ();
	}

	public void switchSkillTarget(){
		parentMenu.switchSkillTargetMode();
	}
}
