using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerialSend : MonoBehaviour
{
    //SerialHandler.c�̃N���X
    public SerialHandler serialHandler;
    int i = 0;


    void FixedUpdate() //������0.001�b���ƂɎ��s�����
    {
        i = i + 1;   //i�����Z���Ă�����1�b���Ƃ�"1"�̃V���A�����M�����s
        if (i > 999) //
        {
            serialHandler.Write("1");
            i = 0;
        }
    }
}