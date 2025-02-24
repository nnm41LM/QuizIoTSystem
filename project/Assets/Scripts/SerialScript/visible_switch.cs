using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�Q�Ƃ��Ή�����I�u�W�F�N�g�ɃA�^�b�`����
public class visible_switch : MonoBehaviour
{
    //https://qiita.com/yjiro0403/items/54e9518b5624c0030531
    //��LURL��SerialHandler.c�̃N���X
    public SerialHandler serialHandler;
    
    public GameObject onigiri;

    void Start()
    {
        //�M������M�����Ƃ��ɁA���̃��b�Z�[�W�̏������s��
        serialHandler.OnDataReceived += OnDataReceived;
    }

    //��M�����M��(message)�ɑ΂��鏈��
    void OnDataReceived(string message)
    {
        var data = message.Split(
                new string[] {""}, System.StringSplitOptions.None);
        //���������������z��œ���Ă���
        //angle:90�Ƃ����ƂQ�ɕ�������
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
            Debug.LogWarning(e.Message);//�G���[��\��
        }
    }
}