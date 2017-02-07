//  Collider2dVisualizationer.cs
//  http://kan-kikuchi.hatenablog.com/entry/Collider2dVisualization
//
//  Created by kan.kikuchi on 2016.09.19.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// LineRendererを使って2dのコライダーを見えるようにするクラス
/// </summary>
public static class Collider2dVisualizationer {

  //生成したLineRendererのリスト
  private static List<ColliderRenedrer> _colliderRenedrerList = new List<ColliderRenedrer>();

  //有効になっているかどうか
  private static bool _isEnabled = true;
  public  static bool  IsEnabled{
    get{return _isEnabled;}
    set{
      _isEnabled = value;
      foreach (ColliderRenedrer colliderRenedrer in _colliderRenedrerList) {
        if(colliderRenedrer != null){
          colliderRenedrer.gameObject.SetActive(_isEnabled);
        }
      }
    }
  }

  //=================================================================================
  //全LineRendererの生成、削除
  //=================================================================================

  /// <summary>
  /// Collider2dを可視化するためのLineRendererを作成
  /// </summary>
  public static void CreateLineRenderer(){
    //全Collider2Dを取得し、コライダーに合わせてLineRendrerを生成
    foreach (BoxCollider2D boxCollider2D in Object.FindObjectsOfType<BoxCollider2D>()) {
      _colliderRenedrerList.AddRange(ColliderRenedrer.CreateLineRenderer(boxCollider2D));
    }
    foreach (EdgeCollider2D edgeCollider2D in Object.FindObjectsOfType<EdgeCollider2D>()) {
      _colliderRenedrerList.AddRange(ColliderRenedrer.CreateLineRenderer(edgeCollider2D));
    }
    foreach (PolygonCollider2D polygonCollider2D in Object.FindObjectsOfType<PolygonCollider2D>()) {
      _colliderRenedrerList.AddRange(ColliderRenedrer.CreateLineRenderer(polygonCollider2D));
    }
    foreach (CircleCollider2D circleCollider2D in Object.FindObjectsOfType<CircleCollider2D>()) {
      _colliderRenedrerList.AddRange(ColliderRenedrer.CreateLineRenderer(circleCollider2D));
    }

    //生成したColliderRenedrerの表示を更新
    IsEnabled = _isEnabled;
  }

  /// <summary>
  /// 生成したLineRendererを全て削除
  /// </summary>
  public static void DeleteLineRenderer(){
    foreach (ColliderRenedrer colliderRenedrer in _colliderRenedrerList) {
      if(colliderRenedrer != null){
        GameObject.Destroy(colliderRenedrer.gameObject);
      }
    }
    _colliderRenedrerList.Clear();
  }

}