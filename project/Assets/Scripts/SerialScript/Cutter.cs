using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutter : MonoBehaviour {

    // ���������Őؒf����ꍇ
  private Vector3 prePos = Vector3.zero;
  private Vector3 prePos2 = Vector3.zero;

  void FixedUpdate ()
  {
      prePos = prePos2;
      prePos2 = transform.position;
  }

    // ���̃R���|�[�l���g��t�����I�u�W�F�N�g��Collider.IsTrigger��ON�ɂ���
    void OnTriggerEnter(Collider other)
    {
        var meshCut = other.gameObject.GetComponent<MeshCut>();
        if (meshCut == null) { return; }

        //������݂̂Őؒf������@�A�����ɂ��Ă͓K�X�ύX
        //var cutPlane = new Plane (transform.right, transform.position);
        //https://nyama41.hatenablog.com/entry/unity_plane_distance
        //�����Őؒf����ꍇ
        //�Ƃ肠����Plane������Ă���

        var cutPlane = new Plane (Vector3.Cross(transform.forward.normalized, prePos - transform.position).normalized, transform.position);
        //����Plane����ɐ�������ʂƂ����v�Z�H���g������
        meshCut.Cut(cutPlane);
    }

}