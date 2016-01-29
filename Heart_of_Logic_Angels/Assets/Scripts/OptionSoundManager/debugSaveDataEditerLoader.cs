using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class debugSaveDataEditerLoader : MonoBehaviour {
	public Text _saveDataNumText;
	public InputField _dataShowText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	//PlayerPrefs key
	private string jsonSaveKey01 = "mainSaveDataJSON01";
	private string jsonSaveKey02 = "mainSaveDataJSON02";
	private string jsonSaveKey03 = "mainSaveDataJSON03";



	public void loadSaveData(){
		staticValueManagerS sVMS = staticValueManagerGetter.getManager ();

		string playerPrefskeyString;

		switch(_saveDataNumText.text){
		case "0":
			playerPrefskeyString = jsonSaveKey01;
			break;
		case "1":
			playerPrefskeyString = jsonSaveKey02;
			break;
		default:
			playerPrefskeyString = jsonSaveKey03;
			break;
		}

		Debug.Log (PlayerPrefs.GetString (playerPrefskeyString));
		_dataShowText.text = PlayerPrefs.GetString (playerPrefskeyString);

		string tmpS = ",";
		_dataShowText.text = _dataShowText.text.Replace(tmpS + "\"" , tmpS + "\"\n");
	}



	public void saveSaveData(){
		if (_dataShowText.text == "") {
			Debug.Log ("saveData is not found");
		}

		string playerPrefskeyString = "";

		switch(_saveDataNumText.text){
		case "0":
			playerPrefskeyString = jsonSaveKey01;
			break;
		case "1":
			playerPrefskeyString = jsonSaveKey02;
			break;
		default:
			playerPrefskeyString = jsonSaveKey03;
			break;
		}

		string tmpS = ",";
		_dataShowText.text = _dataShowText.text.Replace(tmpS + "\"\n", tmpS + "\"" );

		PlayerPrefs.SetString (playerPrefskeyString, _dataShowText.text);

		_dataShowText.text  = "";
	}

	public void deleteSaveData(){
		
	}
}
