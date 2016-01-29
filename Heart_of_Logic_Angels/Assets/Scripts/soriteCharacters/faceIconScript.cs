using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class faceIconScript : MonoBehaviour {
	public Sprite _iconChecked;
	public Sprite _iconNoChecked;
	public int _thisIndex;
	
	private bool thisSorityFlag = true;

	private Image checkIcon;
	private tapedObjectMotion tapSc;
	private faceIconController fIC;
	private soriteCharaDotChanger charaDotScript;

	// Use this for initialization
	void Start () {
		checkIcon = this.transform.Find ("chkImage").GetComponent<Image> ();
		tapSc = this.GetComponent<tapedObjectMotion> ();
		fIC = this.transform.parent.GetComponent<faceIconController>();
		charaDotScript = GameObject.Find ("soriteCharaDot").GetComponent<soriteCharaDotChanger>();
	}

	public void clickIcon(){
		tapSc.actionTapEffect ();
		
		if(_thisIndex != fIC.selectedIconIndex){
			//未選択から選択へ

			fIC.showSelectedCharaInfo(_thisIndex);
			charaDotScript.setAnimation(_thisIndex);

			return;
		}



		if (thisSorityFlag) {
			//既に選択状態である
			thisSorityFlag = false;
			
			Color tmpC = new Color(0.3f,0.3f,0.3f);
			this.GetComponent<Image>().color = tmpC;
			checkIcon.sprite = _iconNoChecked;

		} else {
			// check off
			thisSorityFlag = true;
			
			Color tmpC = new Color(1f,1f,1f);
			this.GetComponent<Image>().color = tmpC;
			checkIcon.sprite = _iconChecked;
		}
	}

	public bool getThisSoriteFlag(){
		return thisSorityFlag;
	}
	
}
