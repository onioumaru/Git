using UnityEngine;
using System.Collections;

public class battleStageScript : MonoBehaviour {
	public BoxCollider2D _dragEreaCollider;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Vector2 getDragErea(){
		return _dragEreaCollider.size;
	}
}
