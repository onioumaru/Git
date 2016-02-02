using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class mainFrameScript : MonoBehaviour {
	public GameObject _Prefab_Node;

	private int thisProgress;
	private List<battleStageSelectVal> nodeItem;
//	private stageSelectManagerScript sSMS;

	public void createThisList(int argsRoute,int argsProgress , int argsStage){
		//ノードの削除
		this.deleteChildNodes ();

		//ノードの作成
		nodeItem = new List<battleStageSelectVal> ();
		thisProgress = argsProgress;

		//マルチステージはここで区切る
		switch(thisProgress){
		//case 4:

		//	break;

		default:
			Debug.Log (thisProgress);
			battleStageSelectVal tmpVal = new battleStageSelectVal(argsRoute ,thisProgress, argsStage);
			nodeItem.Add(tmpVal);

			break;
		}

		for (int intI = 0; intI < nodeItem.Count; intI++) {
			GameObject tmpGO = Instantiate(_Prefab_Node) as GameObject;
			tmpGO.transform.SetParent(this.transform, false);
			
			RectTransform UIRect = tmpGO.GetComponent<RectTransform>();

			Vector2 tmpV2 = new Vector2(120f, -25f + (-50f * intI));
			UIRect.anchoredPosition = tmpV2;

			//情報のセット
			tmpGO.gameObject.GetComponent<stageSelectNode>().setStageValues(nodeItem[intI]);

		}
	}

	private void deleteChildNodes(){
		for (int i=0; i < this.transform.childCount; i++) {
			Destroy(this.transform.GetChild(i).gameObject);
		}
	}
}
