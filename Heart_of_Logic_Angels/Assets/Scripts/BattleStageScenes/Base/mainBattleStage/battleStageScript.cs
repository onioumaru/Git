using UnityEngine;
using System.Collections;

public class battleStageScript : MonoBehaviour {
	public BoxCollider2D _dragEreaCollider;

	public Vector2 getDragErea(){
		return _dragEreaCollider.size;
	}
}
