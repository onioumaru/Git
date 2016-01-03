using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class charaStatusIconCtrl : MonoBehaviour {
	public Sprite _unbreakableIcon;
	public Sprite _regenarateIcon;

	private Vector3 defaultOffset;
	private HashSet<charaStatusIconAdding> thisStatusSet;
	private SpriteRenderer thisSR;


	// Use this for initialization
	void Start () {
		thisSR = this.GetComponent<SpriteRenderer> ();
		defaultOffset = new Vector3(0f, 0.25f, 0f);
		thisStatusSet = new HashSet<charaStatusIconAdding>();

		thisSR.color = Color.white;
		thisSR.sprite = null;

		this.transform.localPosition = defaultOffset;
		StartCoroutine( mailLoop() );
	}


	IEnumerator mailLoop(){
		int nowIndex = 0;

		while (true) {
			if (thisStatusSet.Count > 0) {
				//リストにない場合、処理する必要なし

				do {
					nowIndex++;

					if (nowIndex == (int)charaStatusIconAdding.dummyMaxVal){
						break;
					} else if (nowIndex > (int)charaStatusIconAdding.dummyMaxVal) {
						//ループリセット
						nowIndex = 0;
					}

					//見つけるまでLoop
				} while (thisStatusSet.Contains ((charaStatusIconAdding)nowIndex) != true);

				//Debug.Log ("nowindex : " + (charaStatusIconAdding)nowIndex);

				//表示しているスプライトを変更する
				//以下は随時追加

				switch ((charaStatusIconAdding)nowIndex) {
				case charaStatusIconAdding.regenarate01:
					thisSR.sprite = _regenarateIcon;
					break;

				case charaStatusIconAdding.unbreakable00:
					thisSR.sprite = _unbreakableIcon;
					break;

				case charaStatusIconAdding.dummyMaxVal:
					thisSR.sprite = null;
					break;
				}

				//thisSR.color = Color.white;
			}

			yield return new WaitForSeconds (0.3f);
		}
	}

	public void setIcon(charaStatusIconAdding argsStat){
		thisStatusSet.Add (argsStat);
	}

	public void removeIcon(charaStatusIconAdding argsStat){
		if (thisStatusSet.Contains (argsStat) == true) {
			//存在する場合は削除
			thisStatusSet.Remove (argsStat);
			thisSR.sprite = null;
			//thisSR.color = new Color(1f,1f,1f,0f);
		}
	}

}

public enum charaStatusIconAdding{
	unbreakable00 = 0, regenarate01 = 1, dummyMaxVal = 2
}
