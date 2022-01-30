using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDirection : MonoBehaviour
{
    public Transform To;
    public Transform From;

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(To.position - From.position);
    }
}
