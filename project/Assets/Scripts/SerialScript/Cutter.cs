using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutter : MonoBehaviour {

    // 動く方向で切断する場合
  private Vector3 prePos = Vector3.zero;
  private Vector3 prePos2 = Vector3.zero;

  void FixedUpdate ()
  {
      prePos = prePos2;
      prePos2 = transform.position;
  }

    // このコンポーネントを付けたオブジェクトのCollider.IsTriggerをONにする
    void OnTriggerEnter(Collider other)
    {
        var meshCut = other.gameObject.GetComponent<MeshCut>();
        if (meshCut == null) { return; }

        //一方向のみで切断する方法、方向については適宜変更
        //var cutPlane = new Plane (transform.right, transform.position);
        //https://nyama41.hatenablog.com/entry/unity_plane_distance
        //動きで切断する場合
        //とりあえずPlaneを作っている

        var cutPlane = new Plane (Vector3.Cross(transform.forward.normalized, prePos - transform.position).normalized, transform.position);
        //このPlaneを基に生成する面とかを計算？中身を見る
        meshCut.Cut(cutPlane);
    }

}