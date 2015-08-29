using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class uiTextBlinking : MonoBehaviour {
	private Text _text;
	public float _blinkingSpeed = 0.05f;
	// Use this for initialization
	void Start () {
		_text = this.GetComponent<Text> ();

		StartCoroutine ( blinkLoop() );
	}
	
	// Update is called once per frame
	void Update () {
		
	}




	IEnumerator blinkLoop(){
		float alfa = 0f;

		while (true) {
			alfa += _blinkingSpeed;

			_text.color = new Color(1f,1f,1f, alfa);

			if (alfa > 1){
				alfa = 0;
			}
			yield return null;
		}
	}

}
