using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class charaMenu_parent : MonoBehaviour {
	
	private Camera trackedCamera;
	private float posiZ;
	
	private allCharaBase argsChara;
	private GameObject parentIconSet;
	
	public bool skillTargrtFlag = false;
	public bool buttonActionFlag = false;
	public GameObject _skillTargetBase;

	// Use this for initialization
	void Start () {
		Time.timeScale = 0;
		trackedCamera = Camera.main;
		//allCharaが取れないのでラベルに任せる

		posiZ = this.gameObject.transform.position.z;
		this.transform.position = new Vector3 (trackedCamera.transform.position.x, trackedCamera.transform.position.y, posiZ);
	}

	void OnMouseDown(){
		if (skillTargrtFlag) {
			//skill target Mode

		} else {
			if(buttonActionFlag == false){
				this.closeMe();
			}
		}
	}
	
	public void closeMe(){
		Time.timeScale = 1f;
		Destroy(this.gameObject);
	}
	
	public void switchSkillTargetMode(){
		skillTargrtFlag = true;

		for (int i = 0; i < this.transform.childCount; i++) {
			if (this.transform.GetChild(i).name != "blackBase"){
			Destroy(this.transform.GetChild(i).gameObject);
			}
		}

		GameObject tmpGO = Instantiate (_skillTargetBase) as GameObject;
		Vector3 tmpV_zero = new Vector3 (0, 0, -1);

		tmpGO.transform.parent = this.transform;
		tmpGO.transform.localPosition = tmpV_zero;
	}


	public void setParentChara(GameObject arsGO){
		argsChara = arsGO.GetComponentInChildren<allCharaBase>();
	}

	public void setParentIconSet(GameObject argsGO){
		parentIconSet = argsGO;
	}

	public allCharaBase getAllCharaScript(){
		return argsChara;
	}

	public void setCharaModeIcon(characterMode argsMode){
		charaIconset_modeIcon tmp =	parentIconSet.transform.Find ("4_modeIcon").GetComponentInChildren<charaIconset_modeIcon> ();
		tmp.setModeIcon(argsMode);
	}
}
