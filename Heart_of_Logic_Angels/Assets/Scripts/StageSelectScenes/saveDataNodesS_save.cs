using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class saveDataNodesS_save : MonoBehaviour {
	public int _NodeNo;
	public GameObject _yesNoDialog;
	
	private Text thisNoteText;
	
	private staticValueManagerS sSMS;
	
	public void OnClick_EventTrigger(){
		this.selectedNodeAction ();
	}
	
	void Start(){
		sSMS = staticValueManagerGetter.getManager ();
	}

	private void selectedNodeAction(){
		_yesNoDialog.SetActive (true);
		
		dataSaveDialogYesNoParent tmpScript = _yesNoDialog.GetComponent<dataSaveDialogYesNoParent>();
		tmpScript.setValue (_NodeNo);
	}
	
}
