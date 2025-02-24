using UnityEngine;
using UnityEngine.Events;
using System.IO.Ports;
using Cysharp.Threading.Tasks;
using System.Threading;

public class SerialHandleUtility
{
    private SerialPort _serialPort;
    private UnityEvent<string> _onSerialDataReceived = new UnityEvent<string>();
    private string _receivedMessage = "";
    private CancellationTokenSource _cts;

    #region  公開プロパティ
    public UnityEvent<string> OnSerialDataReceived { get => _onSerialDataReceived; }
    #endregion

    // コンストラクタ
    public SerialHandleUtility(string portName, int baudRate)
    {
        _serialPort = new SerialPort(portName, baudRate);
        _serialPort.Open();

        // シリアル通信の受信ループ開始
        _cts = new CancellationTokenSource();
        ReceiveSerialDataAsync(_cts.Token).Forget();
    }

    /// <summary>
    /// 停止処理
    /// </summary>
    public void Close()
    {
        _cts.Cancel();
        if (_serialPort != null && _serialPort.IsOpen)
        {
            _serialPort.Close();
        }
    }

    /// <summary>
    /// シリアル通信による受信
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    private async UniTaskVoid ReceiveSerialDataAsync(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            if (_serialPort.IsOpen)
            {
                try
                {
                    token.ThrowIfCancellationRequested();
                    _receivedMessage = await UniTask.RunOnThreadPool(() => _serialPort.ReadLine(), cancellationToken: token);
                    _onSerialDataReceived?.Invoke(_receivedMessage);
                }
                catch (System.Exception ex)
                {
                    Debug.LogError($"Serial Error: {ex.Message}");
                }
            }
            await UniTask.Yield(PlayerLoopTiming.Update, token);
        }
    }
}
