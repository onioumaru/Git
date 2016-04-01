using UnityEngine;
using System.Collections;

public class bulletBase_CanDestory : MonoBehaviour {
	public bool destoryF = false;
	public GameObject _destroyEff;

	//攻撃によって破壊される場合の処理
	//くらい判定エリアを持っているオブジェクトにアタッチする事
	public void interceptedThisBullet(){
		Destroy (this.gameObject);

		soundManagerGetter.getManager().playOneShotSound(enm_oneShotSound.flipSword);

		if (_destroyEff != null) {
			GameObject tmpGO = Instantiate (_destroyEff) as GameObject;
			tmpGO.transform.position = this.transform.position;
		}
	}
}
