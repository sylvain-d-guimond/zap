
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EyeColor : MonoBehaviour
{
    [ColorUsageAttribute(true, true)]
    public Color Color1;
    [ColorUsageAttribute(true, true)]
    public Color Color2;

    public float Damper;
    public float Delay;

    public SetColorEvent SetColor;

    private float _startTime;
    private bool _in;
    private bool _out;
    [SerializeField] private Color _lastColor;

    void Start()
    {
        Out();
    }

    public void In()
    {
        if (DebugMode.instance.DebugLevel <= DebugLevels.Debug) Debug.Log("Color in");
        _in = true; _out = false;
        _startTime = Time.time;
    }

    public void Out()
    {
        if (DebugMode.instance.DebugLevel <= DebugLevels.Debug) Debug.Log("Color out");
        _out = true; _in = false;
        _startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (_in)
        {
            if (Time.time - _startTime > Delay)
            {
                _in = false;
                SetColor.Invoke(Color2);
            }
            else
            {
                SetColor.Invoke(_lastColor = Color.Lerp(_lastColor, Color2, Time.deltaTime/Damper));
            }
        }
        if (_out)
        {
            if (Time.time - _startTime > Delay)
            {
                _out = false;
                SetColor.Invoke(Color1);
            }
            else
            {
                SetColor.Invoke(_lastColor = Color.Lerp(_lastColor, Color1, Time.deltaTime / Damper));
            }
        }
    }
}

[Serializable]
public class SetColorEvent : UnityEvent<Color> { }
