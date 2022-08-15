using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float Value;

    protected Dictionary<GameObject, BaseCharacter> _colliding = new Dictionary<GameObject, BaseCharacter>();
    protected List<GameObject> _keys = new List<GameObject>();

    protected virtual BaseCharacter OnTriggerEnter(Collider other)
    {
        var target = other.gameObject.GetComponent<BaseCharacter>();
        if (target != null && target.Alive)
        {
            if (target is Enemy && !_colliding.ContainsKey(other.gameObject))
            {
                _colliding.Add(other.gameObject, target);
                Debug.Log($"{name} from {transform.parent.name} begins collision with {target}");
            }

            return target;
        }

        return null;
    }
    protected virtual void OnTriggerStay(Collider other)
    {
    }

    protected virtual BaseCharacter OnTriggerExit(Collider other)
    {
        if (_colliding.ContainsKey(other.gameObject))
        {
            var exiting = _colliding[other.gameObject];
            _colliding.Remove(other.gameObject);
            Debug.Log($"{name} from {transform.parent.name} ends collision with {exiting}");
            return exiting;
        }
        return null;
    }

    protected virtual void OnDisable()
    {
        _colliding.Clear();
    }

    private void FixedUpdate()
    {
        foreach (var key in _colliding.Keys)
        {
            if (_colliding[key] == null) _keys.Add(key);
        }

        foreach (var key in _keys)
        {
            _colliding.Remove(key);
        }
        _keys.Clear();

        var dead = new List<GameObject>();
        foreach (var target in _colliding.Keys)
        {
            _colliding[target].Damage(Value * Time.deltaTime);
            Debug.Log($"{name} from {transform.parent.name} damaged {target} by {Value * Time.deltaTime} at {Time.time}");
            if (!_colliding[target].Alive) dead.Add(target);
        }

        foreach (var ded in dead)
        {
            _colliding.Remove(ded);
        }
    }
}
