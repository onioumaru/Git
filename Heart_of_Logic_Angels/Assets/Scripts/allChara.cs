using UnityEngine;
using System.Collections;

// Rigidbody2Dコンポーネントを必須にする
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class allChara : MonoBehaviour {

	//Basic Para

	public int nowHP;
	public int maxHP;

	public int nowLv=1;

	public string nowMode= "Attack";

	private float totalExp;


	// 


	// 死亡時エフェクトのPrefab
	public GameObject removeCharaPrefab;
	public GameObject bitmapFontBasePrefab;
	public GameManagerScript gmScript;
	
	public GameObject flag_chara11;

	private Animator thisAnimeter;
	private AudioSource thisAudio;


	//この値が0になるまで、硬直時間とする
	public int movingFreezeCnt = 0;

	public bool stopFlag;

	float offsetX = -0.28f;
	float offsetY = 0f;

	
	// Use this for initialization
	void Start () {
		thisAnimeter = this.gameObject.GetComponentInChildren<Animator>();
		gmScript = GameObject.Find("GameManager").GetComponentInChildren<GameManagerScript>();
		thisAudio = this.gameObject.GetComponentInChildren<AudioSource>();
	}

	// Update is called once per frame
	void Update () {
		if (stopFlag == true) {
			Vector2 tmpVct = new Vector2(0,0);
			rigidbody2D.velocity = tmpVct;
			
		} else {
			Vector3 offsetP = new Vector3(flag_chara11.transform.position.x + offsetX, flag_chara11.transform.position.y + offsetY);
			
			Vector3 tmpV = (offsetP - transform.position);

			if (tmpV.x > 0){
				Vector3 tmpScl = new Vector3(-1f, 1f, 1f); 
				this.transform.localScale = tmpScl;
			} else {
				Vector3 tmpScl = new Vector3(1f, 1f, 1f); 
				this.transform.localScale = tmpScl;
			}

			if (tmpV.magnitude < 0.02f){
				stopFlag = true;
			}else{
				rigidbody2D.velocity = tmpV.normalized;
			}
		} 
	}


	public void setDamage(int argsInt){
		GameObject retObj = Instantiate (bitmapFontBasePrefab, this.transform.position, this.transform.rotation) as GameObject;
		retObj.GetComponentInChildren<common_damage> ().showDamage (this.transform, argsInt);
		thisAnimeter.SetTrigger ("gotoDamage");

		nowHP -= argsInt;
		
		if (nowHP <= 0) {
			this.removedMe(this.transform);
		}
	}
	
	public void removedMe(Transform origin){
		Instantiate (removeCharaPrefab, origin.position, origin.rotation);
		Destroy (this.gameObject);
		Destroy (flag_chara11);
	}

	public void getExp(float argsExp){
		this.totalExp += argsExp;

		retTypeExp tmpExp = gmScript.calcLv (this.totalExp);
		
		if(nowLv != tmpExp.Lv){
			//
			this.nowLv = tmpExp.Lv;
			thisAudio.Play();
		}
	}

	public void setMode(string argsStr){
		switch (argsStr) {
		case "Attack":
			nowMode = "Attack";
			break;
		case "Defence":
			nowMode = "Defence";
			break;
		case "Move":
			nowMode = "Move";
			break;
		case "Skill":
			nowMode = "Skill";

			break;
		}
	}

}
