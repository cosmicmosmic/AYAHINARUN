using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Runner))]
public class RunnerRotator : MonoBehaviour
{
    private Runner runner;

    [SerializeField] private Vector3 rotateStartValue;
    [SerializeField] private Vector3 rotateEndValue;
    [SerializeField] private float rotateTime = 2f;


    private void Awake()
    {
        runner = GetComponent<Runner>();
        Rotate_1();
    }

    private void Rotate_1()
    {
        runner.trBody.DORotate(rotateStartValue, rotateTime).OnComplete(Rotate_2);
    }

    private void Rotate_2()
    {
        runner.trBody.DORotate(rotateEndValue, rotateTime).OnComplete(Rotate_1);
    }
}
