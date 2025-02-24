using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuizModel
{
    private SerialHandleUtility _serialUtil;
    private int[] _answerRights;

    private UnityEvent<int> _onMaterialChangeSignalReceived = new UnityEvent<int>();
    private UnityEvent _onResetSignalReceived = new UnityEvent();
    private bool _isEmptyAnsweringRight = true;

    #region  公開プロパティ
    public UnityEvent<int> OnMaterialChangeSignalReceived { get => _onMaterialChangeSignalReceived; }
    public UnityEvent OnResetSignalReceived { get => _onResetSignalReceived; }
    #endregion

    public QuizModel(string portName, int baudRate, int cubeCount)
    {
        _serialUtil = new SerialHandleUtility(portName, baudRate);
        _serialUtil.OnSerialDataReceived.AddListener(ReceiveAnswerSignal);
        _answerRights = new int[cubeCount + 1];
    }

    public void TestAnswerSignal(string message)
    {
        ReceiveAnswerSignal(message);
    }

    private void ReceiveAnswerSignal(string message)
    {
        var data = message.Split(new string[] { ":" }, System.StringSplitOptions.None);
        if (_answerRights.Length != data.Length) Array.Resize(ref _answerRights, data.Length);

        try
        {
            for (int i = 0; i < _answerRights.Length; i++)
            {
                _answerRights[i] = int.Parse(data[i]);
            }
            SetAnswerState(_answerRights);
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);//エラーを表示
        }
    }

    private void SetAnswerState(int[] answerRights)
    {
        int index = Array.IndexOf(answerRights, 1);//どこにも1がないときは-1が返される
        if (_isEmptyAnsweringRight)
        {
            if (index == -1 || index == answerRights.Length - 1) return;

            if (answerRights[index] == 1)//mode配列のどこかが1になったらその値を回答者とする
            {
                _onMaterialChangeSignalReceived?.Invoke(index);
                _isEmptyAnsweringRight = false;
            }
        }
        //配列の末尾はリセットボタンの扱いにする
        if (answerRights[answerRights.Length - 1] == 1)
        {
            _onResetSignalReceived?.Invoke();
            _isEmptyAnsweringRight = true;
        }
    }
}
