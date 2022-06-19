using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform trTarget;
    [SerializeField] private float speed = 0.05f;
    [SerializeField] private float initPosX = 3.5f;

    public bool isFollow = false;

    public void Init()
    {
        isFollow = false;
        var pos = transform.position;
        pos.x = initPosX;
        transform.position = pos;
    }

    public void ChangeTarget(Transform _tr)
    {
        this.trTarget = _tr;
    }

    private void Update()
    {
        if (trTarget == null)
            return;

        if (isFollow)
        {
            var pos = transform.position;
            var posX = transform.position.x;

            posX = Mathf.Lerp(posX, trTarget.position.x, speed);

            pos.x = posX;
            transform.position = pos;
        }
    }
}
