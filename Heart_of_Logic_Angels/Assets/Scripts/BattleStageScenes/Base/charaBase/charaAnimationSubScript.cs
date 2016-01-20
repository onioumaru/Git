using UnityEngine;
using System.Collections;

public class charaAnimationSubScript : MonoBehaviour {

	public GameObject _mokurenNataSprite;
	public GameObject _mokurenYumiSprite;
	public GameObject _sionCanvasSprite;

	private GameObject animationObj;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void autoCreateSionCanvas(){
		animationObj = Instantiate (_sionCanvasSprite) as GameObject;

		animationObj.transform.parent = this.transform;

		animationObj.transform.localPosition  = new Vector3(0.15f, -0.4f, 0f);
	}

	public void autoGotoSkillDoAnime(){
		this.GetComponent<Animator> ().SetTrigger ("gotoSkillDo");
	}

	public void destorySubAnime(){
		if (animationObj != null) {
			Destroy (animationObj.gameObject);
			animationObj = null;
		}
	}
}
