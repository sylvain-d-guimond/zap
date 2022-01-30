using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    public float Smoothing;
    public float Delay;

    private bool _active;
    private float _startTime;

    public void Set(Transform parent)
    {
        //Debug.Log($"Transition, scale before:{transform.lossyScale}, {transform.lossyScale.x}");
        var scale = transform.lossyScale;
        transform.SetParent(parent, true);
        //transform.SetGlobalScale(scale);
        //Debug.Log($"Transition, scale after:{transform.lossyScale}, {transform.lossyScale.x}");
        _active = true;
        _startTime = Time.time;
        //StartCoroutine(SetScale(scale));
    }

    IEnumerator SetScale(Vector3 scale)
    {
        yield return null;

        transform.SetGlobalScale(scale);
        Debug.Log($"Transition, scale after after:{transform.lossyScale}, {transform.lossyScale.x}");
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (_active)
        {
            if (Time.time > Delay + _startTime)
            {
                transform.localPosition = Vector3.zero;
                _active = false;
            }
            else
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, Time.deltaTime / Smoothing);
            }
        }

    }
}
