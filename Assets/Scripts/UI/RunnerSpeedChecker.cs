using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunnerSpeedChecker : MonoBehaviour
{
    private Camera cam;

    [SerializeField] private Runner runner;
    [SerializeField] private Slider slider;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        UpdateFollowTarget();
        UpdateSpeedGage();
    }

    private void UpdateFollowTarget()
    {
        transform.position = cam.WorldToScreenPoint(runner.transform.position);
    }
    private void UpdateSpeedGage()
    {
        slider.value = runner.GetCurrSpeed() / runner.GetMaxSpeed();
    }
}
