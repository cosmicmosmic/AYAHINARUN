using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class EndPanel : MonoBehaviour
{
    [SerializeField] private CanvasGroup cg;

    [SerializeField] private RectTransform trRootWinner;
    [SerializeField] private RectTransform trRootLoser;
    [SerializeField] private Image imgWinner;
    [SerializeField] private Image imgLoser;
    [SerializeField] private Text txtWinnerTime;
    [SerializeField] private Text txtLoserTime;
    [SerializeField] private GameObject objBtnRestart;

    [SerializeField] private Sprite sprAyaNormal;
    [SerializeField] private Sprite sprAyaLose;
    [SerializeField] private Sprite sprAyaWin;
    [SerializeField] private Sprite sprHinaNormal;
    [SerializeField] private Sprite sprHinaLose;
    [SerializeField] private Sprite sprHinaWin;

    public Action onClickRestart;

    private float timeWinner;
    private float timeLoser;
    private bool isAyaWin;

#if UNITY_EDITOR
    public bool _testisAyaWin;
    public float _testWinnerTime;
    public float _testLoserTime;
    [ContextMenu("Open")]
    public void TestOpen()
    {
        Open(_testisAyaWin, _testWinnerTime, _testLoserTime);
    }
#endif

    public void Open(bool _isAyaWin, float _timeWinner, float _timeLoser)
    {
        gameObject.SetActive(true);
        this.isAyaWin = _isAyaWin;
        cg.alpha = 0f;
        txtWinnerTime.gameObject.SetActive(false);
        txtLoserTime.gameObject.SetActive(false);
        objBtnRestart.SetActive(false);

        trRootWinner.anchoredPosition = Vector2.zero;
        trRootLoser.anchoredPosition = Vector2.zero;

        imgWinner.transform.localRotation = Quaternion.identity;
        imgLoser.transform.localRotation = Quaternion.identity;


        if (_isAyaWin)
        {
            imgWinner.sprite = sprAyaNormal;
            imgLoser.sprite = sprHinaNormal;
        }
        else
        {
            imgWinner.sprite = sprHinaNormal;
            imgLoser.sprite = sprAyaNormal;
        }

        this.timeWinner = _timeWinner;
        this.timeLoser = _timeLoser;

        DOTween.To(() => cg.alpha, (x) => cg.alpha = x, 1f, 0.5f).OnComplete(OnCompleteOpen);
    }

    private void OnCompleteOpen()
    {
        var y = trRootWinner.transform.localRotation.eulerAngles.y;
        trRootWinner.transform.DOLocalRotate(new Vector3(0f, y + 180f, 0f), 0.5f).SetDelay(0.3f);
        Invoke("InvokeWin", 0.55f);
        trRootWinner.transform.DOLocalMoveY(75f, 0.5f).SetEase(Ease.OutExpo).OnComplete(() =>
        {
            trRootWinner.transform.DOLocalMoveY(0f, 0.55f).SetEase(Ease.InExpo);
        });

        var y_2 = trRootLoser.transform.localRotation.eulerAngles.y;
        trRootLoser.transform.DOLocalRotate(new Vector3(0f, y_2 + 180f, 0f), 0.5f).SetDelay(0.3f);
        trRootLoser.transform.DOLocalMoveY(75f, 0.5f).SetEase(Ease.OutExpo).OnComplete(() =>
        {
            trRootLoser.transform.DOLocalMoveY(0f, 0.55f).SetEase(Ease.InExpo).OnComplete(() =>
            {
                txtWinnerTime.gameObject.SetActive(true);
                txtLoserTime.gameObject.SetActive(true);

                float t = 0f;
                DOTween.To(() => t, (x) =>
                {
                    t = x;
                    int min = (int)t / 60;
                    var sec = t % 60;
                    txtWinnerTime.text = string.Format("{0:00}:{1:00.00}", min, sec);
                }, timeWinner, 2f);

                float t2 = 0f;
                DOTween.To(() => t2, (x) =>
                {
                    t2 = x;
                    int min = (int)t2 / 60;
                    var sec = t2 % 60;
                    txtLoserTime.text = string.Format("{0:00}:{1:00.00}", min, sec);
                }, timeLoser, 2.3f).OnComplete(() => objBtnRestart.SetActive(true));
            });
        });
    }

    private void InvokeWin()
    {
        if (isAyaWin)
        {
            imgWinner.sprite = sprAyaWin;
            imgLoser.sprite = sprHinaLose;
        }
        else
        {
            imgWinner.sprite = sprHinaWin;
            imgLoser.sprite = sprAyaLose;
        }
    }

    public void OnClickRestart()
    {
        if (onClickRestart != null)
        {
            onClickRestart();
            onClickRestart = null;
        }
    }
}
