using UnityEngine;
using System.Collections;

static public class LookAt2D {
	static public Quaternion lookAt(Vector3 target, Vector3 basePosi){
		//Debug.Log (target);
		//Debug.Log (basePosi);
		float tmpFl = Vector3.Angle(Vector3.up , (target - basePosi));
		//Debug.Log (tmpFl);

		if ((target-basePosi).x > 0){
			tmpFl = tmpFl * -1f;
		}

		return Quaternion.AngleAxis(tmpFl, Vector3.forward);
	}
}
