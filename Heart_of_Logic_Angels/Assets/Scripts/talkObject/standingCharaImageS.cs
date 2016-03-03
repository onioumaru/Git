using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class standingCharaImageS : MonoBehaviour {
	public Image _childDiffImage;	//
	public Vector3 _thisCharaDefaultLocalPosition;	//初期位置の補正（基本Y軸のみ）

	//イメージはプレハブごとに設定して保存してしまう
	public Sprite _shadowSprite;
	public Sprite[] _childDiffSprite;
	public bool _fadeInAlfaChannelUseF;		// fadein時に顔を表示するか？　首から上のイラストの場合,Falseとする

	public bool _notMainCharaF = false;
	
	private bool fadeInStartF = false;
	private int movCnt;

	private bool initFlag = false;

	private Image thisImage;
	private Image thisImageShadow;

	private Sprite blankSprite;
	
	private const long shadowMoveCnt = 15;
	private const float fadeInSpeed = 0.15f;


	
	// Use this for initialization
	void Start () {
		//初期位置
		this.transform.localPosition = _thisCharaDefaultLocalPosition;
		
		thisImage = this.GetComponent<Image> ();

		if (_notMainCharaF == true) {
			return;
		}

		this.initThis ();

		//initFlag = true;

		this.startFadeIn ();

		StartCoroutine ( mainLoop() );
	}
	
	public void setFaceDiff(string argsString){
		long tmpL = long.Parse (argsString) -1 ;
		_childDiffImage.sprite = _childDiffSprite[tmpL];
		
	}
	
	public void setFaceBlank(){
		blankSprite = Resources.Load<Sprite>("pictChractorStanding/blinkObj");
		_childDiffImage.sprite = blankSprite;
	}

	/*
	public void setSelfSpot(bool isSpot){

		Color tmpC = new Color(0.5f, 0.5f, 0.5f);
		if (isSpot) {
			tmpC = Color.white;
		}

		thisImage.color = tmpC;
		
		if (_notMainCharaF == true) {return;}
		thisImageShadow.color = tmpC;
		_childDiffImage.color = tmpC;

	}
*/

	private void initThis(){
		if (initFlag == true) {
				return;
				}

		//新規作成
		//動的に作成
		blankSprite = Resources.Load<Sprite>("pictChractorStanding/blinkObj");
		GameObject tmpGO = new GameObject ("charaShadow");
		Image tmpI = tmpGO.AddComponent<Image> ();
		thisImageShadow = tmpI;
		thisImageShadow.sprite = blankSprite;
		
		//親を共通に
		thisImageShadow.transform.parent = thisImage.transform.parent;
		thisImageShadow.transform.position = thisImage.transform.position;

	}


	IEnumerator mainLoop(){

		while (true) {
			yield return null;

			if (fadeInStartF == true){

				float tmpA = thisImage.color.a;
				if (tmpA <= 1f){

					tmpA += fadeInSpeed;
					Color tmpC = new Color (1f, 1f, 1f, tmpA);
					
					thisImage.color = tmpC;
					thisImageShadow.color = tmpC;

					//trueの場合はαチャンネルを仕様
					if (_fadeInAlfaChannelUseF) {
						_childDiffImage.color = tmpC;
					}

				}else{

					if ( ! _fadeInAlfaChannelUseF) {
						//αチャンネル使用していなくても、ここで表示
						_childDiffImage.color = Color.white;
					}

					if (movCnt < shadowMoveCnt){
						//影の移動
						Vector3 tmpV3 = new Vector3(0.5f, 0.1f,0f);

						if (this.transform.position.x < 0) {
							tmpV3 = new Vector3(-0.5f, 0.1f,0f);
						}

						thisImageShadow.transform.localPosition += tmpV3;
						movCnt += 1;
					}else{
						fadeInStartF = false;
					}
				}
			}
		}
	}

	public void startFadeIn(){
		fadeInStartF = true;
		movCnt = 0;
		string fPath = "";

		//prefab
		thisImageShadow.sprite = _shadowSprite;


		thisImage.SetNativeSize ();
		thisImage.transform.localScale = Vector3.one;

		thisImageShadow.SetNativeSize ();
		thisImageShadow.transform.localScale = Vector3.one;
		
		thisImageShadow.transform.localPosition = thisImage.transform.localPosition;

		Color tmpC = new Color (1f, 1f, 1f, 0f);
		thisImage.color = tmpC;
		thisImageShadow.color = tmpC;
		_childDiffImage.color = tmpC;


		//一番上に
		thisImageShadow.transform.SetAsFirstSibling();
	}
	
}
