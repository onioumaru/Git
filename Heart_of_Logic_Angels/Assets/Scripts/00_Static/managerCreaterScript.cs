using UnityEngine;
using System.Collections;

public class managerCreaterScript : MonoBehaviour {
	public GameObject _soundManagerPrefabs;
	public GameObject _staticValueManagerPrefabs;



	// Use this for initialization
	void Awake () {
		Application.targetFrameRate = 30;

		GameObject retSM = GameObject.FindWithTag("SoundManager");
		if (retSM == null) {
			GameObject tmpGO = Instantiate(_soundManagerPrefabs);
			//tmpGO.name = "SoundManager";
				}

		GameObject retSSM = GameObject.FindWithTag("StaticSceneManager");
		if (retSSM == null) {
			GameObject tmpGO = Instantiate(_staticValueManagerPrefabs);
			//tmpGO.name = "StaticSceneManager";
		}

		Destroy (this.gameObject);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
