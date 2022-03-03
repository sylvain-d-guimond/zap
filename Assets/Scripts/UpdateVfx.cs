using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class UpdateVfx : MonoBehaviour
{
    public VisualEffect Effect;

    private void Update()
    {
        if (Effect != null)
        {
            Effect.SetVector3("Position", transform.position);
        }
    }
}
