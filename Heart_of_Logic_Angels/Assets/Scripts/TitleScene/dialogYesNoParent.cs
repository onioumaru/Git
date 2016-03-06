using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class dialogYesNoParent : MonoBehaviour {
	public Text thisCaption;

	private int selectedSaveIndex;
	private tapedObjectMotion tapS;

	private bool boubleTapFlag = false;

	// Use this for initialization
	void Start () {

	}

	public void setValue(int argsIndex){

		string baseStr = "\nで始めてよろしいですか？";

		switch (argsIndex) {
		case -1:
			baseStr = "新しいゲーム" + baseStr;
			break;
		case 0:
			baseStr = "SaveData 1" + baseStr;
			break;
		case 1:
			baseStr = "SaveData 2" + baseStr;
			break;
		default:
			baseStr = "SaveData 3" + baseStr;
			break;
				}

		thisCaption.text = baseStr;

		//貰った値を保持
		selectedSaveIndex = argsIndex;
	}
	
	public void clickOK(){
		if (boubleTapFlag == true) {return;}

		StartCoroutine (selectedSaveData ());
	}

	public void clickCancel(){
		if (boubleTapFlag == true) {return;}

		StartCoroutine (hiddenThisTimeWait ());
	}
	
	IEnumerator hiddenThisTimeWait(){
		boubleTapFlag = true;
		yield return new WaitForSeconds (0.1f);
		this.gameObject.SetActive (false);
		boubleTapFlag = false;
	}

	IEnumerator selectedSaveData(){
		boubleTapFlag = true;
		yield return new WaitForSeconds (0.1f);

		staticValueManagerS sSMS = staticValueManagerGetter.getManager ();

		//セーブデータの選択
		if (selectedSaveIndex == -1) {
			//new Game
			sSMS.createNewGameData();
		} else {
			sSMS.setSelectedSaveDat(selectedSaveIndex);
		}

		sSMS.changeScene(sceneChangeStatusEnum.dataLoading);
		boubleTapFlag = false;
	}

}
