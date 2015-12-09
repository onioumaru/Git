using UnityEngine;
using System.Collections;

public class enemyYellowRingRemoved : MonoBehaviour {

	void OnAnimationFinish (){
		Destroy (this.gameObject);
	}

}
