using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;

public class soundManager_Base : MonoBehaviour {
	public GameObject _SE_base;
	public AudioMixer _audioMixer;

	private AudioSource BGMheader;
	private AudioSource BGMloop;

	private float delayedTime;

	private Hashtable oneShotSound;


	// Use this for initialization
	void Start () {
		//Object.DontDestroyOnLoad (this);

		BGMheader = this.transform.Find ("BGM_Loop").GetComponent<AudioSource>();
		BGMloop = this.transform.Find("BGM_FirstLoop").GetComponent<AudioSource>();
		
		if (oneShotSound == null) {
			oneShotSound = new Hashtable();
		}

		this.setDefaultVolume("BGMVol");
		this.setDefaultVolume("MasterVol");
		this.setDefaultVolume("SEVol");
	}

	private void setDefaultVolume(string argsKey){
		float tmpVal;
		string volumeName = "Option_" + argsKey;

		if (PlayerPrefs.HasKey(volumeName) == false) {
			PlayerPrefs.SetFloat (volumeName, 0.8f);
			tmpVal = 0.8f;
		} else {
			tmpVal = PlayerPrefs.GetFloat(volumeName);
		}

		//Debug.Log (tmpVal);

		float clampVal = Mathf.Clamp(tmpVal, 0.0001f, 1.0f);
		float volumeDB = 40f * Mathf.Log10(clampVal);

		_audioMixer.SetFloat(argsKey, Mathf.Clamp(volumeDB, -80.0f, 0.0f));

	}



	public void playBGM(int bgmNo){
		this.allStopBGM ();
		this.setLoadingBGMList (bgmNo);

		//StartCoroutine( soundPlayWaiter() );
		
		BGMheader.Play ();
		BGMloop.PlayDelayed(delayedTime);
	}
	/**
	 * 
	IEnumerator soundPlayWaiter(){
		//2\10フレームくらい待つ
		for (int cntI = 0; cntI < 10; cntI++) {
			yield return null;
				} 

	}
*/

	public void allStopBGM(){
		BGMheader.Stop ();
		BGMloop.Stop ();
	}

	private void setLoadingBGMList(int bgmNo){
		string path = "Sounds/BGM/";
		//Resources\Sounds\BGM\01_2_go_on

		//Debug.Log ("setLoadingBGMList : " + bgmNo);

		//曲ごとにリソースの場所とループタイミングを設定
		//面倒なので、ソースに直描き
		switch(bgmNo){
		case 0:
			BGMheader.clip = Resources.Load(path + "00_1_enjyu") as AudioClip;
			BGMloop.clip = Resources.Load(path + "00_2_enjyu") as AudioClip;
			//delayedTime = 67.5f;

			break;
			
		case 1:
			BGMheader.clip = Resources.Load(path + "01_1_syusuran") as AudioClip;
			BGMloop.clip = Resources.Load(path + "01_2_syusuran") as AudioClip;
			//delayedTime = 15.755f;
			
			break;
		case 2:
			BGMheader.clip = Resources.Load(path + "00_dummyBlank") as AudioClip;
			BGMloop.clip = Resources.Load(path + "02_2_suzu") as AudioClip;
			//delayedTime = 0f;
			
			break;
		case 3:
			BGMheader.clip = Resources.Load(path + "03_1_akane") as AudioClip;
			BGMloop.clip = Resources.Load(path + "03_2_akane") as AudioClip;
			//delayedTime = 134.1f;
			
			break;
		case 4:
			BGMheader.clip = Resources.Load(path + "00_dummyBlank") as AudioClip;
			BGMloop.clip = Resources.Load(path + "04_2_hozuki") as AudioClip;
			//delayedTime = 0f;
			
			break;
		case 5:
			BGMheader.clip = Resources.Load(path + "00_dummyBlank") as AudioClip;
			BGMloop.clip = Resources.Load(path + "05_2_mokuren") as AudioClip;
			//delayedTime = 0f;
			
			break;
		case 6:
			BGMheader.clip = Resources.Load(path + "00_dummyBlank") as AudioClip;
			BGMloop.clip = Resources.Load(path + "06_2_sakura") as AudioClip;
			//delayedTime = 0f;
			
			break;
		case 7:
			BGMheader.clip = Resources.Load(path + "00_dummyBlank") as AudioClip;
			BGMloop.clip = Resources.Load(path + "07_2_sion") as AudioClip;
			//delayedTime = 0f;
			
			break;
		case 8:
			BGMheader.clip = Resources.Load(path + "00_dummyBlank") as AudioClip;
			BGMloop.clip = Resources.Load(path + "08_2_hiragi") as AudioClip;
			//delayedTime = 0f;
			
			break;
		case 9:
			BGMheader.clip = Resources.Load(path + "09_1_gameover") as AudioClip;
			BGMloop.clip = Resources.Load(path + "00_dummyBlank") as AudioClip;
			//delayedTime = 0f;
			
			break;
		case 10:
			BGMheader.clip = Resources.Load(path + "00_dummyBlank") as AudioClip;
			BGMloop.clip = Resources.Load(path + "10_2_ninja") as AudioClip;
			//delayedTime = 0f;
			
			break;
		case 11:
			BGMheader.clip = Resources.Load(path + "00_dummyBlank") as AudioClip;
			BGMloop.clip = Resources.Load(path + "11_2_bittersweet") as AudioClip;
			//delayedTime = 0f;
			
			break;
		case 12:
			BGMheader.clip = Resources.Load(path + "12_stageClear") as AudioClip;
			BGMloop.clip = Resources.Load(path + "00_dummyBlank") as AudioClip;
			//delayedTime = 0f;

			break;

		case 13:
			BGMheader.clip = Resources.Load(path + "00_dummyBlank") as AudioClip;
			BGMloop.clip = Resources.Load(path + "13_02_hosikuzu") as AudioClip;
			//delayedTime = 0f;

			break;

		case 14:
			BGMheader.clip = Resources.Load(path + "14_1_reflectable_battle") as AudioClip;
			BGMloop.clip = Resources.Load(path + "14_2_reflectable_battle") as AudioClip;
			//delayedTime = 0f;

			break;

		case 15:
			BGMheader.clip = Resources.Load(path + "00_dummyBlank") as AudioClip;
			BGMloop.clip = Resources.Load(path + "15_2_nc91697") as AudioClip;
			//delayedTime = 0f;

			break;

		case 16:
			BGMheader.clip = Resources.Load(path + "00_dummyBlank") as AudioClip;
			BGMloop.clip = Resources.Load(path + "16_2_nc101962") as AudioClip;
			//delayedTime = 0f;

			break;

		case 17:
			BGMheader.clip = Resources.Load(path + "00_dummyBlank") as AudioClip;
			BGMloop.clip = Resources.Load(path + "17_2_XAEROTRIC_mono") as AudioClip;
			//delayedTime = 0f;

			break;

		default:
			//実質 delete
			BGMheader.clip = Resources.Load(path + "00_dummyBlank") as AudioClip;
			BGMloop.clip = Resources.Load(path + "00_dummyBlank") as AudioClip;
			//delayedTime = 0f;
			
			break;
		}

		delayedTime = BGMheader.clip.length;
		BGMloop.SetScheduledStartTime(delayedTime);
	}


