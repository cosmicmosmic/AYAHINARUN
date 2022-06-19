using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemainDistance : MonoBehaviour
{
    [SerializeField] private Color green;
    [SerializeField] private Color yellow;
    [SerializeField] private Color red;

    [SerializeField] private Transform trTarget;
    [SerializeField] private Transform trGoal;

    [SerializeField] private Text txtGoal;
    [SerializeField] private UIGradient gradient;

    private void Update()
    {
        UpdateDistance();
    }

    private void UpdateDistance()
    {
        var targetX = trTarget.position.x;
        var goalX = trGoal.position.x;

        var dist = (int)(goalX - targetX);

        if (dist > 30)
        {
            gradient.m_color1 = green;
        }
        else if (dist > 10)
        {
            gradient.m_color1 = yellow;
        }
        else
        {
            gradient.m_color1 = red;
        }

        txtGoal.text = string.Format("{0}m¡æ", dist);
    }
}
