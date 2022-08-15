using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Throw : MonoBehaviour
{
    public float SecondsAveraged = 0.25f;
    public float ForceMultiplier = 100f;

    public UnityEvent OnThrow;

    private Rigidbody _rigidbody;
    private bool _ready;
    private Transform _room;

    private Queue<Tuple<float, Vector3>> _positions = new Queue<Tuple<float, Vector3>>();

    public void Ready()
    {
        _ready = true;
    }

    public void Trigger()
    {
        if (_ready)
        {
            _ready = false;
            var count = _positions.Count - 1;
            var velocity = Vector3.zero;
            var previousPosition = _positions.Dequeue();

            while (_positions.Count > 0)
            {
                var pos = _positions.Dequeue();

                velocity += pos.Item2 - previousPosition.Item2;
            }

            velocity /= count;

            if (_room == null) { _room = Room.Instance.transform; }

            Debug.Log($"{gameObject.name} thrown at velocity {velocity} force {velocity * ForceMultiplier} valueCount {count}");

            _rigidbody = gameObject.AddComponent<Rigidbody>();
            if (HandDebugPanel.Instance != null) HandDebugPanel.Instance.Rigidbody = _rigidbody;
            _rigidbody.useGravity = false;
            _rigidbody.detectCollisions = true;
            _rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
            transform.SetParent(_room, true);
            _rigidbody.AddForce(velocity * ForceMultiplier);

            var magic = GetComponent<Magic>();
            if (magic != null)
            {
                magic.Activate();
                magic.Stage = MagicStage.Thrown;
            }

            OnThrow.Invoke();
            _ready = false;
            MagicManager.Instance.Activate();
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
