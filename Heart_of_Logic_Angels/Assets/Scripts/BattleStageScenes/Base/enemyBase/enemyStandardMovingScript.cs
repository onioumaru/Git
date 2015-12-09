using UnityEngine;
using System.Collections;

[RequireComponent(typeof(enemyAnimationDirection))]
public class enemyStandardMovingScript : MonoBehaviour {
	public int thisMoveType = -1;
	public float _defaultMovingSpeed = 0.1f;

	private GameManagerScript gms;
	private Coroutine _thisCoro;


	// Use this for initialization
	void Start () {
		gms = GameManagerGetter.getGameManager ();
	}

	/// <summary>
	/// 自動的に自キャラクターを追尾したいときに使用する
	/// </summary>
	/// <param name="argsType">Arguments type.</param>
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
	
	IEnumerator simpleCharacterTrackingMoving(){
		//初期座標
		yield return new WaitForSeconds(1f);
		
		GameObject tmpNearChara = gms.getMostNearCharacter(this.transform.position);
		if (tmpNearChara == null){yield break;}
		
		Vector3 targetVctr = tmpNearChara.transform.position;
		iTween.MoveTo(this.gameObject, iTween.Hash("x", targetVctr.x, "y", targetVctr.y, "speed", _defaultMovingSpeed));



		while (true) {
			//更新系
			yield return new WaitForSeconds(10f);
			
			tmpNearChara = gms.getMostNearCharacter(this.transform.position);
			if (tmpNearChara == null){yield break;}

			targetVctr = tmpNearChara.transform.position;

			bool tmpBool = this.gameObject.GetComponent<iTween>().isRunning;

			if (tmpBool){
				//IsRunningがTrue(停止中の場合更新しない)
			iTween.MoveTo(this.gameObject, iTween.Hash("x", targetVctr.x, "y", targetVctr.y, "speed", _defaultMovingSpeed));
			}
		}
	}

	public void setMovingFreeze(){
		iTween.Pause ();
		}


	/// <summary>
	/// 特定座標を指定して移動させたいときに使用する
	/// </summary>
	/// <param name="argsType">Arguments type.</param>
	/// <param name="argsVect">Arguments vect.</param>
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

	public void setMovingStop(){
		iTween thisiTween = this.gameObject.GetComponent<iTween> ();

		if (thisiTween != null) {
			thisiTween.isRunning = false;
		}
	}

}
