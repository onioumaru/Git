using UnityEngine;
using System.Collections;

/*
 * ステージ選択画面で使用
 * リストのノードごとに、そのノードがどのステージを表すか、データを保持する
 */

public class battleStageSelectVal{
	//this paramater is 10000-
	public int storyChapter;
	public int stageNo;	//0スタート
	public int stageStep;	//0スタート
	public string stageTitle;
	public string stageComment;
	public Sprite bgImage;

	public battleStageSelectVal(int argsStoryChapter,int argsStageNo, int argsStageStep){
		this.storyChapter = argsStoryChapter;
		this.stageNo = argsStageNo;
		this.stageStep = argsStageStep;

		this.setStageStrings();

		//背景は固定なので、取っておく
		//stageSelectDataStockerScript tmpDatStock = stageSelectManagerGetter.getsceneSelectManager().getDataStocker();


		switch(argsStoryChapter){
		case 1:
		case 2:
			//廊下の場合
			bgImage = largeBGImageLoader.getImage("00");
			
			break;
		case 3:
			//運動場02(陸上競技場)の場合
			bgImage = largeBGImageLoader.getImage("01");
			
			break;


		}


	}
	
	private void setStageStrings(){
		
		switch (storyChapter) {
		case 1:
			stageTitle = "1-1-1 始まり";
			stageComment = "一体ここはどこなのか。\n記憶も曖昧なエンジュだったが、\n今は立ちあがり、歩みを進める他ない。\n\n※　チュートリアル１\nここでは基本的なルール・操作について確認します。";

			break;

		case  2:
			stageTitle = "2-1-1 シュスラン救出";
			stageComment = "";
			break;
		case  3:
			stageTitle = "3-1 1F：体育館";
			stageComment = "";
			break;
		case  4:
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
