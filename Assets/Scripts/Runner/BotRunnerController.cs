using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotRunnerController : MonoBehaviour
{
    [SerializeField] private Runner runner;
    [SerializeField] private Runner playerRunner;
    [SerializeField] private float delayFactor = 0.2f;

    [SerializeField] private float defaultDelayTime = 0.2f;

    private float delayTime = 0.2f;
    private float currTime;
    private bool isOn = false;
    private int count = 0;

    public void StartBot()
    {
        currTime = 0f;
        isOn = true;
        count = 0;
    }

    private void Update()
    {
        if (isOn)
        {
            currTime += Time.deltaTime;
            if (currTime > delayTime)
            {
                currTime = 0f;
                UpdateBot();
            }
        }
    }

    private void UpdateBot()
    {
        if (count % 2 == 0)
        {
            runner.SpeedUp_A();
        }
        else
        {
            runner.SpeedUp_B();
        }

        count++;


        delayTime = defaultDelayTime - (playerRunner.transform.position.x - runner.transform.position.x) * delayFactor + Random.Range(-0.1f, 0.0f);


        if(count % 100 > 80)
        {
            delayTime = delayTime - 0.1f;
        }
    }
}
