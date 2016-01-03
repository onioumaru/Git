using UnityEngine;
using System.Collections;

public class charaSkill_SakuraClystalBullet : MonoBehaviour {
	private int moveCountFrame = 3;
	private int nowCnt = 0;

	private Vector3 startPosi;
	private Vector3 endPosi;
	private Vector3 oneFramePosi; 

	public GameObject _prefabsExpord; 

	private LineRenderer thisLR;

	// Use this for initialization
	void Start () {
		// for debug
		//endPosi = Vector3.right * 2;

		startPosi = this.transform.position;
		oneFramePosi = (startPosi - endPosi) / moveCountFrame;

		thisLR = this.GetComponent<LineRenderer> ();

		thisLR.SetPosition (0, startPosi);
		thisLR.SetPosition (1, endPosi);

	}

	public void setEndPosision(Vector3 argsV){
		endPosi = argsV;
	}
	
	// Update is called once per frame
	void Update () {
		if (nowCnt >= moveCountFrame) {
			/*
			GameObject tmpGO = Instantiate (_prefabsExpord);
			tmpGO.transform.position = endPosi;
*/
			Destroy (this.gameObject);

		} else {
			thisLR.SetPosition (0, (startPosi - (oneFramePosi * nowCnt)));

			nowCnt += 1;
		}
	}
}
