using UnityEngine;
using System;//Array���g������

//�Q�Ƃ��Ή�����I�u�W�F�N�g�ɃA�^�b�`����
public class SerialReceive_sample : MonoBehaviour
{
    //https://qiita.com/yjiro0403/items/54e9518b5624c0030531
    //��LURL��SerialHandler.c�̃N���X
    public SerialHandler serialHandler;

    private int[] mode = new int[5];

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
            //string�^����float�^�ɕϊ�
            for (int i = 0; i < 5; i++)
            {
                mode[i] = int.Parse(data[i]);//�}�e���A���`�F���W�̕ϐ�
            }
            //Debug.Log("data:" + string.Join(", ", data));

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
            /*�ȉ��̃R�[�h�͖��Ȃ�����
            if (mode[0] == 1)
            {
                cube[0].GetComponent<Renderer>().material = materials[1];
                cube[0].transform.GetChild(0).gameObject.SetActive(true);
                audioSource.PlayOneShot(sound1);
                frag = false;
            }
            else if (mode[1] == 1)
            {
                cube[1].GetComponent<Renderer>().material = materials[3];
                cube[1].transform.GetChild(0).gameObject.SetActive(true);
                audioSource.PlayOneShot(sound1);
                frag = false;
            }
            else if (mode[2] == 1)
            {
                cube[2].GetComponent<Renderer>().material = materials[5];
                cube[2].transform.GetChild(0).gameObject.SetActive(true);
                audioSource.PlayOneShot(sound1);
                frag = false;
            }
            else if (mode[3] == 1)
            {
                cube[3].GetComponent<Renderer>().material = materials[7];
                cube[3].transform.GetChild(0).gameObject.SetActive(true);
                audioSource.PlayOneShot(sound1);
                frag = false;
            }
            */
            //�k�߂���
            
            if (index == -1)
                return;
            else
            {
                if (mode[index] == 1)
                {
                    cube[index].GetComponent<Renderer>().material = materials[2 * index + 1];
                    cube[index].transform.GetChild(0).gameObject.SetActive(true);
                    audioSource.PlayOneShot(sound1);
                    frag = false;
                }
            }
        }
        //Debug.Log("index" + index);
        //�z��̖����̓��Z�b�g�{�^���̈����ɂ���
       
        
        if (mode[mode.Length - 1] == 1)
        {
            for (int i = 0; i < 4; i++)
            {
                cube[i].GetComponent<Renderer>().material = materials[2 * i];
                cube[i].transform.GetChild(0).gameObject.SetActive(false);
                frag = true;
            }
        }
        
    }
}