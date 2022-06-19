using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class TitleAnimation : MonoBehaviour
{
    [SerializeField] private Transform trTitle;
    [SerializeField] private float trTitleStartPosY = 1000f;
    [SerializeField] private float trTitleDuration = 2f;
    [SerializeField] private RectTransform trAya;
    [SerializeField] private float trAyaStartPosY = -1000f;
    [SerializeField] private float trAyaEndPosY = -15f;
    [SerializeField] private float trAyaLoopPosY = -45f;
    [SerializeField] Blink blink;

    public bool isPlaying = false;

    public void InitAnimation()
    {
        blink.enabled = false;
        var posTitle = trTitle.localPosition;
        posTitle.y = trTitleStartPosY;
        trTitle.localPosition = posTitle;

        var posAya = trAya.anchoredPosition;
        posAya.y = trAyaStartPosY;
        trAya.anchoredPosition = posAya;
    }

    public void Play()
    {
        if (isPlaying)
            return;

        isPlaying = true;
        StartCoroutine(WaitTimeCor());
    }

    private float waitTime = 0.5f;
    private IEnumerator WaitTimeCor()
    {
        yield return new WaitForSeconds(waitTime);
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        trTitle.DOLocalMoveY(0f, trTitleDuration).SetEase(Ease.OutBounce).OnComplete(() =>
        {
            blink.enabled = true;
        });

        var endValue = trAya.anchoredPosition;
        endValue.y = trAyaEndPosY;
        DOTween.To(() => trAya.anchoredPosition, (a) => trAya.anchoredPosition = a, endValue, trTitleDuration).OnComplete(() =>
        {
            AyaLoop_1();
            isPlaying = false;
        });
    }

    private void AyaLoop_1()
    {
        var endValue = trAya.anchoredPosition;
        endValue.y = trAyaLoopPosY;
        DOTween.To(() => trAya.anchoredPosition, (a) => trAya.anchoredPosition = a, endValue, trTitleDuration).OnComplete(AyaLoop_2);
    }

    private void AyaLoop_2()
    {
        var endValue = trAya.anchoredPosition;
        endValue.y = trAyaEndPosY;
        DOTween.To(() => trAya.anchoredPosition, (a) => trAya.anchoredPosition = a, endValue, trTitleDuration).OnComplete(AyaLoop_1);
    }
}
