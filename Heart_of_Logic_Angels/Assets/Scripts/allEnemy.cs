using UnityEngine;
using System.Collections;

// Rigidbody2Dコンポーネントを必須にする
[RequireComponent(typeof(Rigidbody2D))]
public class allEnemy : MonoBehaviour {

	// 死亡時エフェクトのPrefab
	public GameObject removeEnemyPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void removedMe(Transform origin){
		Instantiate (removeEnemyPrefab, origin.position, origin.rotation);
		Destroy (this.gameObject);
	}

	public void setMoving(int argsType, float argsMovingSpeed){
		switch (argsType) {
		case 1:
			// simlpe reft Moving
			Vector3 tmpV = new Vector3(-1,0);

			this.rigidbody2D.velocity = tmpV.normalized * argsMovingSpeed;

			break;
		case 2:
			break;
			
		case 3:
			break;

		default:
			break;
		}
	}




}
