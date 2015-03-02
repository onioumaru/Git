using UnityEngine;
using System.Collections;

public class particleDeleyer : MonoBehaviour {
	private ParticleSystem[] parentParticle;
	
	public float delayTime;
	public float endTime;

	// Use this for initialization
	void Start () {
		parentParticle = this.GetComponentsInChildren<ParticleSystem> ();

		for (int i = 0; i < parentParticle.Length; i++) {
			parentParticle[i].Stop();
		}
		
		StartCoroutine (startDeley ());
		StartCoroutine (startEndDeley ());
		StartCoroutine (startDestoryDeley ());
	}
	
	IEnumerator startDeley(){
		yield return new WaitForSeconds (delayTime);
		
		for (int i = 0; i < parentParticle.Length; i++) {
			parentParticle[i].Play();
		}
	}
	
	IEnumerator startEndDeley(){
		yield return new WaitForSeconds (endTime);
		
		for (int i = 0; i < parentParticle.Length; i++) {
			parentParticle[i].Stop();
		}
	}

	IEnumerator startDestoryDeley(){
		yield return new WaitForSeconds (endTime + 1f);

		Destroy (this.gameObject);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
