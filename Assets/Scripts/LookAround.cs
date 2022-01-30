using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAround : MonoBehaviour
{
    public Vector2 Delay = new Vector2(0.5f, 2.5f);
    public Vector2 XBounds = new Vector2(-25, 25);
    public Vector2 YBounds = new Vector2(-45, 45);
    public float Smoothing = 0.1f;

    private float _startTime;
    private float _lastDelay;
    private Vector2 _nextTarget;

    // Update is called once per frame
    void Update()
    {
        if (Time.time > _startTime + _lastDelay)
        {
            _lastDelay = Random.Range(Delay.x, Delay.y);
            _nextTarget = new Vector2(Random.Range(XBounds.x, XBounds.y), Random.Range(YBounds.x, YBounds.y));
            _startTime = Time.time;
            //Debug.Log($"Rotation:{transform.localRotation.eulerAngles}");
        }
        else
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(_nextTarget.x, _nextTarget.y, 0), Time.deltaTime/Smoothing);

        }
    }
}
