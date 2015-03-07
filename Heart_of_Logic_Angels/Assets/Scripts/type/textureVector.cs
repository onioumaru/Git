using UnityEngine;
using System.Collections;

public class textureVector {
	private Sprite tgtTexture;
	private Vector3 thisScale;

	//constracter
	public textureVector(Sprite argsSprite){
		tgtTexture = argsSprite;
	}

	public textureVector(GameObject argsGameobject){
		SpriteRenderer tmpSR = argsGameobject.GetComponentInChildren<SpriteRenderer> ();
		thisScale = tmpSR.transform.localScale;
		tgtTexture = tmpSR.sprite;
	}



	public float getWidth(){
//		Debug.Log (tgtTexture.textureRect);
		return thisScale.x * tgtTexture.textureRect.width / tgtTexture.pixelsPerUnit;
	}

	public float getHeight(){
		return thisScale.y * tgtTexture.textureRect.height / tgtTexture.pixelsPerUnit;
	}

	public Vector3 getBottomOffset_ForCenterPivot(float offsetX, float offsetY){
		float tmpWidth = this.getWidth() / -2f + offsetX; 
		float tmpHeight = this.getHeight() / -2f + offsetY;

		Vector3 tmpV = new Vector3 (tmpWidth, tmpHeight, 0);

		return tmpV;
	}
}
