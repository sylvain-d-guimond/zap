using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Flamethrower : MonoBehaviour
{
    public VisualEffect Effect;
    public Vector3 Forward;
    public bool Firing;

    public void SetFiring(bool firing)
    {
        Firing = firing;
        if (Effect != null)
        {
            Effect.SetBool("Firing", firing);
        }
    }

    private void Update()
    {
        if (Effect!= null && Firing)
        {
            Effect.SetVector3("Position", transform.position);
            Effect.SetVector3("Direction", transform.rotation* Forward);
        }
    }
}
