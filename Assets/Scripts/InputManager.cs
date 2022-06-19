using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CnControls;

public class InputManager : MonoBehaviour
{
    [SerializeField] private KeyCode inputA = KeyCode.N;
    [SerializeField] private KeyCode inputB = KeyCode.M;

    public Action callbackAnyKeyDown;
    public Action callbackInputA;
    public Action callbackInputB;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return))
        {
            InputAnyKey();
        }

        if (Input.GetKeyDown(inputA))
        {
            Input_A();
        }
        else if (Input.GetKeyDown(inputB))
        {
            Input_B();
        }
    }

    private void InputAnyKey()
    {
        if (callbackAnyKeyDown != null)
            callbackAnyKeyDown();
    }

    private void Input_A()
    {
        if (callbackInputA != null)
            callbackInputA();
    }

    private void Input_B()
    {
        if (callbackInputB != null)
            callbackInputB();
    }
}
