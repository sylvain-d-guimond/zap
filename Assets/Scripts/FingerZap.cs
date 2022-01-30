using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FingerZap : MonoBehaviour
{
    public AvgDistance Distance;
    public GameObject SpawnObject;
    public float TargetScale;
    public float TargetDistance = 0.09f;
    public Transform StartPosition;
    public Transform EndPosition;

    public UnityEvent OnActivate;
    public UnityEvent OnDeactivate;

    private bool _active;
    private Transform _effect;

    public void Activate()
    {
        if (!_active)
        {
            OnActivate.Invoke();

            var effect = Instantiate(SpawnObject, StartPosition, false);
            effect.transform.localScale = Vector3.zero;
            _effect = effect.transform;

            _active = true;
        }
    }

    public void Deactivate()
    {
        OnDeactivate.Invoke();
    }

    private void Update()
    {
        if (_active)
        {
            if (Distance.Distance > TargetDistance)
            {
                _effect.GetComponent<Transition>().Set(EndPosition);
                _effect.localScale = TargetScale * Vector3.one;
                _active = false;
                Deactivate();
            }

            _effect.localScale = Vector3.one * TargetScale * (TargetDistance - (TargetDistance - Distance.Distance)) / TargetDistance;

        }
    }
}
