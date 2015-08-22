using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;


public class soundManagerSlider : MonoBehaviour {
	public Text uiText;
	public AudioMixer mainMixer;
	public string volumeName;

	private Scrollbar thisScrB;

	void Start(){
		thisScrB = this.GetComponent<Scrollbar> ();

		if (PlayerPrefs.GetFloat ("Option_" + volumeName) == null) {
			thisScrB.value = 1;
		} else {
			thisScrB.value = PlayerPrefs.GetFloat("Option_" + volumeName);
		}
	}

	public void setSliderValues(){

		float parVal = thisScrB.value * 100f;
		uiText.text = parVal.ToString("0");

		float clampVal = Mathf.Clamp(thisScrB.value, 0.0001f, 1.0f);
		float volumeDB = 40f * Mathf.Log10(clampVal);

		mainMixer.SetFloat(volumeName, Mathf.Clamp(volumeDB, -80.0f, 0.0f));

		PlayerPrefs.SetFloat("Option_" + volumeName, thisScrB.value);
	}
}
