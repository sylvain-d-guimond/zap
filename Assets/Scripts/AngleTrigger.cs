using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AngleTrigger : MonoBehaviour
{
    public Handedness Hand;
    public Fingers Finger;
    public Operator Operator;
    public float Value;
    public bool Active;
    public bool DeactivateOnTrigger = true;

    public UnityEvent OnTrigger;

    public string DebugMessage;

    private HandManager _handMgr;

    private void Start()
    {
        Debug.Log($"Angle trigger on for {Operator} {Value}");
        _handMgr = HandManager.Instance;
    }

    public void SetActive(bool value)
    {
        Debug.Log($"{gameObject.name} sets angle trigger for {Operator} {Value} active: {value}");
        Active = value;
    }

    void Update()
    {
        if (Active)
        {
            var angle = 0f;
            angle = _handMgr.FingerAngle(Hand, Finger);
            switch (Operator)
            {
                case Operator.LessThan:
                    if (angle < Value)
                    {
                        Debug.Log($"AT {angle} < {Value}: {DebugMessage}");
                        OnTrigger.Invoke();
                        if (DeactivateOnTrigger) Active = false;
                    }
                    break;
                case Operator.Equals:
                    if (angle == Value)
                    {
                        Debug.Log($"AT {angle} = {Value}: {DebugMessage}");
                        OnTrigger.Invoke();
                        if (DeactivateOnTrigger) Active = false;
                    }
                    break;
                case Operator.MoreThan:
                    if (angle > Value)
                    {
                        Debug.Log($"AT {angle} < {Value}: {DebugMessage}");
                        OnTrigger.Invoke();
                        if (DeactivateOnTrigger) Active = false;
                    }
                    break;
            }
        }
    }

    private void Invoke()
    {
        OnTrigger.Invoke();
        Debug.Log($"AT: {DebugMessage}");
    }
}

public enum Operator
{
    LessThan,
    Equals,
    MoreThan,
}