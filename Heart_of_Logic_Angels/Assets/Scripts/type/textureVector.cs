using UnityEngine;
using System.Collections;

public class textureVector {
	private Sprite tgtTexture;

	//constracter
	public textureVector(Sprite argsSprite){
		tgtTexture = argsSprite;
	}



	public float getWidth(){
		return tgtTexture.texture.width / tgtTexture.pixelsPerUnit;
	}

	public float getHeight(){
		return tgtTexture.texture.height / tgtTexture.pixelsPerUnit;
	}

	public Vector2 aaa(){
		return tgtTexture.textureRect.center;
		}
/*
	public Vector3 getBottomOffset_ForCenterPivot(){
		Vector3 tmpV = new Vector3 (-1 * this.getWidth / 2f, -1 * this.getHeight / 2f, 0);

		return tmpV;
	}
	*/
}
