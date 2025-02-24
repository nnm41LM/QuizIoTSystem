using UnityEngine;
using System;//Array���g������
using System.Collections.Generic;

//�Q�Ƃ��Ή�����I�u�W�F�N�g�ɃA�^�b�`����
public class SerialReceive_Button : MonoBehaviour
{
    //https://qiita.com/yjiro0403/items/54e9518b5624c0030531
    //��LURL��SerialHandler.c�̃N���X
    public SerialHandler serialHandler;

    private int[] mode = new int[5];//�����͕ύX����K�v����

    public GameObject[] cube;//�}�e���A���𔃂���Q�[���I�u�W�F�N�g
    public Material[] materials;//�}�e���A�����i�[����Ƃ���
    public ParticleSystem particle;

    private bool frag = true;//�𓚌��p�̃t���O 

    public AudioClip sound1;
    private AudioSource audioSource;

    void Start()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
        //�M������M�����Ƃ��ɁA���̃��b�Z�[�W�̏������s��
        serialHandler.OnDataReceived += OnDataReceived;
        //Debug.Log("�z��̒���:" + materials.Length);
    }

    //��M�����M��(message)�ɑ΂��鏈��
    void OnDataReceived(string message)
    {
        var data = message.Split(
                new string[] { ":" }, System.StringSplitOptions.None);
        Array.Resize(ref mode, data.Length);
        //���������������z��œ���Ă���
        try
        {
            //string�^����float�^�ɕϊ�
            for (int i = 0; i < mode.Length; i++)
            {
                mode[i] = int.Parse(data[i]);//�}�e���A���`�F���W�̕ϐ�
            }
            MaterialChange(mode);//�{�^���������ꂽ��}�e���A���̎�ނ�ύX����
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);//�G���[��\��
        }
    }
    //void �͖߂�l���Ȃ����Ƃ��w���Ă��邾���ŁA�����͕ʂɎg���đ��v
    void MaterialChange(int[] mode)
    {
        int index = Array.IndexOf(mode, 1);//�ǂ��ɂ�1���Ȃ��Ƃ���-1���Ԃ����
        if (frag == true)
        {
            if (index == -1 || index == mode.Length - 1)//���������Ă��Ȃ��Ƃ��ƃ��Z�b�g�{�^�����������Ƃ���Warning�����
                return;
            else
            {
                if (mode[index] == 1)//mode�z��̂ǂ�����1�ɂȂ�����
                {
                    cube[index].GetComponent<Renderer>().material = materials[2 * index + 1];//�Ή�����cube�̃}�e���A����Light�ɕύX����
                    cube[index].transform.GetChild(0).gameObject.SetActive(true);//�X�|�b�g���C�g������
                    audioSource.PlayOneShot(sound1);
                    frag = false;//���������悤�ɂ��A����cube�͈ȍ~����Ȃ��悤�ɂ���
                }
            }
        }
        //�z��̖����̓��Z�b�g�{�^���̈����ɂ���
        if (mode[mode.Length - 1] == 1)
        {
            for (int i = 0; i < mode.Length - 1; i++)
            {
                cube[i].GetComponent<Renderer>().material = materials[2 * i];
                cube[i].transform.GetChild(0).gameObject.SetActive(false);
                frag = true;//�t���O�����ɖ߂�
            }
        }

    }
}