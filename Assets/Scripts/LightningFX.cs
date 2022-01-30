using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class LightningFX : MonoBehaviour
{
    public Transform PointA;
    public Transform PointB;

    public VisualEffect Effect;

    public void SetPointA(Transform t)
    {
        PointA = t;
    }

    public void SetPointB(Transform t)
    {
        PointB = t;
    }

    void Update()
    {
        if (PointA == null || PointB == null)
        {
            Effect.enabled = false;
        }
        else
        {
            Effect.enabled = true;
            var fingerSpan = PointB.position - PointA.position;
            var midpoint = PointA.position + (fingerSpan) / 2;
            transform.position = midpoint;
            var scale = fingerSpan.magnitude;
            transform.localScale = new Vector3(scale, scale, scale);
            var firstCross = Vector3.Cross(fingerSpan, Camera.main.transform.position - midpoint);
            var secondCross = Vector3.Cross(fingerSpan, firstCross);
            transform.rotation = Quaternion.LookRotation(firstCross, secondCross);
        }
    }
}
