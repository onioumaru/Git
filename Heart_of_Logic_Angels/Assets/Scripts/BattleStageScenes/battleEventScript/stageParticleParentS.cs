using UnityEngine;
using System.Collections;

public class stageParticleParentS : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//子オブジェクトの破壊
		foreach ( Transform n in this.transform )
		{
			GameObject.Destroy(n.gameObject);
		}

		staticValueManagerS s = staticValueManagerGetter.getManager ();

		if (s.getRenderingStageEffFlag () == false) {
			return;
		}

		sceneChangeValue scv = s.getNowSceneChangeValue();
		string[] tmpStr = scv.sceneFileName.Split (new string[]{"-"}, System.StringSplitOptions.None );

		this.setParticle (tmpStr[0],tmpStr[1],tmpStr[2]);
	}

	private void setParticle(string argsRoute,string argsProgress,string argsStage){

		string commonPath = "Prefabs/battleFieldParticles/";
		GameObject tmpBase = null;
		GameObject tmpSource = null;

		switch(argsProgress){
		case "0":
		case "1":
		case "2":
		case "3":
			switch (argsStage){
			case "1":
				tmpSource = Resources.Load (commonPath + "Particle_sakura") as GameObject;
				break;
				
			default:
				tmpSource = Resources.Load (commonPath + "Particle_Smog") as GameObject;
				break;
			}
			break;
		case "4":
			tmpSource = Resources.Load (commonPath + "Particle_sakura") as GameObject;
			break;

		case "5":
			tmpSource = Resources.Load (commonPath + "Particle_dropTears") as GameObject;
			break;

		case "6":
			tmpSource = Resources.Load (commonPath + "nature2") as GameObject;
			break;

		case "7":
			tmpSource = Resources.Load (commonPath + "Particle_dropTears") as GameObject;
			break;

		default:
			Debug.Log ("stage Particle Not Set!");
			//tmpSource = Resources.Load (commonPath + "Particle_Smog") as GameObject;
			break;
			
		}

		tmpBase = Instantiate (tmpSource) as GameObject;
		tmpBase.transform.parent = this.transform;

	}

	/*
Particle_dropTears
Particle_sakura
Particle_Smog
Particle_SunSpotLight
	*/

	// Update is called once per frame
	void Update () {
	
	}



}
