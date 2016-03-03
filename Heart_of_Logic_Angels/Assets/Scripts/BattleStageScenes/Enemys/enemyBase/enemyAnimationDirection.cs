using UnityEngine;
using System.Collections;

/// <summary>
/// Enemy animation direction.
/// このコンポーネントは、オブジェクトの前回位置を取得して、
/// 移動した方向に合わせて、LocalScaleを反転させて、
/// 異動に合わせて自動的に向きを合わせるようにするもの
/// 
/// 付けるだけで良い
/// 
/// 左向き画像限定
/// </summary>
public class enemyAnimationDirection : MonoBehaviour {
	private float beforeDirectionX;

	// Use this for initialization
	void Start () {
		beforeDirectionX = this.transform.parent.position.x;

		StartCoroutine ( mainLoop() );
	}

	IEnumerator mainLoop(){
		while (true) {
			yield return new WaitForSeconds(0.4f);

			//左右の振り向き検知
			//同じ値の場合は何もしない
			if (beforeDirectionX > this.transform.parent.position.x){
				// 
				float tmpC = this.transform.localScale.x;
				if (tmpC < 0f ){
					// スケールがプラスの場合、-1を掛けてマイナスに
					this.transform.localScale = new Vector3(this.transform.localScale.x * -1f, this.transform.localScale.y, this.transform.localScale.z);
				}
			} else if (beforeDirectionX < this.transform.parent.position.x){
				float tmpC = this.transform.localScale.x;

				if (tmpC > 0f ){
					this.transform.localScale = new Vector3(this.transform.localScale.x * -1f, this.transform.localScale.y, this.transform.localScale.z);
				}
			}

			beforeDirectionX = this.transform.parent.position.x;
		}
	}
}
