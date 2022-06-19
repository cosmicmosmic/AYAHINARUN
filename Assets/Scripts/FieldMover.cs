using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldMover : MonoBehaviour
{
    [SerializeField] private Transform trCamera;

    [SerializeField] private Transform trGround_1;
    [SerializeField] private Transform trGround_2;

    private float swapSize = 20f;

    private void Update()
    {
        if (trCamera == null)
            return;

        if (trCamera.position.x > GetFront().position.x)
        {
            SwapTransform();
        }
    }

    private Transform GetBehind()
    {
        if (trGround_1.transform.position.x < trGround_2.transform.position.x)
        {
            return trGround_1;
        }
        else
        {
            return trGround_2;
        }
    }

    private Transform GetFront()
    {
        if (trGround_1.transform.position.x < trGround_2.transform.position.x)
        {
            return trGround_2;
        }
        else
        {
            return trGround_1;
        }
    }

    private void SwapTransform()
    {
        var pos = GetBehind().transform.position;
        pos.x += swapSize * 2;

        var tr = GetBehind();
        tr.position = pos;
    }
}
