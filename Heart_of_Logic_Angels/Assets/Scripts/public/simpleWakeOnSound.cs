using UnityEngine;
using System.Collections;

public class simpleWakeOnSound : MonoBehaviour {
	public enm_oneShotSound _wakeSound = enm_oneShotSound.findit;

	// Use this for initialization
	void Start () {
		soundManagerGetter.getManager ().playOneShotSound (_wakeSound);
	}
}
