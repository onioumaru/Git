using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class standingCharaImageParent : MonoBehaviour {
	public GameObject _UnderDrawParent;
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
		//カラーは先に定義
		Color tmpC = new Color(0.5f, 0.5f, 0.5f);
		if (isSpot) {
			tmpC = Color.white;
		}
			
		if (this.transform.childCount < 1) {
			//子がいない場合、処理不要と考える
			//Debug.Log ("spot return");
			return;
				}
			
		if (isSpot == false) {
			//このキャラ表示ポジションを後ろに
			this.transform.SetAsFirstSibling ();
		}

		//子のImageの色は先につけてしまう
		Image[] tmpImgs = this.transform.GetComponentsInChildren<Image> ();
		foreach (Image tmpI in tmpImgs) {
			tmpI.color = tmpC;
		}

		/*
		standingCharaImageS tmpS = childNode.GetComponent<standingCharaImageS>();
		if (tmpS != null) {
			//standingCharaImageS　が付いていない、説明用画像が乗っている場合、ここを通る
			tmpS.setSelfSpot (isSpot);
		}
		*/

	}


	public void initCharactorImage(string argsNo){
		//子は消す
		this.destoryChild ();

		//Debug.Log (argsNo);

		string fFullPath = "";

		//4桁固定

		string tmpStr = argsNo.Substring (0, 1);

		if (tmpStr != "0" && tmpStr != "1") {
			
			switch (argsNo) {	
			case "2001":
				//アルルーナ
				fFullPath = "pictChractorStanding/20_1_aruru-na";
				break;
			case "2002":
				//アルルーナ
				fFullPath = "pictChractorStanding/20_2_aruru-na";
				break;
			case "2003":
				//アルルーナ
				fFullPath = "pictChractorStanding/20_3_aruru-na";
				break;

			case "2004":
				//アルルーナ
				fFullPath = "pictChractorStanding/20_4_aruru-na";
				break;

			case "9200":
				fFullPath = "pictChractorStanding/92_warupiyo";
				break;
			case "9201":
				fFullPath = "pictChractorStanding/92_warupiyo_01";
				break;
			case "9202":
				fFullPath = "pictChractorStanding/92_warupiyo_02";
				break;

			case "9300":
				fFullPath = "pictChractorStanding/93_mosaicMonster";
				break;
			case "9400":
				fFullPath = "pictChractorStanding/94_turo02";
				break;
			case "9500":
				fFullPath = "pictChractorStanding/95_tuto01";
				break;
			}
			
			childNode = (GameObject)Instantiate( Resources.Load(fFullPath));
			childNode.transform.parent = this.transform;

			return;
		}


		//標準2キャラ

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

		case "05":
			fFullPath = "pictChractorStanding/05_mokuren";
			break;

		case "06":
			fFullPath = "pictChractorStanding/06_sakura";
			break;

		case "07":
			fFullPath = "pictChractorStanding/07_sion";
			break;

		case "08":
			fFullPath = "pictChractorStanding/08_hiragi";
			break;

		case "12":
			fFullPath = "pictChractorStanding/02_suzusiro_noGlass";
			break;

		default:
			Debug.Log ("Character Number Not Found");
			fFullPath = "pictChractorStanding/00_enjyu/00_enjyu";

			break;
				}

		childNode = (GameObject)Instantiate( Resources.Load(fFullPath));
		childNode.transform.parent = this.transform;


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

		//childNode.transform.localPosition = Vector3.zero;
		//自分で補正するので位置補正は必要ない
	}
}
