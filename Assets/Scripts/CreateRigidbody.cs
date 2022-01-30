using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRigidbody : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public void Add()
    {
        Debug.Log("Creating rigid body");
        HandDebugPanel.Instance.Rigidbody = _rigidbody = gameObject.AddComponent<Rigidbody>();
        _rigidbody.useGravity = false;
        Debug.Log("Rigid body created");
    }

    public void Remove()
    {
        Destroy(_rigidbody);
    }
}
