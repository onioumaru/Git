using UnityEngine;
using System.Collections;

// Rigidbody2Dコンポーネントを必須にする
[RequireComponent(typeof(Rigidbody2D))]
public class allChara : MonoBehaviour {
	
	public int nowHP;
	public int maxHP;

	//この値が0になるまで、硬直時間とする
	public int movingFreezeCnt = 0;
	
	public GameObject flag_chara11;
	
//	public UnityEngine.UI.Text debgTxt;
	
	public bool stopFlag;
	
	float offsetX = -0.28f;
	float offsetY = 0f;

	
	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		if (stopFlag == true) {
			Vector2 tmpVct = new Vector2(0,0);
			rigidbody2D.velocity = tmpVct;
			
		} else {
			Vector3 offsetP = new Vector3(flag_chara11.transform.position.x + offsetX, flag_chara11.transform.position.y + offsetY);
			
			Vector3 tmpV = (offsetP - transform.position);
			
			if (tmpV.magnitude < 0.02f){
				stopFlag = true;
			}else{
				rigidbody2D.velocity = tmpV.normalized;
			}
		} 
	}
	
	public void Damege(int argsVal){
		//this.gameObject.GetComponent
	}

}
