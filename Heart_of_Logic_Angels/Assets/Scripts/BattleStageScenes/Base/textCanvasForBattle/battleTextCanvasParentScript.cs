using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class battleTextCanvasParentScript : MonoBehaviour {
	public Text _txtHold;
	public Text _txtFlag;
	
	private const float CanvasScaleX = 854f;
	private const float CanvasScaleY = 480F;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void showFlagText(){
		_txtFlag.color = Color.white;
		
		Vector2 nowMouseDown =Input.mousePosition;
		
		float tmpX = nowMouseDown.x - (CanvasScaleX / 2f);
		float tmpY = nowMouseDown.y - (CanvasScaleY / 2f);
		
		Vector3 tgtPosision = new Vector3 (tmpX, tmpY, 0f);
		
		_txtFlag.gameObject.transform.localPosition = tgtPosision;
		
		StartCoroutine ( startHide(_txtFlag) );
	}

	public void showHoldText(){
		_txtHold.color = Color.white;
		
		Vector2 nowMouseDown =Input.mousePosition;
		
		float tmpX = nowMouseDown.x - (CanvasScaleX / 2f);
		float tmpY = nowMouseDown.y - (CanvasScaleY / 2f);
		
		Vector3 tgtPosision = new Vector3 (tmpX, tmpY, 0f);
		
		_txtHold.gameObject.transform.localPosition = tgtPosision;
		
		StartCoroutine ( startHide(_txtHold) );
	}

	IEnumerator startHide(Text argsText){
		float LOOPMAX = 30f;

		Color tmpC;

		for (float loopI = 0f; loopI <= LOOPMAX; loopI++) {
			yield return null;

			//Debug.Lof

			float tmp = 1f - (loopI / LOOPMAX);
			//Debug.Log (tmp);

			tmpC = new Color(1f,1f, 1f, tmp);

			argsText.color = tmpC;
		}
	}
}
