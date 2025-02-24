using UnityEngine;
using System;//Arrayを使うため

//２つとも対応するオブジェクトにアタッチする
public class SerialReceive_sample : MonoBehaviour
{
    //https://qiita.com/yjiro0403/items/54e9518b5624c0030531
    //上記URLのSerialHandler.cのクラス
    public SerialHandler serialHandler;

    private int[] mode = new int[5];

    public GameObject[] cube;//マテリアルを買えるゲームオブジェクト
    public Material[] materials;//マテリアルを格納するところ
    public ParticleSystem particle;

    private bool frag = true;//解答権用のフラグ 

    public AudioClip sound1;
    private AudioSource audioSource;

    void Start()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
        //信号を受信したときに、そのメッセージの処理を行う
        serialHandler.OnDataReceived += OnDataReceived;
    }

    //受信した信号(message)に対する処理
    void OnDataReceived(string message)
    {
        var data = message.Split(
                new string[] { ":" }, System.StringSplitOptions.None);
        //分割した文字列を配列で入れている
        //angle:90とかだと２つに分けられる
        try
        {
            //string型からfloat型に変換
            for (int i = 0; i < 5; i++)
            {
                mode[i] = int.Parse(data[i]);//マテリアルチェンジの変数
            }
            //Debug.Log("data:" + string.Join(", ", data));

            MaterialChange(mode);//ボタンが押されたらマテリアルの種類を変更する
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);//エラーを表示
        }
    }
    //void は戻り値がないことを指しているだけで、引数は別に使って大丈夫
    void MaterialChange(int[] mode)
    {
        int index = Array.IndexOf(mode, 1);//どこにも1がないときは-1が返される
        if (frag == true)
        {
            /*以下のコードは問題なく動作
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
            //縮めた物
            
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
        //配列の末尾はリセットボタンの扱いにする
       
        
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