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
		case 3:
			//廊下の場合
			bgImage = largeBGImageLoader.getImage("00");
			break;

		case 4:
			bgImage = largeBGImageLoader.getImage("02");
			break;

		case 5:
		case 10:
			//運動場02(陸上競技場)の場合
			bgImage = largeBGImageLoader.getImage("01");
			break;

		}
	}
	
	private void setStageStrings(){
		
		switch (storyChapter) {
		case 1:
			stageTitle = "1 : 始まり";
			stageComment = "　一体ここはどこなのか。\n記憶も曖昧なエンジュだったが、\n今は立ちあがり、歩みを進める他ない。\n\n※　チュートリアル１\nここでは基本的なルール・操作について確認します。";

			break;

		case  2:
			stageTitle = "2 : シュスラン救出";
			stageComment = "　謎の生命体改め、柄の悪いトサカに絡まれてしまったエンジュ。\n　何だかよく分からないが、謎の声もイイって言っていたので、とりあえず ぶちのめしておこう。\n\n※　チュートリアル２\n基本的な戦闘について確認します。";
			break;
		case  3:
			stageTitle = "3 : 2F 廊下";
			stageComment = " 階段の下から騒がしく声が聞こえる。\n　先ほどの青い鳥の様だが、\nかなりの数がいるような雰囲気だ。\n";
			break;
		case  4:
			stageTitle = "4 : わたり廊下";
			stageComment = " アルルーナの助言の通り、廊下の突き当たりまで来た。\n今まで何も無かった野外に、いつの間にか土の地面が広がっている。\nこの渡り廊下から歩いて外に出れそうだ。";
			break;
		case  5:
			stageTitle = "5 : 陸上競技場";
			stageComment = " シュスランはこの先から何か気持ち悪いものが流れてくるのを感じていた。\n　その先に何かあるかもしれない。";
			break;

		case  10:
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
