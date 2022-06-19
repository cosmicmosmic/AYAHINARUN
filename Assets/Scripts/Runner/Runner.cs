using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Runner : MonoBehaviour
{
    public enum RunnerState
    {
        READY,
        IDLE,
        RUN_A,
        RUN_B,
        GOAL
    }
    public RunnerState State { get; private set; }

    public Transform trBody;

    [SerializeField] private SpriteRenderer sprRenderer;
    [SerializeField] private Sprite sprFront;
    [SerializeField] private Sprite sprReady;
    [SerializeField] private Sprite sprRunA;
    [SerializeField] private Sprite sprRunB;

    private float speed = 0f;
    [SerializeField] private float speedIncreaseFactor = 1f;
    [SerializeField] private float speedDecreaseFactor = 1f;
    [SerializeField] private float maxSpeed = 0.04f;
    private float minSpeed = 0f;

    private void Update()
    {
        if (State == RunnerState.READY)
        {
            return;
        }

        if (State == RunnerState.RUN_A || State == RunnerState.RUN_B || State == RunnerState.IDLE || State == RunnerState.GOAL)
        {
            speed -= Time.deltaTime * speedDecreaseFactor * speed;
            speed = Mathf.Clamp(speed, minSpeed, maxSpeed);
            transform.Translate(GetVelocity());
        }

        if (State == RunnerState.GOAL)
        {
            return;
        }

        if (speed <= 0.003f)
        {
            sprRenderer.sprite = sprReady;
            State = RunnerState.IDLE;
        }
    }

    private Vector3 GetVelocity()
    {
        return Vector3.right * speed;
    }

    public void GetReady()
    {
        transform.localPosition = Vector3.zero;
        State = RunnerState.READY;
        sprRenderer.sprite = sprReady;
    }

    public void StartRun()
    {
        State = RunnerState.RUN_B;
        SpeedUp_A();
        speed = 0.03f;
    }

    public void Goal()
    {
        State = RunnerState.GOAL;
        sprRenderer.sprite = sprReady;
    }

    public void SpeedUp_A()
    {
        if (State == RunnerState.IDLE || State == RunnerState.RUN_B)
        {
            sprRenderer.sprite = sprRunA;
            State = RunnerState.RUN_A;
            speed += Time.deltaTime * speedIncreaseFactor;
        }
    }

    public void SpeedUp_B()
    {
        if (State == RunnerState.IDLE || State == RunnerState.RUN_A)
        {
            sprRenderer.sprite = sprRunB;
            State = RunnerState.RUN_B;
            speed += Time.deltaTime * speedIncreaseFactor;
        }
    }

    public float GetMaxSpeed()
    {
        return maxSpeed;
    }

    public float GetCurrSpeed()
    {
        return speed;
    }
}
