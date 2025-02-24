using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//２つとも対応するオブジェクトにアタッチする
public class visible_switch : MonoBehaviour
{
    //https://qiita.com/yjiro0403/items/54e9518b5624c0030531
    //上記URLのSerialHandler.cのクラス
    public SerialHandler serialHandler;
    
    public GameObject onigiri;

    void Start()
    {
        //信号を受信したときに、そのメッセージの処理を行う
        serialHandler.OnDataReceived += OnDataReceived;
    }

    //受信した信号(message)に対する処理
    void OnDataReceived(string message)
    {
        var data = message.Split(
                new string[] {""}, System.StringSplitOptions.None);
        //分割した文字列を配列で入れている
        //angle:90とかだと２つに分けられる
        try
        {
            if(data[0] == "1")
            {
                onigiri.SetActive(true);
            }
            else
            {
                onigiri.SetActive(false);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);//エラーを表示
        }
    }
}