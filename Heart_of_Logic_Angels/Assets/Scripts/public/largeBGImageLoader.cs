using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public static class largeBGImageLoader {
	public static Sprite getImage(string srgsNoStr){

	string fFullPath = "";
	Sprite retSprite = null;
	Sprite[] charaBase;

	switch (srgsNoStr) {
		case "00":
			fFullPath = "advBackGroungImage/廊下背景02";
			break;
			
		case "01":
			//運動場02
			fFullPath = "advBackGroungImage/運動場02";
			break;

		case "02":
			//渡り廊下01
			fFullPath = "advBackGroungImage/渡り廊下01";
			break;

		case "03":
			fFullPath = "advBackGroungImage/03_植物園";
			break;

		case "04":
			fFullPath = "advBackGroungImage/shadowRoom";
			break;

		case "05":
			fFullPath = "advBackGroungImage/bassballStudio";
			break;

		case "06":
			fFullPath = "advBackGroungImage/hokensitu";
			break;

		case "07":
			fFullPath = "advBackGroungImage/図書館背景";
			break;

		case "08":
			fFullPath = "advBackGroungImage/体育館";
			break;

		case "09":
			fFullPath = "advBackGroungImage/rouka002";
			break;

		case "10":
			fFullPath = "advBackGroungImage/rouka003";
			break;

			//
		case "99":
			//透明画像のダミー
			fFullPath = "pictChractorStanding/blinkObj";
			break;
		}
		
		charaBase = Resources.LoadAll<Sprite>(fFullPath);
		retSprite = charaBase[0];

		return retSprite;

	}
}

