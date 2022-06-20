using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RunnerScaler : MonoBehaviour
{
    private Runner runner;

    [SerializeField] private float scaleStartValue;
    [SerializeField] private float scaleEndValue;
    [SerializeField] private float scaleTime;

    private void Awake()
    {
        runner = GetComponent<Runner>();
        Scale_1();
    }

    private void Scale_1()
    {
        runner.trBody.DOScaleY(scaleStartValue, scaleTime).OnComplete(Scale_2);
    }

    private void Scale_2()
    {
        runner.trBody.DOScaleY(scaleEndValue, scaleTime).OnComplete(Scale_1);
    }
}
