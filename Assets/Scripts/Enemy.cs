using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

public class Enemy : BaseCharacter
{
    private static float _desiredDistance = 1.5f;
    private static float _moveDuration = 4f;
    private static float _chargeDuration = 3f;
    private static float _smoothing = 2f;
    private static float _firingForce = 3f;

    public VisualEffect Charge;
    public GameObject Projectile;

    private bool _moving;
    private float _start;

    private void Start()
    {
        _moving = true;
        _start = Time.time;
    }

    public void Fire()
    {
        var fired = Instantiate(Projectile, Room.Instance.transform);
        fired.transform.position = transform.position;

        var direction = (Game.Instance.CurrentPlayer.transform.position - transform.position).normalized;
        fired.GetComponent<Rigidbody>().AddForce(direction * _firingForce);
    }

    protected override void Update()
    {
        base.Update();
        if (_isAlive)
        {
            //Moving behavior
            if (_moving)
            {
                if (Time.time > _start + _moveDuration)
                {
                    _start = Time.time;
                    _moving = false;
                }

                var desiredPosition = (transform.position - Game.Instance.CurrentPlayer.transform.position).normalized * _desiredDistance +
                    Game.Instance.CurrentPlayer.transform.position;

                transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime / _smoothing);
            }
            //Attacking behavior
            else
            {
                if (Time.time > _start + _chargeDuration)
                {
                    _start = Time.time;
                    _moving = true;
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    Fire();
                }

                Charge.SetFloat("Charge", (Time.time - _start) / _chargeDuration);
            }
        }
    }

    protected override void Die()
    {
        base.Die();
        var rigidBody = GetComponent<Rigidbody>();
        rigidBody.useGravity = true;
        rigidBody.isKinematic = false;
        //Destroy(this.gameObject);
    }
}
