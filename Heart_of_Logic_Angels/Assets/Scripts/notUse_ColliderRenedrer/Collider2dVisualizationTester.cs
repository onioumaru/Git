using UnityEngine;
using System.Collections;

public class Collider2dVisualizationTester : MonoBehaviour {

	/// <summary>
	/// レンダラーを表示
	/// </summary>
	public void On(){
		Collider2dVisualizationer.IsEnabled = true;
	}

	/// <summary>
	/// レンダラーを非表示
	/// </summary>
	public void Off(){
		Collider2dVisualizationer.IsEnabled = false;
	}

	/// <summary>
	/// コライダーを元にレンダラーを作成
	/// </summary>
	public void Create(){
		Collider2dVisualizationer.CreateLineRenderer ();
	}

	/// <summary>
	/// 作成したレンダラーを削除
	/// </summary>
	public void Delete(){
		Collider2dVisualizationer.DeleteLineRenderer ();
	}


	void OnMouseUp(){
		Debug.Log ("click");
		this.Create ();
		this.On ();
	}
}