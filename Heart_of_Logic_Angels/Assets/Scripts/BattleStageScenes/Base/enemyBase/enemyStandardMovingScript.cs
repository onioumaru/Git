using UnityEngine;
using System.Collections;


public class enemyStandardMovingScript : MonoBehaviour {
	public int thisMoveType = -1;
	public float _defaultMovingSpeed = 0.1f;

	private GameManagerScript gms;
	private Coroutine _thisCoro;

	private Vector3 _movingV3;

	public float _outRangeLength = 0.05f;


	// Use this for initialization
	void Start () {
		gms = GameManagerGetter.getGameManager ();
	}

	/// <summary>
	/// 自動的に自キャラクターを追尾したいときに使用する
	/// argsType : 0
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


	/// <summary>
	/// Sets the destory me.
	/// </summary>
	/// <param name="argsSec">Arguments sec.</param>
	public void setDestoryMe(float argsSec){
		StartCoroutine ( waitDestoryWait(argsSec) );
	}

	IEnumerator waitDestoryWait(float argsSec){
		yield return new WaitForSeconds (argsSec);
		Destroy (this.gameObject);
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

			Vector3 chkRange = tmpNearChara.transform.position - this.transform.position;

			if (chkRange.magnitude < _outRangeLength) {
				//一定以上近い場合、移動停止
				this.setMovingStop();

				yield break;
			}

			targetVctr = tmpNearChara.transform.position;
			//TODO: 停止まで言った場合に、iTweenスクリプトが破棄されてNulpointが出る。
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
		case 1:
			_movingV3 = argsVect;
			StartCoroutine ( startSimpleLinearMotion() );
			break;
		}

	}

	public void setMovingStop(){
		iTween thisiTween = this.gameObject.GetComponent<iTween> ();

		if (thisiTween != null) {
			thisiTween.isRunning = false;
		}
	}

	IEnumerator startSimpleLinearMotion(){
		while (true) {
			this.transform.position += _movingV3.normalized * _defaultMovingSpeed;

			yield return new WaitForSeconds (0.001f);
		}
	}
}
