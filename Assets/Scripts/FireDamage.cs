using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamage : Damage
{
    protected override BaseCharacter OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Weapon"))
        {
            var fireball = other.gameObject.GetComponentInChildren<FireBallDamage>();

            fireball?.Trigger();
        }

        return base.OnTriggerEnter(other);
    }
}
