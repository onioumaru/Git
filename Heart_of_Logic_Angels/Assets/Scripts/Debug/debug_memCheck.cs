using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class debug_memCheck : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void Update() {
		uint monoUsed = Profiler.GetMonoUsedSize ();
		uint monoSize = Profiler.GetMonoHeapSize ();
		uint totalUsed = Profiler.GetTotalAllocatedMemory (); // == Profiler.usedHeapSize
		uint totalSize = Profiler.GetTotalReservedMemory ();

		string tmpText = "mono : " + monoUsed/1024 + "/" + monoSize/1024 + "kb\ntotal : " +  totalUsed/1024 + "/" + totalSize/1024 + "kb";

		Text tmpUITxt = this.GetComponent<Text> ();
		tmpUITxt.text = tmpText;
	}
}
