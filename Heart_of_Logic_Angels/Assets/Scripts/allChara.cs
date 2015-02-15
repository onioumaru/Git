using UnityEngine;
using System.Collections;

// Rigidbody2Dコンポーネントを必須にする
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class allChara : MonoBehaviour {

	//以下、開始時にSetが必要
	// 死亡時エフェクトのPrefab
	public GameObject dyingAnimation;
	public GameObject thisCharaFlag;

	//静的に設定済み。Prefabs上でアタッチ
	public GameObject bitmapFontBasePrefab;
	
	//以下設定不要、自動Set
	public GameManagerScript gmScript;
	public charaUserStatus thisChara;
	
	//スクリプト起動時はTrue（停止）にしておく事
	public bool stopFlag;

	//private
	private cameraTrackerScript cmrTracker;
	private Animator thisAnimeter;
	private AudioSource thisAudio;

	//この値が0になるまで、硬直時間とする
	public int movingFreezeCnt = 0;
	
	float offsetX = -0.28f;
	float offsetY = 0f;

	
	// Use this for initialization
	void Start () {
		gmScript = GameObject.Find("GameManager").GetComponentInChildren<GameManagerScript>();
		cmrTracker = GameObject.Find ("CameraTracker").GetComponentInChildren<cameraTrackerScript> ();

		thisAnimeter = this.gameObject.GetComponentInChildren<Animator>();
		thisAudio = this.gameObject.GetComponentInChildren<AudioSource>();

		thisChara = new charaUserStatus (10 , 1f);
	}

	// Update is called once per frame
	void Update () {
		if (stopFlag == true) {
			Vector2 tmpVct = new Vector2(0,0);
			rigidbody2D.velocity = tmpVct;
			
		} else {
			Vector3 offsetP = new Vector3(thisCharaFlag.transform.position.x + offsetX, thisCharaFlag.transform.position.y + offsetY);
			
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
				rigidbody2D.velocity = tmpV.normalized * thisChara.battleStatus.thisInfo.movingSpeedMagn ;
			}
		} 
	}


	//


	//ダメージ処理
	public void setDamage(int argsInt){
		GameObject retObj = Instantiate (bitmapFontBasePrefab, this.transform.position, this.transform.rotation) as GameObject;
		retObj.GetComponentInChildren<common_damage> ().showDamage (this.transform, argsInt);
		thisAnimeter.SetTrigger ("gotoDamage");

		thisChara.nowHP -= argsInt;
		
		if (thisChara.nowHP <= 0) {
			this.removedMe(this.transform);
		}
	}
	
	//死亡時処理
	public void removedMe(Transform origin){
		//カメラの追従はリセット
		cmrTracker.setCharaTrackReset();

		Instantiate (dyingAnimation, origin.position, origin.rotation);
		Destroy (this.gameObject);
		Destroy (thisCharaFlag);
	}

	
	public void getExp(float argsExp){
		thisChara.totalExp += argsExp;

		retTypeExp tmpExp = gmScript.calcLv (thisChara.totalExp);
		
		if(thisChara.nowLv != tmpExp.Lv){
			//
			thisChara.nowLv = tmpExp.Lv;
			thisAudio.Play();
		}
	}
	
	public float getCharaDamage(){
		return thisChara.nowLv * 2f;
	}

	public void setMode(string argsStr){
		thisChara.setMode(argsStr);
	}
}
