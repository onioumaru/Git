using UnityEngine;
using System.Collections;

public class charaSkill_SakuraClystal : MonoBehaviour {
	public GameObject _prefabBullet;
	public GameObject _prefabParticle;

	private bool attackMotionFlag;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (attackMotionFlag == false){
			//ランダム移動させる

			//保留中
		}

	}

	public void setAttack(Vector3 argsTargetPosi, commonCharaAttack argsDm){
		attackMotionFlag = true;

		float MaxRandomWarpRange = 2f;

		Vector3 tmpV3 = Random.insideUnitCircle * MaxRandomWarpRange;
		//クリスタル自体のランダムワープ
		this.transform.localPosition = tmpV3;

		GameObject retGO = Instantiate (_prefabBullet) as GameObject;
		retGO.transform.position = this.transform.position;
		retGO.GetComponent<charaSkill_SakuraClystalBullet> ().setEndPosision (argsTargetPosi);

		GameObject retGO2 = Instantiate(_prefabParticle) as GameObject;
		retGO2.transform.position = this.transform.position;

		StartCoroutine ( startDamageCount(argsDm) );
	}

	IEnumerator startDamageCount(commonCharaAttack argsDm){

		yield return new WaitForSeconds (0.06f);

		argsDm.exec ();

		attackMotionFlag = false;
	} 
}
