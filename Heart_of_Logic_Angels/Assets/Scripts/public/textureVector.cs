using UnityEngine;
using System.Collections;

/*
 * 汎用
 * このクラスは Sprite のゲーム中でのサイズを取得する
 * 戦闘画面のHPバーの表示に使っている
 */

public class textureVector {
	private Sprite tgtTexture;
	private Vector3 thisScale;

	//constracter
	public textureVector(Sprite argsSprite){
		tgtTexture = argsSprite;
	}

	public textureVector(GameObject argsGameobject){
		SpriteRenderer tmpSR = argsGameobject.transform.GetComponentInChildren<SpriteRenderer> ();
		thisScale = tmpSR.transform.localScale;
		tgtTexture = tmpSR.sprite;
	}

	public float getWidth(bool neglectScale){
		if (neglectScale) {
			return thisScale.x * tgtTexture.textureRect.width / tgtTexture.pixelsPerUnit;
		}

		//横幅の計算の場合は、スケール考慮不要
		return tgtTexture.textureRect.width / tgtTexture.pixelsPerUnit;
	}

	public float getHeight(bool neglectScale){
		
		if (neglectScale) {
			return thisScale.y * tgtTexture.textureRect.height / tgtTexture.pixelsPerUnit;
		}

		return tgtTexture.textureRect.height / tgtTexture.pixelsPerUnit;
	}


	public Vector3 getBottomOffset_ForCenterPivot(float offsetX, float offsetY, bool scaleFlag){
		float tmpWidth = this.getWidth(scaleFlag) / -2f + offsetX; 
		float tmpHeight = this.getHeight(scaleFlag) / -2f + offsetY;

		Vector3 tmpV = new Vector3 (tmpWidth, tmpHeight, 0);

		return tmpV;
	}
}
