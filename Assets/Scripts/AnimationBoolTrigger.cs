using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationBoolTrigger : MonoBehaviour
{
    public string Parameter;

    public bool Value
    {
        get { return GetComponent<Animator>().GetBool(Parameter); }
        set { GetComponent<Animator>().SetBool(Parameter, value); }
    }
}
