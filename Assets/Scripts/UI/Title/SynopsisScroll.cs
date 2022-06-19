using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SynopsisScroll : MonoBehaviour
{
    [SerializeField] private Transform trSynopsis;

    [SerializeField] private float speed = 1f;
    [SerializeField] private float startPosY = -300f;
    [SerializeField] private float endPosY = 500f;

    public Action onComplete;

    public void ShowSynopsis()
    {
        SetPostion(startPosY);
    }

    private void Update()
    {
        var currPos = trSynopsis.localPosition.y;
        currPos += Time.deltaTime * speed;
        SetPostion(currPos);

        if (currPos > endPosY)
        {
            if (onComplete != null)
            {
                onComplete();
                onComplete = null;
            }
        }
    }

    private void SetPostion(float value)
    {
        var pos = trSynopsis.localPosition;
        pos.y = value;
        trSynopsis.localPosition = pos;
    }
}
