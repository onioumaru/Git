﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class talkingCanvasS : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Canvas tmpC = this.gameObject.GetComponent<Canvas> ();
		tmpC.worldCamera = Camera.main;
	
	}
}
