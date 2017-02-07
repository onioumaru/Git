using UnityEngine;
using System.Collections;

public class charaAnimationSubScript : MonoBehaviour {

	public GameObject _mokurenNataSprite;
	public GameObject _mokurenYumiSprite;
	public GameObject _sionCanvasSprite;

	private GameObject animationObj;

	public float pingPongSpeed = 20f;
	public float pingPongLength = 0.03f;
	public float pingPongDiffY = 0.03f;
	private Coroutine flyingCoro;

	// Use this for initialization
	void Start () {
		//for debug
		//StartCoroutine(this.flyingHuwahuwaLoop ());
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

	public void flyingHuwahuwaSet(bool argBool){
		if (argBool) {
			flyingCoro = StartCoroutine(flyingHuwahuwaLoop() );
			Debug.Log ("call true");

		} else {
			if (flyingCoro != null) {
				Debug.Log ("stop huwahuwa");
				StopCoroutine (flyingCoro);
				flyingCoro = null;
				this.transform.localPosition = Vector3.zero;
			} 
		}
	}

	IEnumerator flyingHuwahuwaLoop(){
		while (true) {
			this.transform.localPosition = new Vector3 (0f, Mathf.PingPong(Time.time / pingPongSpeed, pingPongLength) + pingPongDiffY, 0f);

			yield return new WaitForSeconds (0.001f);
		}
	}
}
