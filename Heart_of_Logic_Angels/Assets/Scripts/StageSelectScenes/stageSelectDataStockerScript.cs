using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class stageSelectDataStockerScript : MonoBehaviour {
	public Sprite[] _BackGroungImage;
	
	public Sprite getBackGroundImage(storyProgressEnum argsStoryChapter,int argsStageNo){
		Sprite retSprite = null;

		switch (argsStoryChapter) {
				case storyProgressEnum.newGame:
				case storyProgressEnum.endTutorial_nextSyusuranBattle:
					retSprite = _BackGroungImage [0];
						break;
			
				case storyProgressEnum.addSyusuran_nextHouzuki:
					retSprite =  _BackGroungImage [1];
						break;
				case storyProgressEnum.addHouzuki_nextStageSelect01:

				//StageSelect
				switch (argsStageNo) {
						case 0:
								retSprite =  _BackGroungImage [2];
								break;
						case 1:
								retSprite =  _BackGroungImage [3];
								break;
						}
			
						break;
				case storyProgressEnum.endStageSelect01_nextAlrunaBattle:
						retSprite =  _BackGroungImage [3];
						break;
				}

		return retSprite;
		}
}
