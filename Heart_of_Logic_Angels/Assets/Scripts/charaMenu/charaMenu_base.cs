using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]
public class charaMenu_base : MonoBehaviour {

	private Transform thisPosition;
	private SpriteRenderer thisSpriteR;
	private AudioSource thisAudioS;
	private Animator thisAnime;
	
	public float scrollInDelay;
	public float closeFlag;

	public int thisButtonType;

	private float movingFrame = 5;
	
	private allChara parentChara;
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

		Vector3 tmpV = new Vector3 (-0.2f, 0f, 0f);
		tmpV = tmpV + thisPosition.localPosition ;

		this.gameObject.transform.localPosition = tmpV;

		thisSpriteR = this.gameObject.GetComponentInChildren<SpriteRenderer>();

		Color tmpC = thisSpriteR.color;
		tmpC.a = 1f;
		thisSpriteR.color = tmpC;


	}
	
	// Update is called once per frame
	void Update () {
		if (scrollInDelay < 0) {
			if (movingFrame > 0) {
					movingFrame -= 1;
					Vector3 tmpV = new Vector3 (0.04f, 0f, 0f);
					this.gameObject.transform.localPosition += tmpV;
			}
		} else {
			scrollInDelay -= 1;		
		}
	}

	void OnMouseDown(){
		switch(thisButtonType){
		case 0:
			parentChara.setMode(chatacterMode.Attack);
			parentMenu.setCharaModeIcon(chatacterMode.Attack);
			thisAudioS.Play();
			break;
		case 1:
			parentChara.setMode(chatacterMode.Defence);
			parentMenu.setCharaModeIcon(chatacterMode.Defence);
			thisAudioS.Play();
			break;
		case 2:
			parentChara.setMode(chatacterMode.Move);
			parentMenu.setCharaModeIcon(chatacterMode.Move);
			thisAudioS.Play();
			break;
		case 3:
			parentChara.setMode(chatacterMode.Skill);
			parentMenu.setCharaModeIcon(chatacterMode.Skill);
			break;
		}
		thisAnime.updateMode = AnimatorUpdateMode.UnscaledTime;
	}

	void closeParent(){
		parentMenu.closeMe ();
	}
}
