﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class optionMenuGlobal : MonoBehaviour {
	public Toggle _renderShadowToggle;
	public Toggle _renderStageEffToggle;

	// Use this for initialization
	void Start () {
		Debug.Log ("optionMenuGlobal : Set Time.timeScale = 0");
		Time.timeScale = 0f;

		//全てのコライダーを無効化
		this.diseableAllColliders ();

		staticValueManagerS sVMS = staticValueManagerGetter.getManager ();

		_renderShadowToggle.isOn = sVMS.getRenderingShadowFlag ();
		Debug.Log (sVMS.getRenderingShadowFlag ());
		_renderStageEffToggle.isOn = sVMS.getRenderingStageEffFlag ();
		Debug.Log ( sVMS.getRenderingStageEffFlag ());
	}

	void diseableAllColliders(){
		//コライダーの親クラスで指定する
		Collider2D[] allCollider = GameObject.FindObjectsOfType<Collider2D> ();

		foreach (Collider2D tmpCr in allCollider) {
			tmpCr.enabled = false;
				}
	}

	public void changeSenceStageSelect(){
		staticValueManagerGetter.getManager ().changeScene (sceneChangeStatusEnum.gotoStageSelect);
	}

	public void changeSenceTitle(){
		staticValueManagerGetter.getManager ().changeScene (sceneChangeStatusEnum.gotoTitle);
	}

	public void setRenderShadow(){
		PlayerPrefs.SetString("Option_RenderShadow", _renderShadowToggle.isOn.ToString());
	}

	public void setRenderStageEff(){
		PlayerPrefs.SetString("Option_RenderStageEff", _renderStageEffToggle.isOn.ToString());
	}
}
