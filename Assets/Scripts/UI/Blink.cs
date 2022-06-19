using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    [SerializeField] private float blinkTime = 0.5f;
    [SerializeField] private GameObject objBlink;

    private float currTime = 0f;
    private void Update()
    {
        if (objBlink == null)
            return;

        currTime += Time.deltaTime;
        if (currTime > blinkTime)
        {
            currTime = 0f;
            objBlink.SetActive(!objBlink.activeSelf);
        }
    }
}
