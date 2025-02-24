using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizPresenter : MonoBehaviour
{
    private QuizModel _model;
    private QuizView _view;

    [SerializeField] private string _portName = "COM4";
    [SerializeField] private int _baudRate = 9600;

    void Start()
    {
        _view = FindObjectOfType<QuizView>();
        _view.Init();

        _model = new QuizModel(_portName, _baudRate, _view.CubesCount);

        _model.OnMaterialChangeSignalReceived.AddListener(_view.SetMaterial);
        _model.OnResetSignalReceived.AddListener(_view.ResetCubesMaterial);
    }

    private void Update()
    {
        TestMessage();
    }

    private void TestMessage()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log(1);
            _model.TestAnswerSignal("1:0:0:0:0");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log(2);
            _model.TestAnswerSignal("0:1:0:0:0");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log(3);
            _model.TestAnswerSignal("0:0:1:0:0");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log(4);
            _model.TestAnswerSignal("0:0:0:1:0");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Debug.Log(0);
            _model.TestAnswerSignal("0:0:0:0:1");
        }
    }
}
