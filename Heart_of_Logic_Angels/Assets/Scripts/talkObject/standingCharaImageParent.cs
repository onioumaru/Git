using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class standingCharaImageParent : MonoBehaviour {
	private GameObject childNode;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void destoryChild(){
		// すべての子オブジェクトを取得
		foreach ( Transform n in this.transform )
		{
			GameObject.Destroy(n.gameObject);
		}
	}


	public void setThisChildsSpot(bool isSpot){

		if (this.transform.childCount < 1) {
			//子がいない場合、処理不要と考える
			//Debug.Log ("spot return");
			return;
				}

		if (isSpot == false) {
			//このキャラ表示ポジションを後ろに
			this.transform.SetAsFirstSibling ();
		}

		standingCharaImageS tmpS = childNode.GetComponent<standingCharaImageS>();
		tmpS.setSelfSpot (isSpot);

	}


	public void initCharactorImage(string argsNo){
		//子は消す
		this.destoryChild ();

		//Debug.Log (argsNo);

		string fFullPath = "";

		switch (argsNo.Substring(0,2)) {
		case "00":
			fFullPath = "pictChractorStanding/00_enjyu";
			break;
			
		case "01":
			fFullPath = "pictChractorStanding/01_syusuran";
			break;
			
		case "02":
			fFullPath = "pictChractorStanding/02_suzusiro";
			break;

		case "03":
			fFullPath = "pictChractorStanding/03_akane";
			break;
			
		case "04":
			fFullPath = "pictChractorStanding/04_hozuki";
			break;
			
		case "12":
			fFullPath = "pictChractorStanding/02_suzusiro_noGlass";
			break;


		case "95":
			fFullPath = "pictChractorStanding/95_tuto01";
			break;

		default:
			Debug.Log ("Character Number Not Found");
			fFullPath = "pictChractorStanding/00_enjyu/00_enjyu";

			break;
				}

		childNode = (GameObject)Instantiate( Resources.Load(fFullPath));


		
		switch (argsNo.Substring (0, 2)) {
		case "95":
			//キャラクター以外の画像は、ポジションをセットして即終了
			childNode.transform.parent = this.transform;
			childNode.transform.localPosition = new Vector3(0f ,70f ,0f);
			childNode.transform.localScale = Vector3.one;

			return;

			break;
		}


		standingCharaImageS tmpScript = childNode.GetComponent<standingCharaImageS>();

		if (argsNo.Substring (2, 2) == "00") {
			//00の場合はdiff分を消す
			tmpScript.setFaceBlank();

			//ベースに顔が乗っていないキャラは初期顔セット
			switch (argsNo.Substring(0,2)) {
			case "04":
				tmpScript.setFaceDiff ("00");
				break;
			}

		}else {
			//Debug.Log (argsNo.Substring (2, 2));
			tmpScript.setFaceDiff (argsNo.Substring (2, 2));
		}

		childNode.transform.parent = this.transform;
		//childNode.transform.localPosition = Vector3.zero;
		//自分で補正するので位置補正は必要ない
	}
}
