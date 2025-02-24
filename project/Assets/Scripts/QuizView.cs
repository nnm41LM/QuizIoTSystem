using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizView : MonoBehaviour
{
    [SerializeField] private GameObject[] _cubes;//マテリアルを買えるゲームオブジェクト
    [SerializeField] private List<Material> _standByMaterials = new List<Material>();//マテリアルを格納するところ
    [SerializeField] private List<Material> _lightMaterials = new List<Material>();
    [SerializeField] private AudioClip _soundEffectClip;
    private AudioSource _audioSource;

    #region  公開プロパティ
    public int CubesCount { get { return _cubes.Length; } }
    #endregion

    public void Init()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// 対応するマテリアルを設定する
    /// </summary>
    /// <param name="index"></param>
    public void SetMaterial(int index)
    {
        _cubes[index].GetComponent<Renderer>().material = _lightMaterials[index];//対応するcubeのマテリアルをLightに変更する
        _cubes[index].transform.GetChild(0).gameObject.SetActive(true);//スポットライトをつける
        _audioSource.PlayOneShot(_soundEffectClip);
    }

    /// <summary>
    /// マテリアルを初期状態にリセットする
    /// </summary>
    public void ResetCubesMaterial()
    {
        for (var i = 0; i < _cubes.Length; i++)
        {
            _cubes[i].GetComponent<Renderer>().material = _standByMaterials[i];
            _cubes[i].transform.GetChild(0).gameObject.SetActive(false);// スポットライトを消す
        }
    }
}
