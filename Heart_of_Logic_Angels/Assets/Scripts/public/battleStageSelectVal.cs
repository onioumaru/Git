using UnityEngine;
using System.Collections;

/*
 * ステージ選択画面で使用
 * リストのノードごとに、そのノードがどのステージを表すか、データを保持する
 */

public class battleStageSelectVal{
	//this paramater is 10000-
	public storyProgressEnum storyChapter;
	public int stageNo;	//0スタート
	public int clearCount;	//0スタート
	public string stageTitle;
	public string stageComment;
	public Sprite bgImage;

	public battleStageSelectVal(storyProgressEnum argsStoryChapter,int argsStageNo, int argsClearCnt){
		this.storyChapter = argsStoryChapter;
		this.stageNo = argsStageNo;
		this.clearCount = argsClearCnt;

		this.setStageStrings();

		//背景は固定なので、取っておく
		stageSelectDataStockerScript tmpDatStock = stageSelectManagerGetter.getsceneSelectManager().getDataStocker();
		bgImage = tmpDatStock.getBackGroundImage (argsStoryChapter, argsStageNo);

		}


	private void setStageStrings(){

		switch (storyChapter) {
		case  storyProgressEnum.newGame:
			stageTitle = "1-1 始まり";
			stageComment = "";
			break;
		case  storyProgressEnum.endTutorial_nextSyusuranBattle:
			stageTitle = "2-1 シュスランを助けて！";
			stageComment = "";
			break;
		case  storyProgressEnum.addSyusuran_nextHouzuki:
			stageTitle = "3-1 1F：体育館";
			stageComment = "";
			break;
		case  storyProgressEnum.addHouzuki_nextStageSelect01:
			switch(stageNo){
			case 0:
				stageTitle = "4-1 1F：図書館";
				stageComment = "";
				break;
			case 1:
				stageTitle = "4-2 野外：運動場";
				stageComment = "123456\n123456";
				break;
			}
			break;
		default:
			break;
		}
	}
}
