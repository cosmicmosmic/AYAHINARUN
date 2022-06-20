using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Rotator : MonoBehaviour
{
    [SerializeField] private Vector3 rotateStartValue;
    [SerializeField] private Vector3 rotateEndValue;
    [SerializeField] private float rotateTime = 2f;


    private void Awake()
    {
        Rotate_1();
    }

    private void Rotate_1()
    {
        transform.DOLocalRotate(rotateStartValue, rotateTime).OnComplete(Rotate_2);
    }

    private void Rotate_2()
    {
        transform.DOLocalRotate(rotateEndValue, rotateTime).OnComplete(Rotate_1);
    }
}
