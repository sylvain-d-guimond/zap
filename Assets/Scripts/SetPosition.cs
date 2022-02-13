using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPosition : MonoBehaviour
{
    public void Call(Transform transform)
    {
        this.transform.position = transform.position;
    }
}
