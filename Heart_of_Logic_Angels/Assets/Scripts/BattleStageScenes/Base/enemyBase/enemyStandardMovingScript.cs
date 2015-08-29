using UnityEngine;
using System.Collections;

public class enemyStandardMovingScript : MonoBehaviour {
	public int thisMoveType = -1;
	public float _defaultMovingSpeed = 0.1f;

	private GameManagerScript gms;
	private Coroutine _thisCoro;

	// Use this for initialization
	void Start () {
		gms = GameManagerGetter.getGameManager ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void setMoveType(int argsType){
		
		if (_thisCoro != null) {
			StopCoroutine(_thisCoro);
			_thisCoro = null;
		}
		
		switch(argsType){
		case 0:
			_thisCoro = StartCoroutine( simpleCharacterTrackingMoving() );
			break;
		}
	}


	public void setMoveTypeTargetPosi(int argsType, Vector3 argsVect){
		
		if (_thisCoro != null) {
			StopCoroutine(_thisCoro);
			_thisCoro = null;
		}
		
		switch(argsType){
		case 0:
			iTween.MoveTo(this.gameObject, iTween.Hash("x", argsVect.x, "y", argsVect.y, "speed", _defaultMovingSpeed));
			break;
		}

	}
	

	IEnumerator simpleCharacterTrackingMoving(){
		while (true) {
			yield return new WaitForSeconds(1f);

			GameObject tmpNearChara = gms.getMostNearCharacter(this.transform.position);
			if (tmpNearChara == null){yield break;}
			
			Vector3 targetVctr = tmpNearChara.transform.position;

			//for debug
			//Vector3 targetVctr = new Vector3(100f, 0f, 0f);

			iTween.MoveTo(this.gameObject, iTween.Hash("x", targetVctr.x, "y", targetVctr.y, "speed", _defaultMovingSpeed));
		}
	}


}
