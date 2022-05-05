using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float Value;

    private Dictionary<GameObject, BaseCharacter> _colliding = new Dictionary<GameObject, BaseCharacter>();

    private void OnTriggerEnter(Collider other)
    {
        var target = other.gameObject.GetComponent<BaseCharacter>();
        if (target != null)
        {
            _colliding.Add(other.gameObject, target);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        foreach (var target in _colliding.Values)
        {
            target.Damage(Value * Time.deltaTime);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_colliding.ContainsKey(other.gameObject))
        {
            _colliding.Remove(other.gameObject);
        }
    }

    private void OnDisable()
    {
        _colliding.Clear();
    }
}
