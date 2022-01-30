using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Luck : MonoBehaviour
{
    public float Factor = 1;
    public UnityEvent OnTrigger;

    public void Trigger()
    {
        if (Random.Range(0f, 1f) < Factor) OnTrigger.Invoke();
    }
}
