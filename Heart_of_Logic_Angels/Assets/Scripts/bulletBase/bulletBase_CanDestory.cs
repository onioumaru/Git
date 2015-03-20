using UnityEngine;
using System.Collections;

public class bulletBase_CanDestory : MonoBehaviour {
	public bool destoryF = false;

	//攻撃によって破壊される場合の処理
	//くらい判定エリアを持っているオブジェクトにアタッチする事
	public void interceptedThisBullet(){
		Destroy (this.gameObject);
	}
}
