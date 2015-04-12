using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class mainFrameScript : MonoBehaviour {
	public GameObject _Prefab_Node;

	private storyProgress thisProgress;
	private List<battleStageSelectVal> nodeItem;
//	private stageSelectManagerScript sSMS;

	public void createThisList(storyProgress argsProgress){
		//ノードの削除
		this.deleteChildNodes ();

		//ノードの作成
		nodeItem = new List<battleStageSelectVal> ();
		this.thisProgress = argsProgress;

		switch(this.thisProgress.progress){
		default:
			battleStageSelectVal tmpVal = new battleStageSelectVal(this.thisProgress.progress,0,0);
			nodeItem.Add(tmpVal);

			battleStageSelectVal tmpVal2 = new battleStageSelectVal(this.thisProgress.progress,1,0);
			nodeItem.Add(tmpVal2);

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
