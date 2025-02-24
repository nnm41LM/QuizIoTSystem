using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerialSend : MonoBehaviour
{
    //SerialHandler.cのクラス
    public SerialHandler serialHandler;
    int i = 0;


    void FixedUpdate() //ここは0.001秒ごとに実行される
    {
        i = i + 1;   //iを加算していって1秒ごとに"1"のシリアル送信を実行
        if (i > 999) //
        {
            serialHandler.Write("1");
            i = 0;
        }
    }
}