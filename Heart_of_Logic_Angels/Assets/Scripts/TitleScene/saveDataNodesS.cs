using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class saveDataNodesS : MonoBehaviour {
	public GameObject _TapEffectPrefab;
	public int _NodeNo;
	public GameObject _yesNoDialog;

	private Text thisNoteText;
	private tapedObjectMotion tapS;

	private staticValueManagerS sSMS;
	
	public void OnClick_EventTrigger(){
		
		Vector2 mouseDown = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		GameObject tapEffect = Instantiate (_TapEffectPrefab) as GameObject;

		tapEffect.transform.position = mouseDown;
		
		//タップエフェクト
		tapS.actionTapEffect ();

		this.selectedNodeAction ();

	}
	
	void Start(){
		tapS = this.GetComponent<tapedObjectMotion>();
		sSMS = staticValueManagerGetter.getManager ();
	}


	private void selectedNodeAction(){
		_yesNoDialog.SetActive (true);

		dialogYesNoParent tmpScript = _yesNoDialog.GetComponent<dialogYesNoParent>();
		tmpScript.setValue (_NodeNo);
	}

}
