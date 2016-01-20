using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class characterLevelManager : ScriptableObject {
	private List<float> expList;

	public characterLevelManager(){
		//Object.DontDestroyOnLoad (this);
		this.initExpMaster ();
	}
	
	private void initExpMaster(){
		expList = new List<float>();
		float tmpF = 0f;
		
		for (int i=0; i < 200; i++) {
			tmpF = (i * i * i)+(37 * i * i)-38;
			expList.Add(tmpF);
		}
	}
	
	public expLevelInfo calcLv(float argsExp){
		expLevelInfo retExp = new expLevelInfo ();
		
		for (int i = 0; i < 200; i++) {
			if (expList[i] > argsExp){
				//対象の経験値より小さい場合,このレベル
				
				retExp.Lv = i;
				retExp.nextExp = expList[i] - argsExp;
				
				if (i == 0){
					retExp.beforeExp = 0;
				}else{
					retExp.beforeExp = expList[i-1];
				}
				
				retExp.nextLvExp = expList[i];
				retExp.totalExp = argsExp;
				
				break;
			}
		}
		return retExp;
	}
}

// args Type

public class expLevelInfo{
	public int Lv;
	public float totalExp;
	public float nextExp;
	public float beforeExp;
	public float nextLvExp;
}



/*
	static ManagerGetter
*/
public static class characterLevelManagerGetter{
	private static characterLevelManager thisMNG;
	
	public static characterLevelManager getManager(){
		if (thisMNG == null) {
			Debug.Log ("create characterLevelManager");
			thisMNG = new characterLevelManager();
		}
		
		return thisMNG;
	}
}
