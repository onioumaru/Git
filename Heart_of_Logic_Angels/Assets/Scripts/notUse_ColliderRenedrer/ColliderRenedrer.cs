//  ColliderRenedrer.cs
//  http://kan-kikuchi.hatenablog.com/entry/Collider2dVisualization
//
//  Created by kan.kikuchi on 2016.09.24.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// コライダーを表示するためのレンダラー
/// </summary>
public class ColliderRenedrer : MonoBehaviour {

  //LineRenderer用のマテリアル
  private static Material _lineRendererMaterial = null;

  //コライダーを可視化するためのレンダラー
  private LineRenderer _renderer = null;

  //対象のコライダー
  private Collider2D _target = null;

  //=================================================================================
  //Collider2DからLineRendererを生成
  //=================================================================================

  /// <summary>
  /// BoxCollider2DからLineRendererを作成
  /// </summary>
  public static List<ColliderRenedrer> CreateLineRenderer(BoxCollider2D boxCollider2D){

    Vector2 size = boxCollider2D.size, offset = boxCollider2D.offset + (Vector2)boxCollider2D.transform.position;

    List<Vector3> pointList = new List<Vector3>(){
      offset + new Vector2(-size.x * 0.5f , -size.y * 0.5f),
      offset + new Vector2( size.x * 0.5f , -size.y * 0.5f),
      offset + new Vector2( size.x * 0.5f ,  size.y * 0.5f),
      offset + new Vector2(-size.x * 0.5f ,  size.y * 0.5f),
      offset + new Vector2(-size.x * 0.5f , -size.y * 0.5f),
    };

    return CreateLineRenderers(boxCollider2D, pointList, boxCollider2D.isTrigger);
  }

  /// <summary>
  /// EdgeCollider2DからLineRendererを作成
  /// </summary>
  public static List<ColliderRenedrer> CreateLineRenderer(EdgeCollider2D edgeCollider2D){
    List<Vector3> pointList = new List<Vector3>();

    Vector2 offset = edgeCollider2D.offset + (Vector2)edgeCollider2D.transform.position;
    foreach (Vector2 point in edgeCollider2D.points) {
      pointList.Add(offset + (Vector2)point);
    }

    return CreateLineRenderers(edgeCollider2D, pointList, edgeCollider2D.isTrigger);
  }

  /// <summary>
  /// PolygonCollider2DからLineRendererを作成
  /// </summary>
  public static List<ColliderRenedrer> CreateLineRenderer(PolygonCollider2D polygonCollider2D){
    List<Vector3> pointList = new List<Vector3>();

    Vector2 offset = polygonCollider2D.offset + (Vector2)polygonCollider2D.transform.position;
    foreach (Vector2 point in polygonCollider2D.points) {
      pointList.Add(offset + (Vector2)point);
    }
    pointList.Add(offset + (Vector2)polygonCollider2D.points[0]);

    return CreateLineRenderers(polygonCollider2D, pointList, polygonCollider2D.isTrigger);
  }

  /// <summary>
  /// CircleCollider2DからLineRendererを作成
  /// </summary>
  public static List<ColliderRenedrer> CreateLineRenderer(CircleCollider2D circleCollider2D){
    List<Vector3> pointList = new List<Vector3>();

    Vector2 offset = circleCollider2D.offset + (Vector2)circleCollider2D.transform.position;

    int   pointNum  = 36;
    float angleDiff = 360f / pointNum;
    for (int i = 0; i < pointNum; i++)  {
      Vector3 point = offset;

      float angle = angleDiff * i * Mathf.Deg2Rad;
      point.x += circleCollider2D.radius * Mathf.Cos (angle);
      point.y += circleCollider2D.radius * Mathf.Sin (angle);

      pointList.Add(point);
    }
    pointList.Add(pointList[0]);

    return CreateLineRenderers(circleCollider2D, pointList, circleCollider2D.isTrigger);
  }

  //=================================================================================
  //LineRendererの生成
  //=================================================================================

  //対象と頂点の位置を指定して複数のLineRendererを生成
  private static List<ColliderRenedrer> CreateLineRenderers(Collider2D target, List<Vector3> pointList, bool isTrigger){
    List<ColliderRenedrer> colliderRenedrerList = new List<ColliderRenedrer>();

    Vector3 startPoint = pointList[0];

    for (int i = 1; i < pointList.Count; i++) {
      Vector3 endPoint = pointList[i];
      colliderRenedrerList.Add(CreateLineRenderer(target, startPoint, endPoint, isTrigger));
      startPoint = endPoint;
    }

    return colliderRenedrerList;
  }

  //対象と線の開始位置お終了位置を指定してLineRendererを生成
  private static ColliderRenedrer CreateLineRenderer(Collider2D target, Vector3 startPoint, Vector3 endPoint, bool isTrigger){
    if(_lineRendererMaterial == null){
      _lineRendererMaterial = Resources.Load<Material>("ColliderRenedrerMaterial");
    }

    //対象の子にオブジェクトを生成し、そこにColliderRenedrerを作成
    GameObject renedrerObject = new GameObject("ColliderRenedrer");
    renedrerObject.transform.SetParent (target.transform);
    ColliderRenedrer colliderRenedrer = renedrerObject.AddComponent<ColliderRenedrer>();

    //ColliderRenedrerと同じオブジェクトにLineRendererを作成
    LineRenderer lineRenderer = colliderRenedrer.gameObject.AddComponent<LineRenderer>();

    //レイヤーとマテリアル設定
    //lineRenderer.sortingLayerName = ""; //レイヤーを指定
    lineRenderer.sortingOrder     = 100;
    lineRenderer.sharedMaterial   = _lineRendererMaterial;

    //対象が動いたらLineRendererも動くように
    lineRenderer.useWorldSpace = false;

    //線の太さ
    lineRenderer.SetWidth(0.01f, 0.01f);

    //線の色
    Color lineColor = isTrigger ? Color.green : Color.red;
    lineRenderer.SetColors(lineColor, lineColor);

    //頂点を設定
    lineRenderer.SetVertexCount(2);
    lineRenderer.SetPosition   (0, startPoint);
    lineRenderer.SetPosition   (1, endPoint);

    //ColliderRenedrerにレンダラーとコライダーを設定し、返す
    colliderRenedrer.Init(lineRenderer, target);
    return colliderRenedrer;
  }

  //=================================================================================
  //初期化
  //=================================================================================

  public void Init(LineRenderer renderer, Collider2D target){
    _renderer = renderer;
    _target   = target;
    Update();
  }

  //=================================================================================
  //更新
  //=================================================================================

  private void Update () {
    //ターゲットのコライダーとレンダラーを同じ設定にする
    _renderer.enabled = _target.isActiveAndEnabled;
  }

}