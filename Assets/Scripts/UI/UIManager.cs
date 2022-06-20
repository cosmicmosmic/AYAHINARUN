using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject objTitle;
    [SerializeField] private GameObject objSynopsis;
    [SerializeField] private GameObject objGuide;
    [SerializeField] private GameObject objReady;
    [SerializeField] private float readyTime = 2f;
    [SerializeField] private Text txtReady;
    [SerializeField] private GameObject objGameUI;
    public TitleAnimation title;
    public SynopsisScroll synopsis;
    public EndPanel end;
    public CountDown countDown;
    public GameObject objRemainDist;
    public GameObject objGagebar;

    private Action onCompleteReady;
    public Action onClickSkip;
    public Action onClickOkGuide;

    public void InitUI()
    {
        objTitle.SetActive(false);
        objSynopsis.SetActive(false);
        objGuide.SetActive(false);
        objReady.SetActive(false);
        objGameUI.SetActive(false);
        title.InitAnimation();
        synopsis.onComplete = OnClickSkip;
        countDown.ResetCountDown();
        end.gameObject.SetActive(false);
    }

    public void ShowTitle(bool _value)
    {
        objTitle.SetActive(_value);
        if (_value)
        {
            title.Play();
        }

    }

    public void ShowSynopsis(bool _value)
    {
        objSynopsis.SetActive(_value);
        synopsis.ShowSynopsis();
    }

    public void ShowGuide(bool _value)
    {
        objGuide.SetActive(_value);
    }

    public void ShowReady(Action _onCompleteReady)
    {
        this.onCompleteReady = _onCompleteReady;
        StartCoroutine(CorReady());
    }

    private IEnumerator CorReady()
    {
        objReady.SetActive(true);
        txtReady.text = "READY?";
        yield return new WaitForSeconds(readyTime);
        txtReady.text = "3";
        yield return new WaitForSeconds(1f);
        txtReady.text = "2";
        yield return new WaitForSeconds(1f);
        txtReady.text = "1";
        yield return new WaitForSeconds(1f);
        txtReady.text = "GO!";
        yield return new WaitForSeconds(1f);
        objReady.SetActive(false);
        countDown.StartCountDown();
        if (onCompleteReady != null)
        {
            onCompleteReady();
            onCompleteReady = null;
        }
    }

    public void ShowGameUI(bool _value)
    {
        objGameUI.SetActive(_value);
        countDown.gameObject.SetActive(true);
        objRemainDist.SetActive(true);
        objGagebar.SetActive(true);
    }


    public void OnClickSkip()
    {
        if (onClickSkip != null)
        {
            onClickSkip();
            onClickSkip = null;
        }
    }

    public void OnClickOkGuide()
    {
        if(onClickOkGuide != null)
        {
            onClickOkGuide();
            onClickOkGuide = null;
        }
    }
}
