using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class senceChangeManagerScript : MonoBehaviour {
	private Image fadeImage;

	void Start(){
		fadeImage = this.transform.Find("Canvas").Find("fadeImage").GetComponent<Image>();
		//fadeImage
	}

	public void startSenceChange(){
		Application.LoadLevel (1);
	}
}
