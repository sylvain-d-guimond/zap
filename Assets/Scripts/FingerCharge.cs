using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Events;

public class FingerCharge : MonoBehaviour
{
    public VisualEffect Effect;
    public float DesiredAngle = 130f;
    public float MinLoadCharge = 0.7f;
    public float LoadDuration = 3f;
    public Handedness Hand;

    public UnityEvent OnLoaded;

    private bool _charging;
    [SerializeField] float _load;

    private void OnEnable()
    {
        _charging = true;
    }


    void Update()
    {
        if (_charging)
        {
            var hand = HandManager.Instance;
            var angle = hand.FingerAngle(Hand, Fingers.All);
            var charge = 1 - Mathf.Abs(DesiredAngle - angle) / DesiredAngle;

            _load = Mathf.Clamp01(_load + (charge > MinLoadCharge ? 1 : -1) * (Time.deltaTime / LoadDuration));

            if (Mathf.Approximately(_load, 1f))
            {
                _load = 1f;
                charge = 1f;
                _charging = false;
                if (DebugMode.instance.DebugLevel <= DebugLevels.Debug) Debug.Log($"Load from {gameObject.name} triggering {OnLoaded.GetPersistentEventCount()} events");
                OnLoaded.Invoke();
                if (DebugMode.instance.DebugLevel <= DebugLevels.Debug) Debug.Log("Loaded");
            }

            Effect.SetFloat("Load", _load);
            Effect.SetFloat("Charge", charge);
        }
    }
}
