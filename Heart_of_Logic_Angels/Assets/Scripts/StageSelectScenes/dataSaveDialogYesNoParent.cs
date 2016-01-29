using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class dataSaveDialogYesNoParent : MonoBehaviour {
	public Text thisCaption;
	public saveDataNodeParent _saveList;
	
	private int selectedSaveIndex;
	private tapedObjectMotion tapS;
	
	private bool boubleTapFlag = false;
	
	// Use this for initialization
	void Start () {
		
	}
	
	public void setValue(int argsIndex){
		
		string baseStr = "\nに記録してよろしいですか？";
		
		switch (argsIndex) {
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
		_saveList.gameObject.SetActive (false);

		boubleTapFlag = false;
	}
	
	IEnumerator selectedSaveData(){
		boubleTapFlag = true;
		yield return new WaitForSeconds (0.1f);

		staticValueManagerS sVMS = staticValueManagerGetter.getManager ();
		//セーブデータの選択
		sVMS.saveJSONData (selectedSaveIndex);
		
		_saveList.gameObject.SetActive (false);
		_saveList.createList ();		//saveした場合、リストの再作成

		this.gameObject.SetActive (false);
		boubleTapFlag = false;
	}
	
}
