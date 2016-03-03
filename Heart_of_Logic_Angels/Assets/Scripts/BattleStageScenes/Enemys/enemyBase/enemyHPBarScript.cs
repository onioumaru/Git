using UnityEngine;
using System.Collections;

public class enemyHPBarScript : MonoBehaviour {
	private float maxLen = 1;
	//HPBar の長さ
	private float thisDefWidth = 0.64f;
	private float maxHP;
	
	public void setMaxBarLength(float argsLen, float argsMaxHP){
		maxLen = argsLen;
		Vector3 tmpV = new Vector3 (maxLen, 1, 1);
		this.transform.localScale = tmpV;
		maxHP = argsMaxHP;
	}

	public void setMaxBarLength_argsWidth(float argsWidth, float argsMaxHP){
		//Width を渡された場合、割合で全長を出す
		float tmpLen = argsWidth / thisDefWidth;

		maxLen = tmpLen;
		Vector3 tmpV = new Vector3 (maxLen, 1, 1);
		this.transform.localScale = tmpV;

		maxHP = argsMaxHP;
	}

	public void setHP(float nowHP){
		float tmpLen = nowHP / maxHP * maxLen;

		Vector3 tmpV = new Vector3 (tmpLen, 1, 1);
		this.transform.localScale = tmpV;
	}
}