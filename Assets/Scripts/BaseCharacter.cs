using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseCharacter : MonoBehaviour
{
    public float Health = 1f;

    public FloatEvent OnHealthChanged;

    protected virtual void Update()
    {
    }

    public virtual void Damage(float value)
    {
        Health -= value;

        OnHealthChanged.Invoke(Health);

        if (Health < 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Debug.Log($"{name} died");
    }
}

[Serializable]
public class FloatEvent : UnityEvent<float> { }