	public void playOneShotSound(enm_oneShotSound argsS){
		string path = "Sounds/SE/";
		AudioSource tmpAC = null;

		if (oneShotSound.ContainsKey( argsS ) == false) {
			//作って入れる
			GameObject tmpGO = Instantiate(_SE_base) as GameObject;
			tmpAC = tmpGO.GetComponent<AudioSource>();

			oneShotSound.Add(argsS, tmpAC);

			tmpAC.clip = Resources.Load(path + argsS.ToString() ) as AudioClip;

			if (tmpAC.clip == null){
				Debug.Log ("audioClip : " + argsS.ToString() + " is not Found");
			}
		} else {
			tmpAC = oneShotSound[argsS] as AudioSource;
		}

		tmpAC.Play ();
	}

	/// <summary>
	/// 文字列を直で入れる
	/// </summary>
	/// <param name="argsStr">Arguments string.</param>
	public void playOneShotSound(string argsStr){
		string path = "Sounds/SE/";
		AudioSource tmpAC = null;

		if (oneShotSound.ContainsKey( argsStr ) == false) {
			//作って入れる
			GameObject tmpGO = Instantiate(_SE_base) as GameObject;
			tmpAC = tmpGO.GetComponent<AudioSource>();

			oneShotSound.Add(argsStr, tmpAC);

			tmpAC.clip = Resources.Load(path + argsStr ) as AudioClip;

			if (tmpAC.clip == null){
				Debug.Log ("audioClip : " + argsStr + " is not Found");
			}
		} else {
			tmpAC = oneShotSound[argsStr] as AudioSource;
		}

		tmpAC.Play ();
	}
}


/*
 * 
 */
public enum enm_oneShotSound{
	//prefab と同じ名前にする事
	charaMenu
	,massegeNext
	,playerDamage
	,enemyHit
	,nomalButton
	,closeButton
	,effect001
	,attack03
	,scream_of_a_monster_C1
	,knocking_a_wooden_door
	,removeCloud
	,duck
	,skillCancel
	,flipSword
	,tinnitus
	,horror
	,findit
	,buni
	,deadEffectSound
	,metal
	,modeChange
	,Voice_LvUp
}



public static class soundManagerGetter {
	private static soundManager_Base baseScript;

	public static soundManager_Base getManager(){
		if (baseScript == null) {
			baseScript = GameObject.FindWithTag("SoundManager").GetComponent<soundManager_Base>();
				}

		return baseScript;

	}
}