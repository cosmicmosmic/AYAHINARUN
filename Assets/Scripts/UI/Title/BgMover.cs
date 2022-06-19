using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgMover : MonoBehaviour
{
    [SerializeField] private RectTransform rect;
    [SerializeField] private float speed;
    private void Update()
    {
        UpdateRect();
    }

    private void UpdateRect()
    {
        var offSet = rect.offsetMin;
        offSet.x -= Time.deltaTime * speed;
        offSet.y -= Time.deltaTime * speed;
        rect.offsetMin = offSet;
    }
}
