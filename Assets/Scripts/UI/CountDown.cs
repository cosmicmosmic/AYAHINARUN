using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    [SerializeField] private Text txtCountdown;

    private float value;
    private bool isCountdown = false;


    public void StartCountDown()
    {
        isCountdown = true;
    }

    public void StopCountDown()
    {
        isCountdown = false;
    }

    public void ResetCountDown()
    {
        value = 0f;
    }
    public float GetCountDown()
    {
        return value;
    }

    private void Update()
    {
        if (isCountdown == false)
            return;

        UpdateCountdown();
    }

    private void UpdateCountdown()
    {
        value += Time.deltaTime;

        txtCountdown.text = GetCountDownString();
    }

    public string GetCountDownString()
    {
        int min = (int)value / 60;
        var sec = value % 60;
        return string.Format("{0:00}:{1:00.00}", min, sec);
    }
}
