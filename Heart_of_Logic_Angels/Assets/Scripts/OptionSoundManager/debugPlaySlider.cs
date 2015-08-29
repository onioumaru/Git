using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class debugPlaySlider : MonoBehaviour {
	private Slider thisSlider;

	public Text bgmNumText;

	// Use this for initialization
	void Start () {
		thisSlider = this.GetComponent<Slider> ();
	}

	public void setSliderValue(){
		float bgmNum = thisSlider.value;

		bgmNumText.text = bgmNum.ToString ("0");

		}
}
