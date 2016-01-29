using UnityEngine;
using System.Collections;

public class deadEffectParent_Script : MonoBehaviour {
	public GameObject _childEff;
	[System.NonSerialized]
	public enumCharaNum _defaultC = enumCharaNum.enju_01;

	// Use this for initialization
	void Start () {
		this.childNodeCreate ();
		StartCoroutine(destoryMe() );
	}

	IEnumerator destoryMe(){
		yield return new WaitForSeconds(4.0f);
		Destroy(this.gameObject);
	}

	void childNodeCreate(){
		Vector3 tmpV;

		this.createChild (new Vector3(2f, 0f));
		this.createChild (new Vector3(-2f, 0f));
		this.createChild (new Vector3(0f, 2f));
		this.createChild (new Vector3(0f, -2f));

		this.createChild (new Vector3(4f, 0f));
		this.createChild (new Vector3(-4f, 0f));
		this.createChild (new Vector3(0f, 4f));
		this.createChild (new Vector3(0f, -4f));

		this.createChild (new Vector3(3f, 3f));
		this.createChild (new Vector3(-3f, 3f));
		this.createChild (new Vector3(3f, -3f));
		this.createChild (new Vector3(-3f, -3f));
	}

	private void createChild(Vector3 argsV3){
		GameObject tmpGO;

		tmpGO = Instantiate (_childEff) as GameObject;
		tmpGO.transform.SetParent (this.transform);
		tmpGO.transform.localPosition = Vector3.zero;

		tmpGO.GetComponent<deadEffect_Script> ()._movingMoment = argsV3 * 0.01f;
		tmpGO.GetComponent<SpriteRenderer> ().color = this.getColor ();
		tmpGO.SetActive (true);
	}

	private Color getColor(){
		switch (_defaultC) {
		case enumCharaNum.enju_01:
			return new Color(1f, 0.5f, 0.5f);
			break;
		case enumCharaNum.syusuran_02:
			return new Color(0.5f, 0.5f, 1f);
			break;
		case enumCharaNum.suzusiro_03:
			return new Color(0f, 0f, 1f);
			break;
		case enumCharaNum.akane_04:
			return new Color(1f, 1f, 0.5f);
			break;
		case enumCharaNum.houzuki_05:
			return new Color(0f, 0f, 1f);
			break;
		case enumCharaNum.mokuren_06:
			return new Color(1f, 1f, 0.5f);
			break;
		case enumCharaNum.sakura_07:
			return new Color(1f, 0.8f, 0.8f);
			break;
		case enumCharaNum.sion_08:
			return new Color(1f, 1f, 1f);
			break;
		case enumCharaNum.hiragi_09:
		default:
			return new Color(0.5f, 0.5f, 0.5f);
			break;
		}
	}
}
