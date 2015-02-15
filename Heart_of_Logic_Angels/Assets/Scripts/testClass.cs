using UnityEngine;
using System.Collections;

public class testClass : MonoBehaviour {
	void Start () {
		myClass tmpC = new myClass (999);
		tmpC.callX ();
	}
}

public class myClass{
	private int x;

	public myClass(int argsX){
		this.x = argsX;
	}

	public void callX(){
		Debug.Log(this.x);
	}
}
