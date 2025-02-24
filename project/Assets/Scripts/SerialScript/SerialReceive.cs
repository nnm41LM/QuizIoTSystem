using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�Q�Ƃ��Ή�����I�u�W�F�N�g�ɃA�^�b�`����
public class SerialReceive : MonoBehaviour
{
    //https://qiita.com/yjiro0403/items/54e9518b5624c0030531
    //��LURL��SerialHandler.c�̃N���X
    public SerialHandler serialHandler;

    
    void Start()
    {
        //�M������M�����Ƃ��ɁA���̃��b�Z�[�W�̏������s��
        serialHandler.OnDataReceived += OnDataReceived;
    }

    //��M�����M��(message)�ɑ΂��鏈��
    void OnDataReceived(string message)
    {
        var data = message.Split(
                new string[] { ":" }, System.StringSplitOptions.None);
        //���������������z��œ���Ă���
        //angle:90�Ƃ����ƂQ�ɕ�������
        try
        {
            float angle = float.Parse(data[1]);
            Debug.Log(message);
            Debug.Log(data.Length);//Unity�̃R���\�[���Ɏ�M�f�[�^��\��
            this.transform.rotation = Quaternion.Euler(0,angle,0);
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);//�G���[��\��
        }
    }
}