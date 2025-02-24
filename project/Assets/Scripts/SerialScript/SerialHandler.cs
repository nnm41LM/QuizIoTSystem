using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;

public class SerialHandler : MonoBehaviour
{
    public delegate void SerialDataReceivedEventHandler(string message);
    public event SerialDataReceivedEventHandler OnDataReceived;

    //ポート名
    //例
    //Linuxでは/dev/ttyUSB0
    //windowsではCOM1
    //Macでは/dev/tty.usbmodem1421など
    public string portName = "COM4";
    public int baudRate = 9600;

    //これは２つのオブジェクトで同じポートを指定すると一つははじかれる

    private SerialPort serialPort_;
    private Thread thread_;
    private bool isRunning_ = false;

    private string message_;
    private bool isNewMessageReceived_ = false;

    void Awake()
    {
        Open();
       
    }

    void Update()
    {
        if (isNewMessageReceived_)
        {
            OnDataReceived(message_);
        }
        isNewMessageReceived_ = false;
    }

    void OnDestroy()
    {
        Close();
    }

    //ポート名を指定してSerialPortを開く
    private void Open()
    {
        serialPort_ = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
        //または
        //serialPort_ = new SerialPort(portName, baudRate);
        serialPort_.Open();

        isRunning_ = true;

        thread_ = new Thread(Read);
        thread_.Start();
        //タイムアウト処理、ミリ秒
        //読み取り操作（ReadLine()）が完了していないとき、タイムアウトになるまでのミリ秒を取得する
        serialPort_.ReadTimeout = 2000;//2秒ごとにexceptionとなる

    }

    private void Close()
    {
        isNewMessageReceived_ = false;
        isRunning_ = false;

        //タスクの終了待ち
        //新しいものだとThreadではなくTaskらしい
        if (thread_ != null && thread_.IsAlive)
        {
            thread_.Join();
        }

        //タスクが終了してからSerialPortを閉じる
        if (serialPort_ != null && serialPort_.IsOpen)
        {
            serialPort_.Close();
            serialPort_.Dispose();
        }
    }

    private void Read()
    {
        while (isRunning_ && serialPort_ != null && serialPort_.IsOpen)
        {
            try
            {
                message_ = serialPort_.ReadLine();//左辺stringに読み込んだ内容を代入
                isNewMessageReceived_ = true;
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e.Message);//ただタイムアウト入れるとこの警告が出るが、警告を消したいならこの一文を消して空にすればいい
            }
        }
    }

    public void Write(string message)//UnityからArduinoへ送信するときに使う関数
    {
        try
        {
            serialPort_.Write(message);
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }
}