using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseCharacter : MonoBehaviour
{
    public static int idCounter;

    public float Health = 1f;
    public float Defense = 0f;

    public FloatEvent OnHealthChanged;
    public UnityEvent OnDeath;

    private int _id = idCounter++;

    public bool Alive { get => _isAlive; }

    protected bool _isAlive = true;

    protected virtual void Update()
    {
    }

    public virtual void Damage(float value)
    {
        Health -= (1-Defense)*value;

        OnHealthChanged.Invoke(Health);

        if (DebugMode.instance.DebugLevel <= DebugLevels.Debug) Debug.Log($"Character {gameObject.name} damaged by {value}");

        if (Health < 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        _isAlive = false;
        OnDeath.Invoke();
        if (DebugMode.instance.DebugLevel <= DebugLevels.Debug) Debug.Log($"{name} died");
    }

    public override string ToString()
    {
        return $"{name} id: {_id} health:{Health} def:{Defense} alive:{(Alive?"yes":"no")}";
    }
}

[Serializable]
public class FloatEvent : UnityEvent<float> { }