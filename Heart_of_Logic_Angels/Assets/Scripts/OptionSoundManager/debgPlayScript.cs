using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class debgPlayScript : MonoBehaviour {
	private soundManager_Base sndM;
	public Text bgmNumText;

	public void testPlay(){
		sndM = soundManagerGetter.getManager ();

		int tmpBGMNo = int.Parse (bgmNumText.text);
		sndM.playBGM (tmpBGMNo);
	}

	public void testStop(){
		sndM = soundManagerGetter.getManager ();

		sndM.allStopBGM ();

	}

	public void testSEplay(){
		sndM = soundManagerGetter.getManager ();
		
		sndM.playOneShotSound(enm_oneShotSound.nomalButton);
		sndM.playOneShotSound(enm_oneShotSound.enemyHit);
	}

}
