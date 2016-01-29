using UnityEngine;
using System.Collections;

public class standingCharaImageUnderDrawParentS : MonoBehaviour {
	public GameObject _LeftChild;
	public GameObject _CenterChild;
	public GameObject _RightChild;

	// Update is called once per frame
	void Update () {
		this.transform.SetAsFirstSibling ();
	}
}
