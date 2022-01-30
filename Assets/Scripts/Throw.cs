using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Throw : MonoBehaviour
{
    public Transform Room;
    public float SecondsAveraged = 0.25f;
    public float ForceMultiplier = 100f;

    private Rigidbody _rigidbody;
    private bool _ready;

    private Queue<Tuple<float, Vector3>> _positions = new Queue<Tuple<float, Vector3>>();

    public void Ready()
    {
        _ready = true;
    }

    public void Trigger()
    {
        if (_ready)
        {
            var count = _positions.Count - 1;
            var velocity = Vector3.zero;
            var previousPosition = _positions.Dequeue();

            while (_positions.Count > 0)
            {
                var pos = _positions.Dequeue();
                velocity += pos.Item2 - previousPosition.Item2;
            }

            velocity /= count;

            if (Room == null) { Room = GameObject.FindGameObjectsWithTag("Room").First().transform; }

            Debug.Log($"{gameObject.name} thrown at velocity {velocity}  force {velocity * ForceMultiplier}");

            _rigidbody = gameObject.AddComponent<Rigidbody>();
            if (HandDebugPanel.Instance != null) HandDebugPanel.Instance.Rigidbody = _rigidbody;
            _rigidbody.useGravity = false;
            _rigidbody.detectCollisions = true;
            _rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
            transform.SetParent(Room);
            _rigidbody.AddForce(velocity * ForceMultiplier);

            MagicManager.Instance.Activate();

            _ready = false;
        }
    }

    private void FixedUpdate()
    {
        if (_ready)
        {
            _positions.Enqueue(new Tuple<float, Vector3>(Time.time, transform.position));

            while (_positions.Peek().Item1 < Time.time - SecondsAveraged)
            {
                _positions.Dequeue();
            }
        }
    }
}
